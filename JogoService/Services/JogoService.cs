using JogoService.Model;
using JogoService.Repositories.Interfaces;
using JogoService.Requests;
using JogoService.Responses;
using JogoService.Services.Interfaces;

namespace JogoService.Services.Implementations
{
    public class JogoService : IJogoService
    {
        private readonly IJogoRepository _jogoRepository;

        public JogoService(IJogoRepository jogoRepository)
        {
            _jogoRepository = jogoRepository;
        }

        public async Task<List<JogoResponse>> BuscarTodosAsync()
        {
            var jogos = await _jogoRepository.BuscarTodosAsync();

            return jogos
                .Select(ConverterParaResponse)
                .ToList();
        }

        public async Task<JogoResponse?> BuscarPorIdAsync(long id)
        {
            var jogo = await _jogoRepository.BuscarPorIdAsync(id);

            if (jogo == null)
                return null;

            return ConverterParaResponse(jogo);
        }

        public async Task<JogoResponse> CadastrarAsync(JogoRequest request)
        {
            var jogo = new Jogo(
                request.Nome,
                request.Descricao,
                request.Tamanho,
                request.Preco,
                request.IdCategoria,
                request.IdadeMinima,
                request.Ativo,
                request.IdFornecedor,
                "Sistema"
            );
            await _jogoRepository.AdicionarAsync(jogo);
            await _jogoRepository.SalvarAlteracoesAsync();

            var jogoCompleto = await _jogoRepository.BuscarPorIdAsync(jogo.Id);

            return ConverterParaResponse(jogoCompleto!);
        }

        public async Task<JogoResponse?> AtualizarAsync(long id, JogoRequest request)
        {
            var jogo = await _jogoRepository.BuscarPorIdAsync(id);

            if (jogo == null)
                return null;

            jogo.Nome = request.Nome;
            jogo.Descricao = request.Descricao;
            jogo.Tamanho = request.Tamanho;
            jogo.Preco = request.Preco;
            jogo.IdCategoria = request.IdCategoria;
            jogo.IdadeMinima = request.IdadeMinima;
            jogo.Ativo = request.Ativo;
            jogo.IdFornecedor = request.IdFornecedor;
            jogo.DataAtualizacao = DateTime.UtcNow;
            jogo.AtualizadoPor = "Sistema";

            _jogoRepository.Atualizar(jogo);
            await _jogoRepository.SalvarAlteracoesAsync();

            return ConverterParaResponse(jogo);
        }

        public async Task<bool> ExcluirAsync(long id)
        {
            var jogo = await _jogoRepository.BuscarPorIdAsync(id);

            if (jogo == null)
                return false;

            _jogoRepository.Remover(jogo);
            await _jogoRepository.SalvarAlteracoesAsync();

            return true;
        }

        private static JogoResponse ConverterParaResponse(Jogo jogo)
        {
            return new JogoResponse
            {
                Id = jogo.Id,
                Nome = jogo.Nome,
                Descricao = jogo.Descricao,
                Tamanho = jogo.Tamanho,
                Preco = jogo.Preco,
                Categoria = jogo.Categoria?.Descricao ?? string.Empty,
                IdadeMinima = jogo.IdadeMinima,
                Ativo = jogo.Ativo,
                NumeroVendas = jogo.NumeroVendas,
                EmpresaFornecedora =
                    jogo.EmpresaFornecedora?.Nome ?? string.Empty
            };
        }
    }
}