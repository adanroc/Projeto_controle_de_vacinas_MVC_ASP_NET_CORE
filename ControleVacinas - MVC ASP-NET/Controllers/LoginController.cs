using ControleVacinas___MVC_ASP_NET.Data;
using ControleVacinas___MVC_ASP_NET.Helper;
using ControleVacinas___MVC_ASP_NET.Models;
using ControleVacinas___MVC_ASP_NET.Repositorio;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ControleVacinas___MVC_ASP_NET.Controllers
{
    public class LoginController : Controller
    {
        private readonly IListaDeUsuariosRepositorio _listaDeUsuariosRepositorio;
        private readonly ISessao _sessao;
        private readonly IEmail _email;

        public LoginController(IListaDeUsuariosRepositorio listaDeUsuariosRepositorio,
                               ISessao sessao,
                               IEmail email)
        {
            _listaDeUsuariosRepositorio = listaDeUsuariosRepositorio;
            _sessao = sessao;
            _email = email; 
        }
        public IActionResult Index()
        {
            // Se o usuário estiver logado, redirecionar para a Home
            if (_sessao.BuscarSessaoDoUsuario() != null) return RedirectToAction("Index","Home"); 
            return View();
        }

        public IActionResult RedefinirSenha()
        {
            return View();
        }

        public IActionResult Sair()
        {
            // Remova a sessão do usuário
            _sessao.RemoverSessaoDoUsuario();

            // Adicione a flag para identificar que foi feito logout
            HttpContext.Session.SetString("UsuarioDeslogado", "true");

            return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public IActionResult Entrar(LoginModel loginModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ListaDeUsuariosModel usuario = _listaDeUsuariosRepositorio.BuscarPorLogin(loginModel.Login);
                    if (usuario != null)
                    {
                        if(usuario.SenhaValida(loginModel.Senha))
                        {
                            _sessao.CriarSessaoDoUsuario(usuario); 
                            return RedirectToAction("Index", "Home");
                        }
                        TempData["MensagemErro"] = $"Senha do usuário inválida. Tente novamente.";
                    }
                    TempData["MensagemErro"] = $"Usuário e/ou senha inválido(s). Tente novamente.";
                }
                return View("Index");
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não foi possível realizar seu login, tente novamente! Erro:{erro.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpPost] 
        public IActionResult EnviarLinkParaRedefinirSenha(RedefinirSenhaModel redefinirSenhaModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ListaDeUsuariosModel usuario = _listaDeUsuariosRepositorio.BuscarPorEmailELogin(redefinirSenhaModel.Email, redefinirSenhaModel.Login);

                    if (usuario != null)
                    {
                        string novaSenha = usuario.GerarNovaSenha();                        
                        string mensagem = $"Sua nova senha é: {novaSenha}";

                        bool emailEnviado = _email.Enviar(usuario.Email, "Sistema de Vacinação dos Servidores - Nova Senha", mensagem);

                        if(emailEnviado)
                        {
                            _listaDeUsuariosRepositorio.Atualizar(usuario);
                            TempData["MensagemSucesso"] = $"Enviamos para seu e-mail cadastrado uma nova senha.";
                        } else {
                            TempData["MensagemErro"] = $"Não foi possível enviar o e-mail para trocar sua senha. Por favor, tente novamente.";
                        }
                        return RedirectToAction("Index", "Login");
                    }
                    TempData["MensagemErro"] = $"Não foi possível redefinir sua senha.Por favor, verifique os dados informados e tente novamente.";
                }
                return View("Index");
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não foi possível redefinir sua senha, tente novamente! Erro:{erro.Message}";
                return RedirectToAction("Index");
            }
        }
    }
}


