using JogoService.Model;
using JogoService.Repositories.Interfaces;
using JogoService.Requests;
using JogoService.Responses;
using JogoService.Services.Interfaces;

namespace JogoService.Services.Implementations
{
    public class CategoriaService : ICategoriaService
    {
        private readonly ICategoriaRepository _categoriaRepository;

        public CategoriaService(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }

        public async Task<List<CategoriaResponse>> BuscarTodosAsync()
        {
            var categorias = await _categoriaRepository.BuscarTodosAsync();

            return categorias
                .Select(ConverterParaResponse)
                .ToList();
        }

        public async Task<CategoriaResponse?> BuscarPorIdAsync(long id)
        {
            var categoria = await _categoriaRepository.BuscarPorIdAsync(id);

            if (categoria == null)
                return null;

            return ConverterParaResponse(categoria);
        }

        public async Task<CategoriaResponse> CadastrarAsync(
            CategoriaRequest request)
        {
            var categoria = new Categoria(
                request.Descricao,
                "Sistema"
            );

            await _categoriaRepository.AdicionarAsync(categoria);
            await _categoriaRepository.SalvarAlteracoesAsync();

            return ConverterParaResponse(categoria);
        }

        public async Task<CategoriaResponse?> AtualizarAsync(
            long id,
            CategoriaRequest request)
        {
            var categoria = await _categoriaRepository.BuscarPorIdAsync(id);

            if (categoria == null)
                return null;

            categoria.Descricao = request.Descricao;
            categoria.DataAtualizacao = DateTime.UtcNow;
            categoria.AtualizadoPor = "Sistema";

            _categoriaRepository.Atualizar(categoria);
            await _categoriaRepository.SalvarAlteracoesAsync();

            return ConverterParaResponse(categoria);
        }

        public async Task<bool> ExcluirAsync(long id)
        {
            var categoria = await _categoriaRepository.BuscarPorIdAsync(id);

            if (categoria == null)
                return false;

            _categoriaRepository.Remover(categoria);
            await _categoriaRepository.SalvarAlteracoesAsync();

            return true;
        }

        private static CategoriaResponse ConverterParaResponse(
            Categoria categoria)
        {
            return new CategoriaResponse
            {
                Id = categoria.Id,
                Descricao = categoria.Descricao
            };
        }
    }
}