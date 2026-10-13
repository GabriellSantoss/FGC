using UsuarioService.Model;
using UsuarioService.Repositories.Interfaces;
using UsuarioService.Requests;
using UsuarioService.Responses;
using UsuarioService.Services.Interfaces;

namespace UsuarioService.Services.Implementations
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<List<UsuarioResponse>> BuscarTodosAsync()
        {
            var usuarios = await _usuarioRepository.BuscarTodosAsync();

            return usuarios.Select(usuario => new UsuarioResponse
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Email = usuario.Email,
                Idade = usuario.Idade,
                Perfil = usuario.Perfil?.Descricao ?? ""
            }).ToList();
        }

        public async Task<UsuarioResponse?> BuscarPorIdAsync(long id)
        {
            var usuario = await _usuarioRepository.BuscarPorIdAsync(id);

            if (usuario == null)
                return null;

            return new UsuarioResponse
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Email = usuario.Email,
                Idade = usuario.Idade,
                Perfil = usuario.Perfil?.Descricao ?? ""
            };
        }

        public async Task<UsuarioResponse> CadastrarAsync(UsuarioRequest request)
        {
            var emailExiste = await _usuarioRepository.ExistePorEmailAsync(request.Email);

            if (emailExiste)
                throw new Exception("E-mail já cadastrado.");

            var usuario = new Usuario(
    request.Nome,
    string.Empty,
    string.Empty,
    request.Email,
  BCrypt.Net.BCrypt.HashPassword(request.Senha),
    request.DataNascimento,
    request.PerfilId,
    "Sistema"
);

            await _usuarioRepository.AdicionarAsync(usuario);
            await _usuarioRepository.SalvarAlteracoesAsync();

            return new UsuarioResponse
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Email = usuario.Email,
                Idade = usuario.Idade,
                Perfil = usuario.Perfil?.Descricao ?? ""
            };
        }

        public async Task<UsuarioResponse?> AtualizarAsync(long id, UsuarioRequest request)
        {
            var usuario = await _usuarioRepository.BuscarPorIdAsync(id);

            if (usuario == null)
                return null;

            usuario.Nome = request.Nome;
            usuario.Email = request.Email;
            usuario.DataNascimento = request.DataNascimento;
            usuario.PerfilId = request.PerfilId;
            usuario.DataAtualizacao = DateTime.UtcNow;
            usuario.AtualizadoPor = "Sistema";

            _usuarioRepository.AtualizarAsync(usuario);
            await _usuarioRepository.SalvarAlteracoesAsync();

            return new UsuarioResponse
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Email = usuario.Email,
                Idade = usuario.Idade,
                Perfil = usuario.Perfil?.Descricao ?? ""
            };
        }

        public async Task<bool> ExcluirAsync(long id)
        {
            var usuario = await _usuarioRepository.BuscarPorIdAsync(id);

            if (usuario == null)
                return false;

            _usuarioRepository.RemoverAsync(usuario);
            await _usuarioRepository.SalvarAlteracoesAsync();

            return true;
        }
    }
}