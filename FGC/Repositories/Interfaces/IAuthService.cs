using UsuarioService.Requests;
using UsuarioService.Responses;

namespace UsuarioService.Services.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResponse?> LoginAsync(LoginRequest request);
    }
}