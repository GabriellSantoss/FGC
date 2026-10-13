using JogoService.Entidades.Requests;
using JogoService.Requests;
using JogoService.Services;
using JogoService.Services.Implementations;
using JogoService.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace JogoService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AvaliacaoController : ControllerBase
    {
        private readonly IAvaliacaoService _avaliacaoService;

        public AvaliacaoController(IAvaliacaoService avaliacaoService)
        {
            _avaliacaoService = avaliacaoService;
        }

        [HttpGet]
        public async Task<IActionResult> BuscarTodos()
        {
            var avaliacoes = await _avaliacaoService.BuscarTodosAsync();

            return Ok(avaliacoes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> BuscarPorId(long id)
        {
            var avaliacao = await _avaliacaoService.BuscarPorIdAsync(id);

            if (avaliacao == null)
                return NotFound();

            return Ok(avaliacao);
        }
        [HttpPost]
        public async Task<IActionResult> Cadastrar(
        [FromBody] AvaliacaoRequest request)
        {
            var avaliacao = await _avaliacaoService.CadastrarAsync(request);

            return CreatedAtAction(
       nameof(BuscarPorId),
       new { id = avaliacao.Id },
       avaliacao);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(
            long id,
            [FromBody] AvaliacaoRequest request)
        {
            var avaliacao = await _avaliacaoService.AtualizarAsync(id, request);

            if (avaliacao == null)
                return NotFound();

            return Ok(avaliacao);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Excluir(long id)
        {
            var removida = await _avaliacaoService.ExcluirAsync(id);

            if (!removida)
                return NotFound();

            return NoContent();
        }
    }
}
