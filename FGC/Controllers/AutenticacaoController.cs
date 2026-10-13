using Microsoft.AspNetCore.Mvc;

namespace UsuarioService.Controllers
{
    public class AutenticacaoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
