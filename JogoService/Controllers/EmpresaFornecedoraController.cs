using JogoService.Requests;
using JogoService.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JogoService.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class EmpresaFornecedoraController : ControllerBase
    {
        private readonly IEmpresaFornecedoraService _empresaFornecedoraService;

        public EmpresaFornecedoraController(IEmpresaFornecedoraService empresaFornecedoraService)
        {
            _empresaFornecedoraService = empresaFornecedoraService;
        }

        [HttpGet]
        public async Task<IActionResult> BuscarTodos()
        {
            var empresasFornecedoras = await _empresaFornecedoraService.BuscarTodosAsync();

            return Ok(empresasFornecedoras);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> BuscarPorId(long id)
        {
            var empresaFornecedora = await _empresaFornecedoraService.BuscarPorIdAsync(id);

            if (empresaFornecedora == null)
                return NotFound();

            return Ok(empresaFornecedora);
        }

        [Authorize(Roles = "Administrador")]
        [HttpPost]
        public async Task<IActionResult> Cadastrar(
            [FromBody] EmpresaFornecedoraRequest request)
        {
            var empresaFornecedora = await _empresaFornecedoraService.CadastrarAsync(request);

            return CreatedAtAction(
                nameof(BuscarPorId),
                new { id = empresaFornecedora.Id },
                empresaFornecedora);
        }

        [Authorize(Roles = "Administrador")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(
            long id,
            [FromBody] EmpresaFornecedoraRequest request)
        {
            var empresaFornecedora = await _empresaFornecedoraService.AtualizarAsync(id, request);

            if (empresaFornecedora  == null)
                return NotFound();

            return Ok(empresaFornecedora);
        }

        [Authorize(Roles = "Administrador")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Excluir(long id)
        {
            var removida = await _empresaFornecedoraService.ExcluirAsync(id);

            if (!removida)
                return NotFound();

            return NoContent();
        }
    }
}
