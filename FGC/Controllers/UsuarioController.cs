using Microsoft.AspNetCore.Mvc;
using UsuarioService.Requests;
using UsuarioService.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace UsuarioService.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet]
        public async Task<IActionResult> BuscarTodos()
        {
            var usuarios = await _usuarioService.BuscarTodosAsync();

            return Ok(usuarios);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> BuscarPorId(long id)
        {
            var usuario = await _usuarioService.BuscarPorIdAsync(id);

            if (usuario == null)
                return NotFound();

            return Ok(usuario);
        }

        
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Cadastrar([FromBody] UsuarioRequest request)
        {
            var usuario = await _usuarioService.CadastrarAsync(request);

            return CreatedAtAction(nameof(BuscarPorId), new { id = usuario.Id }, usuario);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(long id, UsuarioRequest request)
        {
            var usuario = await _usuarioService.AtualizarAsync(id, request);

            if (usuario == null)
                return NotFound();

            return Ok(usuario);
        }
        [Authorize(Roles = "Administrador")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Excluir(long id)
        {
            var removido = await _usuarioService.ExcluirAsync(id);

            if (!removido)
                return NotFound();

            return NoContent();
        }
    }
}