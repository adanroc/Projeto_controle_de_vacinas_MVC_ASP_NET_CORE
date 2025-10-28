using ControleVacinas___MVC_ASP_NET.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ControleVacinas___MVC_ASP_NET.ViewComponents
{
    public class Menu : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            string sessaoUsuario = HttpContext.Session.GetString("sessaoUsuarioLogado");
            if (string.IsNullOrEmpty(sessaoUsuario)) return null;
            ListaDeUsuariosModel usuario = JsonConvert.DeserializeObject<ListaDeUsuariosModel>(sessaoUsuario);
            return View(usuario);
        } 
    }
}
