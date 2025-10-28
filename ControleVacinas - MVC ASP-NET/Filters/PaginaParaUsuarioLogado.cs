using ControleVacinas___MVC_ASP_NET.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace ControleVacinas___MVC_ASP_NET.Filters
{
    public class PaginaParaUsuarioLogado : ActionFilterAttribute
    {
      public override void OnActionExecuted(ActionExecutedContext context)
        {
            // Verifica se o usuário acabou de fazer logout
            string usuarioDeslogado = context.HttpContext.Session.GetString("UsuarioDeslogado");

            // Se o usuário deslogado está identificado, impede o uso do botão "Voltar"
            if (!string.IsNullOrEmpty(usuarioDeslogado) && usuarioDeslogado == "true")
            {
                // Limpa a flag de deslogado para evitar que ela persista em sessões futuras
                context.HttpContext.Session.Remove("UsuarioDeslogado");

                // Adiciona cabeçalhos para evitar cache da página
                context.HttpContext.Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
                context.HttpContext.Response.Headers["Pragma"] = "no-cache";
                context.HttpContext.Response.Headers["Expires"] = "0";

                // Redireciona para a tela de login
                context.Result = new RedirectToRouteResult(new RouteValueDictionary
                {
                    { "controller", "Login" },
                    { "action", "Index" }
                });
                return; // Impede que o filtro continue, já que o redirecionamento já foi feito
            }

            string sessaoUsuario = context.HttpContext.Session.GetString("sessaoUsuarioLogado");

            // Verifique se a sessão do usuário está vazia ou inválida
            if (string.IsNullOrEmpty(sessaoUsuario))
            {
                // (pode causar lentidão)
                // Adiciona cabeçalhos para evitar cache da página (impede de usar o sistema deslogado usando botão de voltar do navegador)
                context.HttpContext.Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
                context.HttpContext.Response.Headers["Pragma"] = "no-cache";
                context.HttpContext.Response.Headers["Expires"] = "0";

                context.Result = new RedirectToRouteResult(new RouteValueDictionary
                {
                    { "controller", "Login" },
                    {"action","Index"}
                });
            }
            else
            {
                // (pode causar lentidão)
                // Adiciona cabeçalhos para evitar cache da página (impede de usar o sistema deslogado usando botão de voltar do navegador)
                //context.HttpContext.Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
                //context.HttpContext.Response.Headers["Pragma"] = "no-cache";
                //context.HttpContext.Response.Headers["Expires"] = "0";

                // Deserializar o objeto de sessão
                ListaDeUsuariosModel usuario = JsonConvert.DeserializeObject<ListaDeUsuariosModel>(sessaoUsuario);
                if (usuario == null)
                {
                    context.Result = new RedirectToRouteResult(new RouteValueDictionary
                    {
                        { "controller", "Login" },
                        { "action", "Index" }
                    });
                }
            }

            base.OnActionExecuted(context);
        }
    }
}
