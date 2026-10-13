namespace PagamentoService.Clients
{
    public interface IUsuarioClient
    {
        Task<bool> UsuarioExisteAsync(Guid usuarioId, string token);
    }
}