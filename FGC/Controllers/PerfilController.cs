using Microsoft.AspNetCore.Mvc;
using UsuarioService.Requests;
using UsuarioService.Services.Interfaces;

namespace UsuarioService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PerfilController : ControllerBase
    {
        private readonly IPerfilService _perfilService;

        public PerfilController(IPerfilService perfilService)
        {
            _perfilService = perfilService;
        }

        [HttpGet]
        public async Task<IActionResult> BuscarTodos()
        {
            var perfis = await _perfilService.BuscarTodosAsync();
            return Ok(perfis);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> BuscarPorId(long id)
        {
            var perfil = await _perfilService.BuscarPorIdAsync(id);

            if (perfil == null)
                return NotFound();

            return Ok(perfil);
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar([FromBody] PerfilRequest request)
        {
            var perfil = await _perfilService.CadastrarAsync(request);

            return CreatedAtAction(nameof(BuscarPorId), new { id = perfil.Id }, perfil);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(long id, [FromBody] PerfilRequest request)
        {
            var perfil = await _perfilService.AtualizarAsync(id, request);

            if (perfil == null)
                return NotFound();

            return Ok(perfil);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Excluir(long id)
        {
            var removido = await _perfilService.ExcluirAsync(id);

            if (!removido)
                return NotFound();

            return NoContent();
        }
    }
}