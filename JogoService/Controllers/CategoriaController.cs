using JogoService.Requests;
using JogoService.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JogoService.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaService _categoriaService;

        public CategoriaController(ICategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }

        [HttpGet]
        public async Task<IActionResult> BuscarTodos()
        {
            var categorias = await _categoriaService.BuscarTodosAsync();

            return Ok(categorias);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> BuscarPorId(long id)
        {
            var categoria = await _categoriaService.BuscarPorIdAsync(id);

            if (categoria == null)
                return NotFound();

            return Ok(categoria);
        }

        [Authorize(Roles = "Administrador")]
        [HttpPost]
        public async Task<IActionResult> Cadastrar(
            [FromBody] CategoriaRequest request)
        {
            var categoria = await _categoriaService.CadastrarAsync(request);

            return CreatedAtAction(
                nameof(BuscarPorId),
                new { id = categoria.Id },
                categoria);
        }

        [Authorize(Roles = "Administrador")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(
            long id,
            [FromBody] CategoriaRequest request)
        {
            var categoria = await _categoriaService.AtualizarAsync(id, request);

            if (categoria == null)
                return NotFound();

            return Ok(categoria);
        }

        [Authorize(Roles = "Administrador")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Excluir(long id)
        {
            var removida = await _categoriaService.ExcluirAsync(id);

            if (!removida)
                return NotFound();

            return NoContent();
        }
    }
}