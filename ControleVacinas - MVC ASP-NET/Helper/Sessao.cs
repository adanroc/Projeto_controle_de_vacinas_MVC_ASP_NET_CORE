using ControleVacinas___MVC_ASP_NET.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace ControleVacinas___MVC_ASP_NET.Helper
{
    public class Sessao : ISessao
    {
        private readonly IHttpContextAccessor _httpContext;

        public Sessao (IHttpContextAccessor httpContextAcessor)
        {
            _httpContext = httpContextAcessor;
        }

        public ListaDeUsuariosModel BuscarSessaoDoUsuario()
        {
            string sessaoUsuarioLogadoEncontrado = _httpContext.HttpContext.Session.GetString("sessaoUsuarioLogado");
            if (string.IsNullOrEmpty(sessaoUsuarioLogadoEncontrado)) return null;
            return JsonConvert.DeserializeObject<ListaDeUsuariosModel>(sessaoUsuarioLogadoEncontrado);
        }

        public void CriarSessaoDoUsuario(ListaDeUsuariosModel usuario)
        {
            string usuarioSerializado = JsonConvert.SerializeObject(usuario);
            _httpContext.HttpContext.Session.SetString("sessaoUsuarioLogado", usuarioSerializado);
        }

        public void RemoverSessaoDoUsuario()
        {
            _httpContext.HttpContext.Session.Remove("sessaoUsuarioLogado");
        }
    }
}

