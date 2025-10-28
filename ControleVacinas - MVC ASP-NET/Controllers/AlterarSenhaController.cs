using ControleVacinas___MVC_ASP_NET.Helper;
using ControleVacinas___MVC_ASP_NET.Models;
using ControleVacinas___MVC_ASP_NET.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace ControleVacinas___MVC_ASP_NET.Controllers
{
    public class AlterarSenhaController : Controller
    {
        private readonly IListaDeUsuariosRepositorio _listaDeUsuariosRepositorio;
        private readonly ISessao _sessao;

        public AlterarSenhaController(IListaDeUsuariosRepositorio listaDeUsuariosRepositorio,
                                      ISessao sessao)
        {
            _listaDeUsuariosRepositorio = listaDeUsuariosRepositorio;
            _sessao = sessao;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Alterar(AlterarSenhaModel alterarSenhaModel)
        {
            try
            {
                ListaDeUsuariosModel usuarioLogado = _sessao.BuscarSessaoDoUsuario();
                alterarSenhaModel.Id = usuarioLogado.Id;

                if (ModelState.IsValid)
                {
                    _listaDeUsuariosRepositorio.AlterarSenha(alterarSenhaModel);
                    TempData["MensagemSucesso"] = "Senha alterada com sucesso!";
                    return View("Index", alterarSenhaModel);
                }
                return View("Index", alterarSenhaModel);

            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não foi possível alterar sua senha, tente novamente. Erro: {erro.Message}";
                return View("Index", alterarSenhaModel);
            }
        }
    }
}
