using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PagamentoService.Requests;
using PagamentoService.Services.Interfaces;

namespace PagamentoService.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CompraController : ControllerBase
    {
        private readonly ICompraService _compraService;

        public CompraController(ICompraService compraService)
        {
            _compraService = compraService;
        }

        [HttpGet]
        public async Task<IActionResult> BuscarTodos()
        {
            var compras = await _compraService.BuscarTodosAsync();

            return Ok(compras);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> BuscarPorId(long id)
        {
            var compra = await _compraService.BuscarPorIdAsync(id);

            if (compra == null)
                return NotFound();

            return Ok(compra);
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar(
            [FromBody] CompraRequest request)
        {
            var compra = await _compraService.CadastrarAsync(request);

            return CreatedAtAction(
                nameof(BuscarPorId),
                new { id = compra.Id },
                compra);
        }

        [Authorize(Roles = "Administrador")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(
            long id,
            [FromBody] CompraRequest request)
        {
            var compra = await _compraService.AtualizarAsync(id, request);

            if (compra == null)
                return NotFound();

            return Ok(compra);
        }

        [Authorize(Roles = "Administrador")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Excluir(long id)
        {
            var removida = await _compraService.ExcluirAsync(id);

            if (!removida)
                return NotFound();

            return NoContent();
        }

        [HttpGet("usuario/{usuarioId:guid}")]
        public async Task<IActionResult> BuscarPorUsuario(Guid usuarioId)
        {
            var compras =
                await _compraService.BuscarPorUsuarioAsync(usuarioId);

            return Ok(compras);
        }
    }
}