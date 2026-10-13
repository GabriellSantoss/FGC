using System.Net;

namespace PagamentoService.Clients
{
    public class JogoClient : IJogoClient
    {
        private readonly HttpClient _httpClient;

        public JogoClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> JogoExisteAsync(long jogoId, string token)
        {
            using var request = new HttpRequestMessage(
                HttpMethod.Get,
                $"api/Jogo/{jogoId}");

            request.Headers.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue(
                    "Bearer",
                    token);

            var response = await _httpClient.SendAsync(request);

            if (response.StatusCode == HttpStatusCode.NotFound)
                return false;

            response.EnsureSuccessStatusCode();

            return true;
        }
    }
}