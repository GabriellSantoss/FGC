using PagamentoService.Clients;
using PagamentoService.Model;
using PagamentoService.Repositories.Interfaces;
using PagamentoService.Requests;
using PagamentoService.Responses;
using PagamentoService.Services.Interfaces;

namespace PagamentoService.Services.Implementations
{
    public class CompraService : ICompraService
    {
        private readonly ICompraRepository _compraRepository;
        private readonly IPagamentoRepository _pagamentoRepository;
        private readonly IJogoClient _jogoClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUsuarioClient _usuarioClient;

        public CompraService(
     ICompraRepository compraRepository,
     IPagamentoRepository pagamentoRepository,
     IJogoClient jogoClient,
     IUsuarioClient usuarioClient,
     IHttpContextAccessor httpContextAccessor)
        {
            _compraRepository = compraRepository;
            _pagamentoRepository = pagamentoRepository;
            _jogoClient = jogoClient;
            _usuarioClient = usuarioClient;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<CompraResponse>> BuscarTodosAsync()
        {
            var compras = await _compraRepository.BuscarTodosAsync();

            return compras
                .Select(ConverterParaResponse)
                .ToList();
        }

        public async Task<CompraResponse?> BuscarPorIdAsync(long id)
        {
            var compra = await _compraRepository.BuscarPorIdAsync(id);

            if (compra == null)
                return null;

            return ConverterParaResponse(compra);
        }

        public async Task<CompraResponse> CadastrarAsync(
       CompraRequest request)
        {
            var authorizationHeader =
                _httpContextAccessor.HttpContext?
                    .Request.Headers.Authorization
                    .ToString();

            var token = authorizationHeader?
                .Replace("Bearer ", "", StringComparison.OrdinalIgnoreCase);

            if (string.IsNullOrWhiteSpace(token))
                throw new UnauthorizedAccessException(
                    "Token não encontrado.");

            var pagamento =
                await _pagamentoRepository.BuscarPorIdAsync(
                    request.PagamentoId);

            if (pagamento == null)
                throw new KeyNotFoundException(
                    "Pagamento não encontrado.");

            if (pagamento.Status != "Aprovado")
                throw new InvalidOperationException(
                    "Pagamento ainda não foi aprovado.");

            if (pagamento.UsuarioId != request.UsuarioId)
                throw new InvalidOperationException(
                    "O usuário da compra é diferente do usuário do pagamento.");

            if (pagamento.JogoId != request.JogoId)
                throw new InvalidOperationException(
                    "O jogo da compra é diferente do jogo do pagamento.");

            var compraExistente =
                await _compraRepository.ExistePorPagamentoIdAsync(
                    request.PagamentoId);

            if (compraExistente)
                throw new InvalidDataException(
                    "Esse pagamento já possui uma compra vinculada.");

            var jogoExiste =
                await _jogoClient.JogoExisteAsync(
                    request.JogoId,
                    token);

            if (!jogoExiste)
                throw new KeyNotFoundException(
                    "Jogo não encontrado.");

            var compra = new Compra(
                request.UsuarioId,
                request.JogoId,
                request.PagamentoId,
                "Sistema"
            );

            await _compraRepository.AdicionarAsync(compra);
            await _compraRepository.SalvarAlteracoesAsync();

            return ConverterParaResponse(compra);
        }
        public async Task<CompraResponse?> AtualizarAsync(
            long id,
            CompraRequest request)
        {
            var compra = await _compraRepository.BuscarPorIdAsync(id);

            if (compra == null)
                return null;

            compra.UsuarioId = request.UsuarioId;
            compra.JogoId = request.JogoId;
            compra.PagamentoId = request.PagamentoId;
            compra.DataAtualizacao = DateTime.UtcNow;
            compra.AtualizadoPor = "Sistema";

            _compraRepository.Atualizar(compra);
            await _compraRepository.SalvarAlteracoesAsync();

            return ConverterParaResponse(compra);
        }

        public async Task<bool> ExcluirAsync(long id)
        {
            var compra = await _compraRepository.BuscarPorIdAsync(id);

            if (compra == null)
                return false;

            _compraRepository.Remover(compra);
            await _compraRepository.SalvarAlteracoesAsync();

            return true;
        }

        private static CompraResponse ConverterParaResponse(
            Compra compra)
        {
            return new CompraResponse
            {
                Id = compra.Id,
                UsuarioId = compra.UsuarioId,
                JogoId = compra.JogoId,
                PagamentoId = compra.PagamentoId,
                DataCompra = compra.DataCompra
            };
        }

        public async Task<List<CompraResponse>> BuscarPorUsuarioAsync(
    Guid usuarioId)
        {
            var compras =
                await _compraRepository.BuscarPorUsuarioAsync(usuarioId);

            return compras
                .Select(ConverterParaResponse)
                .ToList();
        }
    }
}