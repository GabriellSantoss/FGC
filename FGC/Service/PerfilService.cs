using UsuarioService.Model;
using UsuarioService.Repositories.Interfaces;
using UsuarioService.Requests;
using UsuarioService.Responses;
using UsuarioService.Services.Interfaces;

namespace UsuarioService.Services.Implementations
{
    public class PerfilService : IPerfilService
    {
        private readonly IPerfilRepository _perfilRepository;

        public PerfilService(IPerfilRepository perfilRepository)
        {
            _perfilRepository = perfilRepository;
        }
       
        public async Task<List<PerfilResponse>> BuscarTodosAsync()
        {
            var perfis = await _perfilRepository.BuscarTodosAsync();

            return perfis
                .Select(ConverterParaResponse)
                .ToList();
        }

        public async Task<PerfilResponse?> BuscarPorIdAsync(long id)
        {
            var perfil = await _perfilRepository.BuscarPorIdAsync(id);

            if (perfil == null)
                return null;

            return ConverterParaResponse(perfil);
        }

        public async Task<PerfilResponse> CadastrarAsync(PerfilRequest request)
        {
            var perfil = new Perfil(
                request.Descricao,
                "Sistema"
            );

            await _perfilRepository.AdicionarAsync(perfil);
            await _perfilRepository.SalvarAlteracoesAsync();

            return ConverterParaResponse(perfil);
        }

        public async Task<PerfilResponse?> AtualizarAsync(long id, PerfilRequest request)
        {
            var perfil = await _perfilRepository.BuscarPorIdAsync(id);

            if (perfil == null)
                return null;

            perfil.Descricao = request.Descricao;
            perfil.DataAtualizacao = DateTime.UtcNow;
            perfil.AtualizadoPor = "Sistema";

            _perfilRepository.Atualizar(perfil);
            await _perfilRepository.SalvarAlteracoesAsync();

            return ConverterParaResponse(perfil);
        }

        public async Task<bool> ExcluirAsync(long id)
        {
            var perfil = await _perfilRepository.BuscarPorIdAsync(id);

            if (perfil == null)
                return false;

            _perfilRepository.Remover(perfil);
            await _perfilRepository.SalvarAlteracoesAsync();

            return true;
        }

        private PerfilResponse ConverterParaResponse(Perfil perfil)
        {
            return new PerfilResponse
            {
                Id = perfil.Id,
                Descricao = perfil.Descricao
            };
        }

    }
}