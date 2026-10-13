using PagamentoService.Messaging;
using PagamentoService.Model;
using PagamentoService.Repositories.Interfaces;
using PagamentoService.Requests;
using PagamentoService.Responses;
using PagamentoService.Services.Interfaces;

namespace PagamentoService.Services.Implementations
{
    public class PagamentoService : IPagamentoService
    {
        private readonly IPagamentoRepository _pagamentoRepository;
        private readonly IRabbitMqPublisher _publisher;

        public PagamentoService(
            IPagamentoRepository pagamentoRepository,
            IRabbitMqPublisher publisher)
        {
            _pagamentoRepository = pagamentoRepository;
            _publisher = publisher;
        }

        public async Task<List<PagamentoResponse>> BuscarTodosAsync()
        {
            var pagamentos =
                await _pagamentoRepository.BuscarTodosAsync();

            return pagamentos
                .Select(ConverterParaResponse)
                .ToList();
        }

        public async Task<PagamentoResponse?> BuscarPorIdAsync(long id)
        {
            var pagamento =
                await _pagamentoRepository.BuscarPorIdAsync(id);

            if (pagamento == null)
                return null;

            return ConverterParaResponse(pagamento);
        }

        public async Task<PagamentoResponse> CadastrarAsync(
            PagamentoRequest request)
        {
            var pagamento = new Pagamento
            {
                UsuarioId = request.UsuarioId,
                JogoId = request.JogoId,
                Valor = request.Valor,
                FormaPagamento = request.FormaPagamento,
                Status = "Pendente",
                CriadoPor = "Sistema"
            };

            await _pagamentoRepository.AdicionarAsync(pagamento);
            await _pagamentoRepository.SalvarAlteracoesAsync();

            await _publisher.PublicarAsync("pagamento-criado", new
            {
                pagamento.Id,
                pagamento.UsuarioId,
                pagamento.JogoId,
                pagamento.Valor,
                pagamento.Status,
                pagamento.FormaPagamento,
                pagamento.DataCriacao
            });

            return ConverterParaResponse(pagamento);
        }

        public async Task<PagamentoResponse?> AtualizarStatusAsync(
            long id,
            string status)
        {
            var pagamento =
                await _pagamentoRepository.BuscarPorIdAsync(id);

            if (pagamento == null)
                return null;

            pagamento.Status = status;
            pagamento.DataAtualizacao = DateTime.UtcNow;
            pagamento.AtualizadoPor = "Sistema";

            _pagamentoRepository.Atualizar(pagamento);
            await _pagamentoRepository.SalvarAlteracoesAsync();

            return ConverterParaResponse(pagamento);
        }

        public async Task<bool> ExcluirAsync(long id)
        {
            var pagamento =
                await _pagamentoRepository.BuscarPorIdAsync(id);

            if (pagamento == null)
                return false;

            _pagamentoRepository.Remover(pagamento);
            await _pagamentoRepository.SalvarAlteracoesAsync();

            return true;
        }

        private static PagamentoResponse ConverterParaResponse(
            Pagamento pagamento)
        {
            return new PagamentoResponse
            {
                Id = pagamento.Id,
                UsuarioId = pagamento.UsuarioId,
                JogoId = pagamento.JogoId,
                Valor = pagamento.Valor,
                Status = pagamento.Status,
                FormaPagamento = pagamento.FormaPagamento,
                DataCriacao = pagamento.DataCriacao
            };
        }


    }
}