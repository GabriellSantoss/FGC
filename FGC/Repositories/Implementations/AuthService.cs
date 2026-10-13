using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UsuarioService.Model;
using UsuarioService.Repositories.Interfaces;
using UsuarioService.Requests;
using UsuarioService.Responses;
using UsuarioService.Services.Interfaces;

namespace UsuarioService.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IConfiguration _configuration;

        public AuthService(
            IUsuarioRepository usuarioRepository,
            IConfiguration configuration)
        {
            _usuarioRepository = usuarioRepository;
            _configuration = configuration;
        }

        private string GerarToken(Usuario usuario)
        {
            var chave = Encoding.UTF8.GetBytes(
                _configuration["Jwt:Secret"]);

            var credenciais = new SigningCredentials(
                new SymmetricSecurityKey(chave),
                SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
        new Claim(JwtRegisteredClaimNames.Sub, usuario.Id.ToString()),
        new Claim(JwtRegisteredClaimNames.Email, usuario.Email),
        new Claim(ClaimTypes.Name, usuario.Nome),
        new Claim(ClaimTypes.Role, usuario.Perfil.Descricao)
    };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: credenciais);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<LoginResponse?> LoginAsync(LoginRequest request)
        {
            var usuario = await _usuarioRepository.BuscarPorEmailAsync(request.Email);

            if (usuario == null)
                return null;

            if (!BCrypt.Net.BCrypt.Verify(request.Senha, usuario.HashSenha))
                return null;

            var expiracao = DateTime.UtcNow.AddHours(2);

            var token = GerarToken(usuario);

            return new LoginResponse
            {
                Token = token,
                Expiracao = expiracao
            };
        }
    }
    }
