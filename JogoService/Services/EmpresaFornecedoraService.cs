using JogoService.Model;
using JogoService.Repositories.Interfaces;
using JogoService.Requests;
using JogoService.Responses;
using JogoService.Services.Interfaces;

namespace JogoService.Services.Implementations
{
    public class EmpresaFornecedoraService : IEmpresaFornecedoraService
    {
        private readonly IEmpresaFornecedoraRepository _empresaFornecedoraRepository;

        public EmpresaFornecedoraService(
            IEmpresaFornecedoraRepository empresaFornecedoraRepository)
        {
            _empresaFornecedoraRepository = empresaFornecedoraRepository;
        }

        public async Task<List<EmpresaFornecedoraResponse>> BuscarTodosAsync()
        {
            var empresas = await _empresaFornecedoraRepository.BuscarTodosAsync();

            return empresas
                .Select(ConverterParaResponse)
                .ToList();
        }

        public async Task<EmpresaFornecedoraResponse?> BuscarPorIdAsync(long id)
        {
            var empresa =
                await _empresaFornecedoraRepository.BuscarPorIdAsync(id);

            if (empresa == null)
                return null;

            return ConverterParaResponse(empresa);
        }

        public async Task<EmpresaFornecedoraResponse> CadastrarAsync(
            EmpresaFornecedoraRequest request)
        {
            var empresa = new EmpresaFornecedora(
                request.Nome,
                request.CNPJ,
                "Sistema"
            );

            await _empresaFornecedoraRepository.AdicionarAsync(empresa);
            await _empresaFornecedoraRepository.SalvarAlteracoesAsync();

            return ConverterParaResponse(empresa);
        }

        public async Task<EmpresaFornecedoraResponse?> AtualizarAsync(
            long id,
            EmpresaFornecedoraRequest request)
        {
            var empresa =
                await _empresaFornecedoraRepository.BuscarPorIdAsync(id);

            if (empresa == null)
                return null;

            empresa.Nome = request.Nome;
            empresa.CNPJ = request.CNPJ;
            empresa.DataAtualizacao = DateTime.UtcNow;
            empresa.AtualizadoPor = "Sistema";

            _empresaFornecedoraRepository.Atualizar(empresa);
            await _empresaFornecedoraRepository.SalvarAlteracoesAsync();

            return ConverterParaResponse(empresa);
        }

        public async Task<bool> ExcluirAsync(long id)
        {
            var empresa =
                await _empresaFornecedoraRepository.BuscarPorIdAsync(id);

            if (empresa == null)
                return false;

            _empresaFornecedoraRepository.Remover(empresa);
            await _empresaFornecedoraRepository.SalvarAlteracoesAsync();

            return true;
        }

        private static EmpresaFornecedoraResponse ConverterParaResponse(
            EmpresaFornecedora empresa)
        {
            return new EmpresaFornecedoraResponse
            {
                Id = empresa.Id,
                Nome = empresa.Nome,
                CNPJ = empresa.CNPJ
            };
        }
    }
}