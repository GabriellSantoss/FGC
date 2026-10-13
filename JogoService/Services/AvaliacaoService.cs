using JogoService.Entidades.Requests;
using JogoService.Model;
using JogoService.Repositories.Implementations;
using JogoService.Repositories.Interfaces;
using JogoService.Requests;
using JogoService.Responses;

namespace JogoService.Services
{
    public class AvaliacaoService : IAvaliacaoService
    {
        private readonly IAvaliacaoRepository _avaliacaoRepository;
        private readonly IJogoRepository _jogoRepository;

        public AvaliacaoService(
    IAvaliacaoRepository avaliacaoRepository,
    IJogoRepository jogoRepository)
        {
            _avaliacaoRepository = avaliacaoRepository;
            _jogoRepository = jogoRepository;
        }

        public async Task<List<AvaliacaoResponse>> BuscarTodosAsync()
        {
            var avaliacoes = await _avaliacaoRepository.BuscarTodosAsync();

            return avaliacoes
                .Select(ConverterParaResponse)
                .ToList();
        }

        public async Task<AvaliacaoResponse?> BuscarPorIdAsync(long id)
        {
            var avaliacao = await _avaliacaoRepository.BuscarPorIdAsync(id);

            if (avaliacao == null)
                return null;

            return ConverterParaResponse(avaliacao);
        }

        public async Task<AvaliacaoResponse> CadastrarAsync(
     AvaliacaoRequest request)
        {
            var jogo = await _jogoRepository.BuscarPorIdAsync(request.IdJogo);

            if (jogo == null)
                throw new Exception("Jogo não encontrado.");

            var avaliacao = new Avaliacao(
                request.IdUsuario,
                request.IdJogo,
                request.Nota,
                request.Comentario,
                "Sistema"
            );

            await _avaliacaoRepository.AdicionarAsync(avaliacao);
            await _avaliacaoRepository.SalvarAlteracoesAsync();

            return ConverterParaResponse(avaliacao);
        }

        public async Task<AvaliacaoResponse?> AtualizarAsync(
            long id,
            AvaliacaoRequest request)
        {
            var avaliacao = await _avaliacaoRepository.BuscarPorIdAsync(id);

            if (avaliacao == null)
                return null;

            avaliacao.Comentario = request.Comentario;
            avaliacao.DataAtualizacao = DateTime.UtcNow;
            avaliacao.AtualizadoPor = "Sistema";

            _avaliacaoRepository.Atualizar(avaliacao);
            await _avaliacaoRepository.SalvarAlteracoesAsync();

            return ConverterParaResponse(avaliacao);
        }

        public async Task<bool> ExcluirAsync(long id)
        {
            var avaliacao = await _avaliacaoRepository.BuscarPorIdAsync(id);

            if (avaliacao == null)
                return false;

            _avaliacaoRepository.Remover(avaliacao);
            await _avaliacaoRepository.SalvarAlteracoesAsync();

            return true;
        }

        private static AvaliacaoResponse ConverterParaResponse(Avaliacao avaliacao)
        {
            return new AvaliacaoResponse
            {
                
                Comentario = avaliacao.Comentario,
                Nota = avaliacao.Nota,
                IdUsuario = avaliacao.IdUsuario,
                IdJogo = avaliacao.IdJogo
            };
        }
    }
}
