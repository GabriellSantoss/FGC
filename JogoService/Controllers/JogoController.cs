using JogoService.Requests;
using JogoService.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JogoService.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class JogoController : ControllerBase
    {
        private readonly IJogoService _jogoService;

        public JogoController(IJogoService jogoService)
        {
            _jogoService = jogoService;
        }

        [HttpGet]
        public async Task<IActionResult> BuscarTodos()
        {
            var jogos = await _jogoService.BuscarTodosAsync();

            return Ok(jogos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> BuscarPorId(long id)
        {
            var jogo = await _jogoService.BuscarPorIdAsync(id);

            if (jogo == null)
                return NotFound();

            return Ok(jogo);
        }

        [Authorize(Roles = "Administrador")]
        [HttpPost]
        public async Task<IActionResult> Cadastrar(
            [FromBody] JogoRequest request)
        {
            var jogo = await _jogoService.CadastrarAsync(request);

            return CreatedAtAction(
                nameof(BuscarPorId),
                new { id = jogo.Id },
                jogo);
        }

        [Authorize(Roles = "Administrador")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(
            long id,
            [FromBody] JogoRequest request)
        {
            var jogo = await _jogoService.AtualizarAsync(id, request);

            if (jogo == null)
                return NotFound();

            return Ok(jogo);
        }

        [Authorize(Roles = "Administrador")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Excluir(long id)
        {
            var removido = await _jogoService.ExcluirAsync(id);

            if (!removido)
                return NotFound();

            return NoContent();
        }
    }
}