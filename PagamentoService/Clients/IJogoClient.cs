namespace PagamentoService.Clients
{
    public interface IJogoClient
    {
        Task<bool> JogoExisteAsync(long jogoId, string token);
    }
}