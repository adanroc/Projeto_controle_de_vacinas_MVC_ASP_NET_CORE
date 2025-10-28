using ControleVacinas___MVC_ASP_NET.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace ControleVacinas___MVC_ASP_NET.Filters
{
    public class PaginaRestritaSomenteAdmin : ActionFilterAttribute
    {
      public override void OnActionExecuted(ActionExecutedContext context)
        {
            string sessaoUsuario = context.HttpContext.Session.GetString("sessaoUsuarioLogado");
            if(string.IsNullOrEmpty(sessaoUsuario)) 
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Login" }, {"action","Index"} });
            } else
            {
                ListaDeUsuariosModel usuario = JsonConvert.DeserializeObject<ListaDeUsuariosModel>(sessaoUsuario);
                if (usuario == null)
                {
                    context.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Login" }, { "action", "Index" } });
                }

                if(usuario.Perfil != Enums.PerfilEnum.Admin)
                {
                    context.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Restrito" }, { "action", "Index" } });
                }
            }

            base.OnActionExecuted(context);
        }
    }
}
