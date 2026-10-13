using System.Net;
using System.Net.Http.Headers;

namespace PagamentoService.Clients
{
    public class UsuarioClient : IUsuarioClient
    {
        private readonly HttpClient _httpClient;

        public UsuarioClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> UsuarioExisteAsync(
            Guid usuarioId,
            string token)
        {
            using var request = new HttpRequestMessage(
                HttpMethod.Get,
                $"api/Usuario/{usuarioId}");

            request.Headers.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.SendAsync(request);

            if (response.StatusCode == HttpStatusCode.NotFound)
                return false;

            response.EnsureSuccessStatusCode();

            return true;
        }
    }
}