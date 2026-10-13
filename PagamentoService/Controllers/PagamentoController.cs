using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PagamentoService.Requests;
using PagamentoService.Services.Interfaces;

namespace PagamentoService.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class PagamentoController : ControllerBase
    {
        private readonly IPagamentoService _pagamentoService;

        public PagamentoController(IPagamentoService pagamentoService)
        {
            _pagamentoService = pagamentoService;
        }

        [HttpGet]
        public async Task<IActionResult> BuscarTodos()
        {
            var pagamentos = await _pagamentoService.BuscarTodosAsync();

            return Ok(pagamentos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> BuscarPorId(long id)
        {
            var pagamento = await _pagamentoService.BuscarPorIdAsync(id);

            if (pagamento == null)
                return NotFound();

            return Ok(pagamento);
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar(
            [FromBody] PagamentoRequest request)
        {
            var pagamento = await _pagamentoService.CadastrarAsync(request);

            return CreatedAtAction(
                nameof(BuscarPorId),
                new { id = pagamento.Id },
                pagamento);
        }

        [Authorize(Roles = "Administrador")]
        [HttpPatch("{id}/status")]
        public async Task<IActionResult> AtualizarStatus(
            long id,
            [FromBody] string status)
        {
            var pagamento =
                await _pagamentoService.AtualizarStatusAsync(id, status);

            if (pagamento == null)
                return NotFound();

            return Ok(pagamento);
        }

        [Authorize(Roles = "Administrador")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Excluir(long id)
        {
            var removido = await _pagamentoService.ExcluirAsync(id);

            if (!removido)
                return NotFound();

            return NoContent();
        }
    }
}