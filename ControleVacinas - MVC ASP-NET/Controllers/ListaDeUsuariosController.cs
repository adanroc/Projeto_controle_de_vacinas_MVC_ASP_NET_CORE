using ControleVacinas___MVC_ASP_NET.Filters;
using ControleVacinas___MVC_ASP_NET.Models;
using ControleVacinas___MVC_ASP_NET.Repositorio;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ControleVacinas___MVC_ASP_NET.Controllers
{
    [PaginaRestritaSomenteAdmin]
    public class ListaDeUsuariosController : Controller
    {
        private readonly IListaDeUsuariosRepositorio _listaDeUsuariosRepositorio;
        private readonly IListaDeCadastrosRepositorio _listaDeCadastrosRepositorio; 
        public ListaDeUsuariosController(IListaDeUsuariosRepositorio listaDeUsuariosRepositorio,
                                         IListaDeCadastrosRepositorio listaDeCadastrosRepositorio)
        {
            _listaDeUsuariosRepositorio = listaDeUsuariosRepositorio;
            _listaDeCadastrosRepositorio = listaDeCadastrosRepositorio;
        }

        public IActionResult Index()
        {
            List<ListaDeUsuariosModel> listaDeUsuarios = _listaDeUsuariosRepositorio.BuscarTodos();
            return View(listaDeUsuarios);
        }

        public IActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Cadastrar(ListaDeUsuariosModel listaDeUsuariosModel)
        {
            try
            {
                //Debug                 
                if (ModelState.IsValid)
                {
                    listaDeUsuariosModel = _listaDeUsuariosRepositorio.Adicionar(listaDeUsuariosModel);                                                                         

                    TempData["MensagemSucesso"] = "Cadastro realizado com sucesso!";
                    return RedirectToAction("Index");
                } else {
                    foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                    {
                        Console.WriteLine($"Erro: {error.ErrorMessage}");
                    }
                    return View(listaDeUsuariosModel);
                }
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não foi possível realizar o cadastro, tente novamente! Erro:{erro.Message}";
                return RedirectToAction("Index");
            }
        }

        public IActionResult ListarCadastrosPorUsuarioId(int id)
        {
            //List<ListaDeCadastrosModel> cadastros = _listaDeCadastrosRepositorio.BuscarTodosOsCadastrosDoUsuario(id);
            List<ListaDeCadastrosModel> cadastros = _listaDeCadastrosRepositorio.BuscarTodos(id);
            return PartialView("_CadastrosUsuario", cadastros);
        }

        public IActionResult Apagar(int id)
        {
            try
            {
                bool apagado = _listaDeUsuariosRepositorio.Apagar(id);
                if (apagado)
                {
                    TempData["MensagemSucesso"] = "Usuario apagado com sucesso!";
                }
                else
                {
                    TempData["MensagemErro"] = $"Ops, não foi possível apagar o usuário, tente novamente!";
                }

                return RedirectToAction("Index");
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não foi possível apagar o usuário, tente novamente! Erro:{erro.Message}";
                return RedirectToAction("Index");

            }
        }

        public IActionResult ApagarConfirmacao(int id)
        {
            ListaDeUsuariosModel listaDeUsuariosModel = _listaDeUsuariosRepositorio.ListarPorId(id);
            return View(listaDeUsuariosModel);
        }

        public IActionResult Editar(int id)
        {
            ListaDeUsuariosModel listaDeUsuariosModel = _listaDeUsuariosRepositorio.ListarPorId(id);
            return View(listaDeUsuariosModel);
        }

        [HttpPost]
        public IActionResult Editar(ListaDeUsuariosSemSenhaModel listaDeUsuariosSemSenhaModel)
        {
            try
            {
                ListaDeUsuariosModel usuario = null;

                if (ModelState.IsValid)
                {
                    usuario = new ListaDeUsuariosModel()
                    {
                        Id = listaDeUsuariosSemSenhaModel.Id,
                        Nome = listaDeUsuariosSemSenhaModel.Nome,
                        Login = listaDeUsuariosSemSenhaModel.Login,
                        Email = listaDeUsuariosSemSenhaModel.Email,
                        Perfil = listaDeUsuariosSemSenhaModel.Perfil
                    };

                    usuario = _listaDeUsuariosRepositorio.Atualizar(usuario);
                    TempData["MensagemSucesso"] = "Usuário alterado com sucesso!";
                    return RedirectToAction("Index");
                }
                return View(usuario);
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não foi possível atualizar o usuário, tente novamente! Erro:{erro.Message}";
                return RedirectToAction("Index");
            }
        }
    }
}
