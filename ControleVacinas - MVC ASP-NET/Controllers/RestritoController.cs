using ControleVacinas___MVC_ASP_NET.Filters;
using Microsoft.AspNetCore.Mvc;

namespace ControleVacinas___MVC_ASP_NET.Controllers
{
    [PaginaParaUsuarioLogado]
    public class RestritoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
