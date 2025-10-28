using ControleVacinas___MVC_ASP_NET.Controllers;
using ControleVacinas___MVC_ASP_NET.Data;
using ControleVacinas___MVC_ASP_NET.Filters;
using ControleVacinas___MVC_ASP_NET.Helper;
using ControleVacinas___MVC_ASP_NET.Models;
using ControleVacinas___MVC_ASP_NET.Repositorio;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.Runtime.ConstrainedExecution;

namespace ControleVacinas___MVC_ASP_NET.Controllers
{
    [PaginaParaUsuarioLogado]
    public class HomeController : Controller
    {
        private readonly ISessao _sessao;
        private readonly IListaDeUsuariosRepositorio _listaDeUsuariosRepositorio;
        private readonly IListaDeCadastrosRepositorio _listaDeCadastrosRepositorio;

        public HomeController(ISessao sessao, IListaDeUsuariosRepositorio listaDeUsuariosRepositorio, IListaDeCadastrosRepositorio listaDeCadastrosRepositorio)
        {
            _sessao = sessao;
            _listaDeUsuariosRepositorio = listaDeUsuariosRepositorio;
            _listaDeCadastrosRepositorio = listaDeCadastrosRepositorio;
        }

        public IActionResult Index()
        {
            // Tenta obter o usuário logado da sessão
            ListaDeUsuariosModel usuarioLogado = _sessao.BuscarSessaoDoUsuario();

            if (usuarioLogado != null)
            {
                // Busca informações atualizadas no banco, se necessário
                var usuarioAtualizado = _listaDeUsuariosRepositorio.ListarPorId(usuarioLogado.Id);

                ViewBag.NomeUsuario = usuarioAtualizado.Nome;
                //TempData["MensagemSucesso"] = $"Bem Vindo, {ViewBag.NomeUsuario}, ao SCVS.";

            }
            else
            {
                ViewBag.NomeUsuario = "Usuário Desconhecido";
            }            

            //// Calcular os  totais do banco de dados ao acessar uma primeira vez o sistema antes de acessar a index de lista de cadastros
            //// Calcular os totais em relação aos servidores
            //var balanco = _listaDeCadastrosRepositorio.CalcularBalancoDeVacinacaoDosServidores();

            //// Armazenar os valores no ViewBag
            //ViewBag.totalServidoresCadastrados = balanco.totalServidoresCadastrados;
            //ViewBag.totalVacinadosCompletamente = balanco.totalVacinadosCompletamente;
            //ViewBag.totalVacinadosParcialmente = balanco.totalVacinadosParcialmente;
            //ViewBag.totalNaoVacinados = balanco.totalNaoVacinados;

            //// Calcular os totais de cada dose de todas as vacinas (todo o banco)
            //var totaisPorDose = _listaDeCadastrosRepositorio.CalcularTotaisPorDose();

            //// Armazenar os valores no ViewBag
            //ViewBag.TotalDose1HepatiteB = totaisPorDose.TotalDose1HepatiteB;
            //ViewBag.TotalDose2HepatiteB = totaisPorDose.TotalDose2HepatiteB;
            //ViewBag.TotalDose3HepatiteB = totaisPorDose.TotalDose3HepatiteB;
            //ViewBag.TotalReforcoHepatiteB = totaisPorDose.TotalReforcoHepatiteB;

            //ViewBag.TotalExameAntiHBS = totaisPorDose.TotalExameAntiHBS;
            //ViewBag.TotalResultadoAntiHBS = totaisPorDose.TotalResultadoAntiHBS;

            //ViewBag.TotalDose1DifteriaTetano = totaisPorDose.TotalDose1DifteriaTetano;
            //ViewBag.TotalDose2DifteriaTetano = totaisPorDose.TotalDose2DifteriaTetano;
            //ViewBag.TotalDose3DifteriaTetano = totaisPorDose.TotalDose3DifteriaTetano;
            //ViewBag.TotalReforcoDifteriaTetano = totaisPorDose.TotalReforcoDifteriaTetano;

            //ViewBag.TotalDose1TripliceViral = totaisPorDose.TotalDose1TripliceViral;
            //ViewBag.TotalDose2TripliceViral = totaisPorDose.TotalDose2TripliceViral;

            //ViewBag.TotalDose1Covid = totaisPorDose.TotalDose1Covid;
            //ViewBag.TotalDose2Covid = totaisPorDose.TotalDose2Covid;
            //ViewBag.TotalDose3Covid = totaisPorDose.TotalDose3Covid;
            //ViewBag.TotalReforco1Covid = totaisPorDose.TotalReforco1Covid;
            //ViewBag.TotalReforco2Covid = totaisPorDose.TotalReforco2Covid;

            //ViewBag.TotalDoseUnicaFebreAmarela = totaisPorDose.TotalDoseUnicaFebreAmarela;
            //ViewBag.TotalDoseAnualInfluenza = totaisPorDose.TotalDoseAnualInfluenza;

            //// Calcular os totais de cada tipo de vacina (todo o banco)
            //var totaisPorVacina = _listaDeCadastrosRepositorio.CalcularTotaisPorVacina();

            //ViewBag.TotalHepatiteB = totaisPorVacina.TotalHepatiteB;
            //ViewBag.TotalExameAntiHBS = totaisPorVacina.TotalExameAntiHBS;
            //ViewBag.TotalDifteriaTetano = totaisPorVacina.TotalDifteriaTetano;
            //ViewBag.TotalTripliceViral = totaisPorVacina.TotalTripliceViral;
            //ViewBag.TotalCovid = totaisPorVacina.TotalCovid;
            //ViewBag.TotalFebreAmarela = totaisPorVacina.TotalFebreAmarela;
            //ViewBag.TotalInfluenza = totaisPorVacina.TotalInfluenza;

            //return View();

            // Redireciona para o Index do ListaDeCadastrosController
            return RedirectToAction("Index", "ListaDeCadastros");

            //Caso Precise Compartilhar Dados no Redirecionamento: Se há informações calculadas no HomeController que precisam ser exibidas na view do ListaDeCadastrosController, você pode usar o TempData para transferir esses dados:

            //TempData["TotalServidoresCadastrados"] = balanco.totalServidoresCadastrados;
            // Outros dados...
            //return RedirectToAction("Index", "ListaDeCadastros");

            //No ListaDeCadastrosController, recupere os dados com TempData:
            //ViewBag.TotalServidoresCadastrados = TempData["TotalServidoresCadastrados"];
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
