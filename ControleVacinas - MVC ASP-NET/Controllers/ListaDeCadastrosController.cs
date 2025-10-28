using ControleVacinas___MVC_ASP_NET.Filters;
using ControleVacinas___MVC_ASP_NET.Helper;
using ControleVacinas___MVC_ASP_NET.Models;
using ControleVacinas___MVC_ASP_NET.Repositorio;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using static iTextSharp.text.pdf.events.IndexEvents;
using System.Net;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Reflection.Metadata;
using Document = iTextSharp.text.Document;
using System.Runtime.Intrinsics.X86;
using System.IO;
using System.Xml.Linq;
using static ControleVacinas___MVC_ASP_NET.Repositorio.ListaDeCadastrosRepositorio;
using iTextSharp.xmp.impl;
using Org.BouncyCastle.Pqc.Crypto.Lms;
using iTextSharp.text.pdf.codec.wmf;
using iTextSharp.text.pdf.hyphenation;
using System.Text;

namespace ControleVacinas___MVC_ASP_NET.Controllers
{
    [PaginaParaUsuarioLogado]
    public class ListaDeCadastrosController : Controller
    {
        private readonly IListaDeCadastrosRepositorio _listaDeCadastrosRepositorio;
        private readonly ISessao _sessao;
        public ListaDeCadastrosController(IListaDeCadastrosRepositorio listaDeCadastrosRepositorio,
                                          ISessao sessao)
        {
            _listaDeCadastrosRepositorio = listaDeCadastrosRepositorio;
            _sessao = sessao;
        }

        public IActionResult Index()
        {
            _listaDeCadastrosRepositorio.CalcularTotalTiposDeVacinasTodosCadastros();

            _listaDeCadastrosRepositorio.CalcularTotalDosesTodosCadastros();

            //// Inicializa o valor do Enum (SituacaoVacinacao)
            //var situacaoVacinacao = ControleVacinas___MVC_ASP_NET.Enums.SituacaoVacinacaoEnum.VacinacaoCompleta;

            //// Armazenar o valor do Enum no ViewBag
            //ViewBag.SituacaoVacinacao = situacaoVacinacao;


            // Calcular os totais em relação aos servidores
            var balanco = _listaDeCadastrosRepositorio.CalcularBalancoDeVacinacaoDosServidores();

            //// Armazenar os valores no ViewBag            
            //ViewBag.totalServidoresCadastrados = balanco.totalServidoresCadastrados;
            //ViewBag.totalVacinadosCompletamente = balanco.totalVacinadosCompletamente;
            //ViewBag.totalVacinadosParcialmente = balanco.totalVacinadosParcialmente;
            //ViewBag.totalNaoVacinados = balanco.totalNaoVacinados;

            // Verificar se está vazio ou nulo e atribuir null se for o caso
            ViewBag.totalServidoresCadastrados = balanco?.totalServidoresCadastrados ?? (int?)null;
            ViewBag.totalVacinadosCompletamente = balanco?.totalVacinadosCompletamente ?? (int?)null;
            ViewBag.totalVacinadosParcialmente = balanco?.totalVacinadosParcialmente ?? (int?)null;
            ViewBag.totalNaoVacinados = balanco?.totalNaoVacinados ?? (int?)null;


            //try
            //{
            //    // Carregar os dados para a ViewBag
            //    CarregarBalancoVacinacao();

            //    // Aqui você pode passar outros dados necessários para a View
            //    return View();
            //}
            //catch (Exception erro)
            //{
            //    TempData["MensagemErro"] = $"Ops, ocorreu um erro: {erro.Message}";
            //    return RedirectToAction("Index");
            //}


            // Calcular os totais de cada dose de todas as vacinas (todo o banco)
            var totaisPorDose = _listaDeCadastrosRepositorio.CalcularTotaisPorDose();

            // Armazenar os valores no ViewBag
            ViewBag.TotalDose1HepatiteB = totaisPorDose.TotalDose1HepatiteB;
            ViewBag.TotalDose2HepatiteB = totaisPorDose.TotalDose2HepatiteB;
            ViewBag.TotalDose3HepatiteB = totaisPorDose.TotalDose3HepatiteB;
            ViewBag.TotalReforcoHepatiteB = totaisPorDose.TotalReforcoHepatiteB;

            ViewBag.TotalExameAntiHBS = totaisPorDose.TotalExameAntiHBS;
            ViewBag.TotalResultadoAntiHBS = totaisPorDose.TotalResultadoAntiHBS;

            ViewBag.TotalDose1DifteriaTetano = totaisPorDose.TotalDose1DifteriaTetano;
            ViewBag.TotalDose2DifteriaTetano = totaisPorDose.TotalDose2DifteriaTetano;
            ViewBag.TotalDose3DifteriaTetano = totaisPorDose.TotalDose3DifteriaTetano;
            ViewBag.TotalReforcoDifteriaTetano = totaisPorDose.TotalReforcoDifteriaTetano;

            ViewBag.TotalDose1TripliceViral = totaisPorDose.TotalDose1TripliceViral;
            ViewBag.TotalDose2TripliceViral = totaisPorDose.TotalDose2TripliceViral;

            ViewBag.TotalDose1Covid = totaisPorDose.TotalDose1Covid;
            ViewBag.TotalDose2Covid = totaisPorDose.TotalDose2Covid;
            ViewBag.TotalDose3Covid = totaisPorDose.TotalDose3Covid;
            ViewBag.TotalReforco1Covid = totaisPorDose.TotalReforco1Covid;
            ViewBag.TotalReforco2Covid = totaisPorDose.TotalReforco2Covid;

            ViewBag.TotalDoseUnicaFebreAmarela = totaisPorDose.TotalDoseUnicaFebreAmarela;
            ViewBag.TotalDoseAnualInfluenza = totaisPorDose.TotalDoseAnualInfluenza;

            // Calcular os totais de cada tipo de vacina (todo o banco)
            var totaisPorVacina = _listaDeCadastrosRepositorio.CalcularTotaisPorVacina();

            ViewBag.TotalHepatiteB = totaisPorVacina.TotalHepatiteB;
            ViewBag.TotalExameAntiHBS = totaisPorVacina.TotalExameAntiHBS;
            ViewBag.TotalDifteriaTetano = totaisPorVacina.TotalDifteriaTetano;
            ViewBag.TotalTripliceViral = totaisPorVacina.TotalTripliceViral;
            ViewBag.TotalCovid = totaisPorVacina.TotalCovid;
            ViewBag.TotalFebreAmarela = totaisPorVacina.TotalFebreAmarela;
            ViewBag.TotalInfluenza = totaisPorVacina.TotalInfluenza;


            // Obter a menor e maior data de cada coluna de datas no banco
            //var datasMinMax = _listaDeCadastrosRepositorio.ObterMinMaxDatas();
            //ViewBag.datasMinMax = datasMinMax;

            // Obter as datas mínimas e máximas do repositório
            var datasMinMax = _listaDeCadastrosRepositorio.ObterMinMaxDatas();

            // Converter as datas para o formato "yyyy-MM-dd"
            var datasMinMaxFormatadas = new
            {
                MinDataNascimento = datasMinMax.MinDataNascimento?.ToString("yyyy-MM-dd"),
                MaxDataNascimento = datasMinMax.MaxDataNascimento?.ToString("yyyy-MM-dd"),
                MinDataDose1HepatiteB = datasMinMax.MinDataDose1HepatiteB?.ToString("yyyy-MM-dd"),
                MaxDataDose1HepatiteB = datasMinMax.MaxDataDose1HepatiteB?.ToString("yyyy-MM-dd"),
                MinDataDose2HepatiteB = datasMinMax.MinDataDose2HepatiteB?.ToString("yyyy-MM-dd"),
                MaxDataDose2HepatiteB = datasMinMax.MaxDataDose2HepatiteB?.ToString("yyyy-MM-dd"),
                MinDataDose3HepatiteB = datasMinMax.MinDataDose3HepatiteB?.ToString("yyyy-MM-dd"),
                MaxDataDose3HepatiteB = datasMinMax.MaxDataDose3HepatiteB?.ToString("yyyy-MM-dd"),
                MinDataDoseReforcoHepatiteB = datasMinMax.MinDataDoseReforcoHepatiteB?.ToString("yyyy-MM-dd"),
                MaxDataDoseReforcoHepatiteB = datasMinMax.MaxDataDoseReforcoHepatiteB?.ToString("yyyy-MM-dd"),
                MinDataExameAntiHBS = datasMinMax.MinDataExameAntiHBS?.ToString("yyyy-MM-dd"),
                MaxDataExameAntiHBS = datasMinMax.MaxDataExameAntiHBS?.ToString("yyyy-MM-dd"),
                MinDataDose1DifteriaTetano = datasMinMax.MinDataDose1DifteriaTetano?.ToString("yyyy-MM-dd"),
                MaxDataDose1DifteriaTetano = datasMinMax.MaxDataDose1DifteriaTetano?.ToString("yyyy-MM-dd"),
                MinDataDose2DifteriaTetano = datasMinMax.MinDataDose2DifteriaTetano?.ToString("yyyy-MM-dd"),
                MaxDataDose2DifteriaTetano = datasMinMax.MaxDataDose2DifteriaTetano?.ToString("yyyy-MM-dd"),
                MinDataDose3DifteriaTetano = datasMinMax.MinDataDose3DifteriaTetano?.ToString("yyyy-MM-dd"),
                MaxDataDose3DifteriaTetano = datasMinMax.MaxDataDose3DifteriaTetano?.ToString("yyyy-MM-dd"),
                MinDataDoseReforcoDifteriaTetano = datasMinMax.MinDataDoseReforcoDifteriaTetano?.ToString("yyyy-MM-dd"),
                MaxDataDoseReforcoDifteriaTetano = datasMinMax.MaxDataDoseReforcoDifteriaTetano?.ToString("yyyy-MM-dd"),
                MinDataDose1TripliceViral = datasMinMax.MinDataDose1TripliceViral?.ToString("yyyy-MM-dd"),
                MaxDataDose1TripliceViral = datasMinMax.MaxDataDose1TripliceViral?.ToString("yyyy-MM-dd"),
                MinDataDose2TripliceViral = datasMinMax.MinDataDose2TripliceViral?.ToString("yyyy-MM-dd"),
                MaxDataDose2TripliceViral = datasMinMax.MaxDataDose2TripliceViral?.ToString("yyyy-MM-dd"),
                MinDataDose1Covid = datasMinMax.MinDataDose1Covid?.ToString("yyyy-MM-dd"),
                MaxDataDose1Covid = datasMinMax.MaxDataDose1Covid?.ToString("yyyy-MM-dd"),
                MinDataDose2Covid = datasMinMax.MinDataDose2Covid?.ToString("yyyy-MM-dd"),
                MaxDataDose2Covid = datasMinMax.MaxDataDose2Covid?.ToString("yyyy-MM-dd"),
                MinDataDose3Covid = datasMinMax.MinDataDose3Covid?.ToString("yyyy-MM-dd"),
                MaxDataDose3Covid = datasMinMax.MaxDataDose3Covid?.ToString("yyyy-MM-dd"),
                MinDataDoseReforco1Covid = datasMinMax.MinDataDoseReforco1Covid?.ToString("yyyy-MM-dd"),
                MaxDataDoseReforco1Covid = datasMinMax.MaxDataDoseReforco1Covid?.ToString("yyyy-MM-dd"),
                MinDataDoseReforco2Covid = datasMinMax.MinDataDoseReforco2Covid?.ToString("yyyy-MM-dd"),
                MaxDataDoseReforco2Covid = datasMinMax.MaxDataDoseReforco2Covid?.ToString("yyyy-MM-dd"),
                MinDataDoseUnicaFebreAmarela = datasMinMax.MinDataDoseUnicaFebreAmarela?.ToString("yyyy-MM-dd"),
                MaxDataDoseUnicaFebreAmarela = datasMinMax.MaxDataDoseUnicaFebreAmarela?.ToString("yyyy-MM-dd"),
                MinDataDoseAnualInfluenza = datasMinMax.MinDataDoseAnualInfluenza?.ToString("yyyy-MM-dd"),
                MaxDataDoseAnualInfluenza = datasMinMax.MaxDataDoseAnualInfluenza?.ToString("yyyy-MM-dd"),
                    MinDataGeral = datasMinMax.MinDataGeral?.ToString("yyyy-MM-dd"),
                MaxDataGeral = datasMinMax.MaxDataGeral?.ToString("yyyy-MM-dd")
            };

            // Passar os dados formatados para a View
            ViewBag.datasMinMax = datasMinMaxFormatadas;


            // Calcular idades dos cadastros
            var idades = _listaDeCadastrosRepositorio.CalcularIdadeDosCadastros();
            ViewBag.idades = idades;

            //Busca todos os cadastros independente do Usuario Logado
            //List<ListaDeCadastrosModel> listaDeCadastros = _listaDeCadastrosRepositorio.BuscarTodos();

            //Busca todos os cadastros marcados como Normal
            List<ListaDeCadastrosModel> listaDeCadastros = _listaDeCadastrosRepositorio.BuscarTodosUsuarioPadrao();

            return View(listaDeCadastros);

            //Busca somente os cadastros relacionados ao Usuario Logado
            //ListaDeUsuariosModel usuarioLogado = _sessao.BuscarSessaoDoUsuario();
            //List<ListaDeCadastrosModel> listaDeCadastros = _listaDeCadastrosRepositorio.BuscarTodos(usuarioLogado.Id);

            //return View(listaDeCadastros);

            //Busca somente os cadastros marcados com a opção válidos 
            //(Indica que não foram "apagados pelo usuario padrão)
        }

        //Métodos auxiliares (chamados na index)
        // Método para carregar os dados do balanço e passar para a ViewBag
        private void CarregarBalancoVacinacao()
        {
            // Calcular os totais em relação aos servidores
            var balanco = _listaDeCadastrosRepositorio.CalcularBalancoDeVacinacaoDosServidores();

            // Verificar se está vazio ou nulo e atribuir null se for o caso
            ViewBag.totalServidoresCadastrados = balanco?.totalServidoresCadastrados ?? (int?)null;
            ViewBag.totalVacinadosCompletamente = balanco?.totalVacinadosCompletamente ?? (int?)null;
            ViewBag.totalVacinadosParcialmente = balanco?.totalVacinadosParcialmente ?? (int?)null;
            ViewBag.totalNaoVacinados = balanco?.totalNaoVacinados ?? (int?)null;
        }

        public IActionResult Cadastrar()
        {
            return View();
        }
        public IActionResult Editar(int id)
        {
            ListaDeCadastrosModel listaDeCadastrosModel = _listaDeCadastrosRepositorio.ListarPorId(id);
            return View(listaDeCadastrosModel);
        }
        public IActionResult ApagarConfirmacao(int id)
        {
            ListaDeCadastrosModel listaDeCadastrosModel = _listaDeCadastrosRepositorio.ListarPorId(id);
            return View(listaDeCadastrosModel);
        }
        public IActionResult Apagar(int id)
        {
            try
            {
                bool apagado = _listaDeCadastrosRepositorio.Apagar(id);
                if (apagado)
                {
                    TempData["MensagemSucesso"] = "Cadastro apagado com sucesso!";
                } else
                {
                    TempData["MensagemErro"] = $"Ops, não foi possível apagar o cadastro, tente novamente!";
                }

                return RedirectToAction("Index");
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não foi possível apagar o cadastro, tente novamente! Erro:{erro.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult PseudoApagarUsuarioPadrao(int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //Editar os cadastros sem associar ao Id do Usuário Logado
                    bool apagado_usuario_padrao = _listaDeCadastrosRepositorio.ApagarUsuarioPadrao(id);

                    if (apagado_usuario_padrao)
                    {
                        TempData["MensagemSucesso"] = "Cadastro apagado com sucesso!";                        
                    } else
                    {
                        TempData["MensagemErro"] = $"Ops, não foi possível apagar o cadastro, tente novamente!";
                    }
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["MensagemErro"] = $"Ops, não foi possível apagar o cadastro, tente novamente!";                    
                }
                return RedirectToAction("Index");
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não foi possível apagar o cadastro, tente novamente! Erro:{erro.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult Cadastrar(ListaDeCadastrosModel listaDeCadastrosModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Verificar se pelo menos um campo foi preenchido (desnecessário)
                    //bool AlgumCampoPreenchido = _listaDeCadastrosRepositorio.VerificarAlgumCampoPreenchido(listaDeCadastrosModel);
                    //if (AlgumCampoPreenchido)
                    //{
                    //    TempData["MensagemErro"] = "Por favor, preencha pelo menos um campo antes de realizar o cadastro.";
                    //    return View(listaDeCadastrosModel);
                    //}

                    // Verificar se já existe um cadastro idêntico
                    bool cadastroDuplicado = _listaDeCadastrosRepositorio.ExisteCadastroIdentico(listaDeCadastrosModel);
                    if (cadastroDuplicado)
                    {
                        TempData["MensagemErro"] = "Já existe um cadastro idêntico. Por favor, revise os dados.";
                        return View(listaDeCadastrosModel);
                    }

                    //Adicionar os cadastros associado ao Id do Usuário Logado
                    ListaDeUsuariosModel usuarioLogado = _sessao.BuscarSessaoDoUsuario();
                    listaDeCadastrosModel.UsuarioId = usuarioLogado.Id;

                    //Adicionar os cadastros sem associar ao Id do Usuário Logado
                    listaDeCadastrosModel = _listaDeCadastrosRepositorio.Adicionar(listaDeCadastrosModel);

                    //Debug

                    TempData["MensagemSucesso"] = "Cadastro realizado com sucesso!";
                    return RedirectToAction("Index");

                } else if (!ModelState.IsValid) {
                    //Debug para identificar validação falsa
                    foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                    {
                        Console.WriteLine($"Erro: {error.ErrorMessage}");
                    }
                }
                return View(listaDeCadastrosModel);
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não foi possível realizar o cadastro, tente novamente! Erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult Alterar(ListaDeCadastrosModel listaDeCadastrosModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Verificar se já existe um cadastro idêntico
                    bool cadastroDuplicado = _listaDeCadastrosRepositorio.ExisteCadastroIdentico(listaDeCadastrosModel);
                    if (cadastroDuplicado)
                    {
                        TempData["MensagemErro"] = "Você não alterou nenhum dado. Por favor, revise os dados.";
                        return View("Editar", listaDeCadastrosModel);
                    }

                    //Editar os cadastros associando ao Id do Usuário Logado
                    ListaDeUsuariosModel usuarioLogado = _sessao.BuscarSessaoDoUsuario();
                    listaDeCadastrosModel.UsuarioId = usuarioLogado.Id;

                    //Editar os cadastros sem associar ao Id do Usuário Logado
                    listaDeCadastrosModel = _listaDeCadastrosRepositorio.Atualizar(listaDeCadastrosModel);

                    TempData["MensagemSucesso"] = "Cadastro alterado com sucesso!";
                    return RedirectToAction("Index");
                }
                return View("Editar", listaDeCadastrosModel);
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não foi possível atualizar seu cadastro, tente novamente! Erro:{erro.Message}";
                return RedirectToAction("Index");
            }
        }



        public IActionResult RelatorioPdf()
        {
            return View();
        }

        public IActionResult VisualizarRelatorio(ListaDeCadastrosModel listaDeCadastrosModel)
        {
            // Recupera os dados
            List<ListaDeCadastrosModel> listaDeCadastros = _listaDeCadastrosRepositorio.BuscarTodosUsuarioPadrao();

            // Calcula os totais
            var totaisPorDose = _listaDeCadastrosRepositorio.CalcularTotaisPorDose();
            var totaisPorVacina = _listaDeCadastrosRepositorio.CalcularTotaisPorVacina();
            var balancoVacinacao = _listaDeCadastrosRepositorio.CalcularBalancoDeVacinacaoDosServidores();

            try
            {
                // Cria um stream de memória para gerar o PDF em memória
                using (var memoryStream = new MemoryStream())
                {

                    // Cria o Pdf (Orientação: Paisagem) - Margem Superior
                    //Document doc = new Document(PageSize.A4.Rotate());
                    // (Orientação: Retrato)
                    Document doc = new Document(PageSize.A4.Rotate());

                    //Margem do papel
                    doc.SetMargins(15, 15, 50, 15); // Margem superior ajustada para começar depois do cabeçalho 40 -> 100: antes (40, 40, 40, 80);
                    doc.AddCreationDate();

                    PdfWriter writer = PdfWriter.GetInstance(doc, memoryStream);

                    writer.CloseStream = false; // Importante para não fechar o MemoryStream

                    //Definir nome e local do arquivo
                    var NomeArquivo = "Relatorio_SCVS_" + DateTime.Now.ToString("dd:MM:yyyy_HH:mm:ss") + ".pdf";

                    // Criar um evento de página para adicionar cabeçalho e rodapé                
                    writer.PageEvent = new Header(); // Cabeçalho(Header) 
                    writer.PageEvent = new Footer(NomeArquivo);// Rodapé(Footer): numeração de página

                    //Abrir documento
                    doc.Open();

                    // Adicionar título usando a classe TituloDocumento
                    doc.Add(TituloDocumento.GerarTitulo("RELATÓRIO DE VACINAÇÃO DOS SERVIDORES"));

                    // Adicionar data usando a classe DataDocumento
                    doc.Add(DataDocumento.GerarData());

                    // Adicionar Texto com o Resultado Balanço de Vacinação dos Servidores
                    doc.Add(BalancoVacinacao.GerarBalancoVacinacao(balancoVacinacao));

                    // Adicionar tabela usando a classe TabelaRelatorio
                    doc.Add(TabelaRelatorio.GerarTabela(listaDeCadastros, totaisPorDose, totaisPorVacina));

                    // Finaliza o documento
                    doc.Close();

                    //------------------Método 1 (Retorna o documento baixado no pc do usuário)---------------------------- 

                    // Configura o tipo de conteúdo para PDF
                    //Response.ContentType = "application/pdf";
                    //Response.Headers.Add("Content-Disposition", "inline; filename=Relatorio.pdf");

                    // Escreve o conteúdo do PDF para a resposta
                    //memoryStream.Seek(0, SeekOrigin.Begin);

                    // Retorna o documento baixado no pc do usuário
                    //return File(memoryStream.ToArray(), "application/pdf", "Relatorio.pdf");

                    //------------------Método 2 (Retorna uma URL temporária com o pdf sem baixar)----------------------------

                    // Configurar o nome do arquivo no cabeçalho e garantir exibição no navegador (na hora de baixar fica com esse nome  o pdf, na hora de vizualizar o pdf, fica com o nome do método)
                    Response.Headers.Add("Content-Disposition", $"inline; filename=\"{NomeArquivo}\"");

                    // Configura o tipo de conteúdo para exibição no navegador
                    memoryStream.Seek(0, SeekOrigin.Begin);
                    return File(memoryStream.ToArray(), "application/pdf");
                }
            }
            catch (Exception ex)
            {
                TempData["MensagemErro"] = $"Erro ao gerar o relatório: {ex.Message}";
                return RedirectToAction("RelatorioPdf");
            }
        }

        public IActionResult GerarRelatorio(ListaDeCadastrosModel listaDeCadastrosModel)
        {
            //List<ListaDeCadastrosModel> listaDeCadastros = _listaDeCadastrosRepositorio.BuscarTodos();

            List<ListaDeCadastrosModel> listaDeCadastros = _listaDeCadastrosRepositorio.BuscarTodosUsuarioPadrao();

            try
            {
                //Criar documento
                Document doc = new Document(PageSize.A4); // Tamanho da página: A4 Paisagem: .Rotate()

                //Margem do papel
                doc.SetMargins(40, 40, 70, 10); // Margem superior ajustada para começar depois do cabeçalho 40 -> 100: antes (40, 40, 40, 80);
                doc.AddCreationDate();

                //Definir nome e local do arquivo
                var NomeArquivo = "Relatorio_SCVS_" + DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss") + ".pdf";

                //Diretorio 
                string diretorio = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "relatorios_cadastros");
                //Garantir que o diretório existe
                if (!Directory.Exists(diretorio))
                {
                    Directory.CreateDirectory(diretorio);
                }

                //Construir o caminho completo do arquivo
                string CaminhoCompleto = Path.Combine(diretorio, NomeArquivo);

                //Informar caminho que vai gravar
                PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(CaminhoCompleto, FileMode.Create));

                // Criar um evento de página para adicionar cabeçalho e rodapé                
                writer.PageEvent = new Header(); // Cabeçalho(Header) 
                writer.PageEvent = new Footer(NomeArquivo);// Rodapé(Footer): numeração de página

                //Abrir documento
                doc.Open();

                // Adicionar título usando a classe TituloDocumento
                //doc.Add(TituloDocumento.GerarTitulo("RELATÓRIO DE VACINAÇÃO DOS SERVIDORES"));

                // Adicionar tabela usando a classe TabelaRelatorio
                //doc.Add(TabelaRelatorio.GerarTabela(listaDeCadastros));

                //Fechar documento
                doc.Close();

                //Construir URL públic para o pdf
                var urlRelativo = Url.Content($"~/relatorios_cadastros/{NomeArquivo}");

                //Redirecionar para a URL do arquivo no navegador
                return Redirect(urlRelativo);
                //Reponse.Redirect("/pdf/relatorio.pdf");
            }
            catch (IOException ioEx)
            {
                TempData["MensagemErro"] = $"Ops, erro ao acessar o relatório, tente novamente! Erro:{ioEx.Message}";
                return RedirectToAction("RelatorioPdf");

            }
            catch (UnauthorizedAccessException uaEx)
            {
                TempData["MensagemErro"] = $"Ops, permissão negada ao acessar o relatório, tente novamente! Erro:{uaEx.Message}";
                return RedirectToAction("RelatorioPdf");
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não foi possível gerar o relatório em pdf, tente novamente! Erro:{erro.Message}";
                return RedirectToAction("RelatorioPdf");
            }
        }

        //Classe para o título do documento (acima da tabela)
        public class TituloDocumento
        {
            public static Paragraph GerarTitulo(string tituloRelatorio)
            {
                var titulo = new Paragraph
                {
                    Font = new Font(Font.FontFamily.HELVETICA, 9, Font.BOLD), // Define a fonte
                    Alignment = Element.ALIGN_CENTER // Centraliza o texto
                };

                // Adiciona o texto do título
                titulo.Add(tituloRelatorio + "\n");

                return titulo;
            }
        }

        //Classe para a data do documento (acima da tabela)
        public class DataDocumento
        {
            public static Paragraph GerarData()
            {

                var dataEHora = new Paragraph
                {
                    Font = new Font(Font.FontFamily.COURIER, 7), // Define a fonte
                    Alignment = Element.ALIGN_CENTER // Centraliza o texto
                };

                // Adiciona a data e hora de criação
                dataEHora.Add($"Emitido em: {DateTime.Now:dd/MM/yyyy HH:mm:ss}\n"); // Usar fonte 10 40 pixels(px)

                return dataEHora;
            }
        }

        //Classe para o balanço de vacinação dos servidores
        public class BalancoVacinacao
        {
            public static Paragraph GerarBalancoVacinacao(ListaDeCadastrosRepositorio.BalancoVacinacao balancoVacinacao)
            {
                var balanco = new Paragraph
                {
                    Font = new Font(Font.FontFamily.HELVETICA, 7), // Define a fonte
                    Alignment = Element.ALIGN_LEFT // Centraliza o texto
                };

                // Adiciona balanço de vacinação totais dos servidores
                //balanco.Add($"BALANÇO DE VACINAÇÃO DOS SERVIDORES:\n"); // Usar fonte 10 40 pixels(px)

                // Adiciona balanço de vacinação totais dos servidores
                balanco.Add($"Total de Servidores: {balancoVacinacao.totalServidoresCadastrados}, Vacinados Completamente: {balancoVacinacao.totalVacinadosCompletamente}, Vacinados Parcialmente: {balancoVacinacao.totalVacinadosParcialmente}, Nenhuma Vacina Aplicada: {balancoVacinacao.totalNaoVacinados}.\n\n"); // Usar fonte 10 40 pixels(px)

                return balanco;
            }
        }

//Classe para a tabela
public class TabelaRelatorio
        {
            public static PdfPTable GerarTabela(List<ListaDeCadastrosModel> listaDeCadastros,
                                                ListaDeCadastrosRepositorio.TotaisPorDose totaisPorDose,
                                                ListaDeCadastrosRepositorio.TotaisPorVacina totaisPorVacina)
            {

                int quantColunas = 26;
                var table = new PdfPTable(quantColunas)
                {
                    WidthPercentage = 100 // Define a largura da tabela
                };

                // Criar a fonte Helvética de tamanho 10

                Font fonteTabela = new Font(Font.FontFamily.HELVETICA, 7);
                Font fonteCaebcalhoLinhaUm = new Font(Font.FontFamily.HELVETICA, 7, Font.NORMAL, BaseColor.WHITE);
                
                //// Definir as larguras das colunas
                //float[] columnWidths_linha_um = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
                //table.SetWidths(columnWidths_linha_um);

                //// Cabeçalho da tabela - Linha 1
                //var colunas_linha_um = new[] { "Hepatite B (HB)", "Exame Anti-HBS", "Difteria Tétano (DT)", "Triplice Viral", "Covid-19", "Febre Amarela", "Influenza" };

                //foreach (var coluna in colunas_linha_um)
                //{
                //    table.AddCell(coluna);
                //}

                // Definir as larguras das colunas
                float[] columnWidths_linha_dois = new float[] { 4f, 5.5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 6f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5.5f, 5f};
                table.SetWidths(columnWidths_linha_dois);

                // Cabeçalho da tabela - Linha 1 com mesclagem das colunas
                table.AddCell(new PdfPCell(new Phrase("Servidor Ativo", fonteCaebcalhoLinhaUm)) { Colspan = 6, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = BaseColor.GRAY});
                table.AddCell(new PdfPCell(new Phrase("Hepatite B (HB)", fonteCaebcalhoLinhaUm)) { Colspan = 4, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = BaseColor.GRAY });
                table.AddCell(new PdfPCell(new Phrase("Anti-HBS", fonteCaebcalhoLinhaUm)) { Colspan = 2, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = BaseColor.GRAY });
                table.AddCell(new PdfPCell(new Phrase("Difteria e Tétano (DF)", fonteCaebcalhoLinhaUm)) { Colspan = 4, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = BaseColor.GRAY });
                table.AddCell(new PdfPCell(new Phrase("Tríplice Viral", fonteCaebcalhoLinhaUm)) { Colspan = 2, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = BaseColor.GRAY });
                table.AddCell(new PdfPCell(new Phrase("Covid-19", fonteCaebcalhoLinhaUm)) { Colspan = 5, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = BaseColor.GRAY });
                table.AddCell(new PdfPCell(new Phrase("Febre Amarela", fonteCaebcalhoLinhaUm)) { Colspan = 1, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = BaseColor.GRAY });
                table.AddCell(new PdfPCell(new Phrase("Influenza", fonteCaebcalhoLinhaUm)) { Colspan = 1, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = BaseColor.GRAY });
                table.AddCell(new PdfPCell(new Phrase("Totais", fonteCaebcalhoLinhaUm)) { Colspan = 1, VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = BaseColor.GRAY });

                // Cabeçalho da tabela - Linha 2
                var colunas_linha_dois = new[]
                {
                    "Id", "Matricula", "Nome", "DT Nascimento", "Setor", "Cargo", // Servidor ativo
                    "DT 1ªDose", "DT 2ªDose", "DT 3ªDose", "DT Reforço", // Hepatite B
                    "DT Exame", "Resultado", // Anti-HBS
                    "DT 1ªDose","DT 2ªDose", "DT 3ªDose", "DT Reforço", // Difteria e Tétano
                    "DT 1ªDose", "DT 2ªDose", // Tríplice Viral
                    "DT 1ªDose","DT 2ªDose", "DT 3ªDose", "DT Reforço 1 ", "DT Reforço 2", // Covid-19
                    "DT Dose Única", // Febre Amarela
                    "DT Dose Anual", // Influenza
                    "Dose / Vacina"
                    //"Situação de Vacinação"
                }; //"Total Vacinas Aplicadas" , "Total Doses Aplicadas"

                foreach (var coluna in colunas_linha_dois)
                {
                    table.AddCell(new PdfPCell(new Phrase(coluna, fonteTabela)) { VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = new BaseColor(211, 211, 211) });// Cinza claro                     
             // Centralizado verticalmente
                }

                // Repetir linha do cabeçalho: Definir que a tabela tem 2 linhas de cabeçalho
                table.HeaderRows = 2;

                int Id = 1; // Inicializa o contador antes do laço

                // Preenche os dados da tabela
                foreach (var cadastro in listaDeCadastros)
                {
                    //table.AddCell(new PdfPCell(new Phrase(Id.ToString(), fonteTabela)));
                    table.AddCell(new PdfPCell(new Phrase(Id.ToString(), fonteTabela)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    table.AddCell(new PdfPCell(new Phrase(cadastro.NumMatricula.ToString(), fonteTabela)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });

                    // Exemplo de aplicação da hifenização ao adicionar conteúdo à tabela
                    string nomeHifenizado = HifenizarTexto(cadastro.Nome, 8);
                    table.AddCell(new PdfPCell(new Phrase(nomeHifenizado, fonteTabela))
                    {
                        HorizontalAlignment = Element.ALIGN_CENTER,
                        VerticalAlignment = Element.ALIGN_MIDDLE
                    });

                    //table.AddCell(new PdfPCell(new Phrase(cadastro.Nome, fonteTabela)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    table.AddCell(new PdfPCell(new Phrase(cadastro.DataNascimento.ToString("dd/\nMM/\nyyyy"), fonteTabela)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    table.AddCell(new PdfPCell(new Phrase(cadastro.Setor, fonteTabela)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    table.AddCell(new PdfPCell(new Phrase(cadastro.Cargo, fonteTabela)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    table.AddCell(new PdfPCell(new Phrase(cadastro.DataDose1HepatiteB?.ToString("dd/\nMM/\nyyyy") ?? "", fonteTabela)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    table.AddCell(new PdfPCell(new Phrase(cadastro.DataDose2HepatiteB?.ToString("dd/\nMM/\nyyyy") ?? "", fonteTabela)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    table.AddCell(new PdfPCell(new Phrase(cadastro.DataDose3HepatiteB?.ToString("dd/\nMM/\nyyyy") ?? "", fonteTabela)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    table.AddCell(new PdfPCell(new Phrase(cadastro.DataDoseReforcoHepatiteB?.ToString("dd/\nMM/\nyyyy") ?? "", fonteTabela)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    table.AddCell(new PdfPCell(new Phrase(cadastro.DataExameAntiHBS?.ToString("dd/\nMM/\nyyyy") ?? "", fonteTabela)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    table.AddCell(new PdfPCell(new Phrase(cadastro.ResultadoExameAntiHBS ?? "", fonteTabela)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    table.AddCell(new PdfPCell(new Phrase(cadastro.DataDose1DifteriaTetano?.ToString("dd/\nMM/\nyyyy") ?? "", fonteTabela)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    table.AddCell(new PdfPCell(new Phrase(cadastro.DataDose2DifteriaTetano?.ToString("dd/\nMM/\nyyyy") ?? "", fonteTabela)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    table.AddCell(new PdfPCell(new Phrase(cadastro.DataDose3DifteriaTetano?.ToString("dd/\nMM/\nyyyy") ?? "", fonteTabela)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    table.AddCell(new PdfPCell(new Phrase(cadastro.DataDoseReforcoDifteriaTetano?.ToString("dd/\nMM/\nyyyy") ?? "", fonteTabela)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    table.AddCell(new PdfPCell(new Phrase(cadastro.DataDose1TripliceViral?.ToString("dd/\nMM/\nyyyy") ?? "", fonteTabela)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    table.AddCell(new PdfPCell(new Phrase(cadastro.DataDose2TripliceViral?.ToString("dd/\nMM/\nyyyy") ?? "", fonteTabela)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    table.AddCell(new PdfPCell(new Phrase(cadastro.DataDose1Covid?.ToString("dd/\nMM/\nyyyy") ?? "", fonteTabela)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    table.AddCell(new PdfPCell(new Phrase(cadastro.DataDose2Covid?.ToString("dd/\nMM/\nyyyy") ?? "", fonteTabela)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    table.AddCell(new PdfPCell(new Phrase(cadastro.DataDose3Covid?.ToString("dd/\nMM/\nyyyy") ?? "", fonteTabela)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    table.AddCell(new PdfPCell(new Phrase(cadastro.DataDoseReforco1Covid?.ToString("dd/\nMM/\nyyyy") ?? "", fonteTabela)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    table.AddCell(new PdfPCell(new Phrase(cadastro.DataDoseReforco2Covid?.ToString("dd/\nMM/\nyyyy") ?? "", fonteTabela)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    table.AddCell(new PdfPCell(new Phrase(cadastro.DataDoseUnicaFebreAmarela?.ToString("dd/\nMM/\nyyyy") ?? "", fonteTabela)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });
                    table.AddCell(new PdfPCell(new Phrase(cadastro.DataDoseAnualInfluenza?.ToString("dd/\nMM/\nyyyy") ?? "", fonteTabela)) { HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE });


                    // Exibindo a Situação de Vacinação com base no código
                    //string situacaoVacinacao = "";
                    //string detalhesVacinacao = "";
                    //switch (cadastro.SituacaoVacinacao)
                    //{
                    //    case ControleVacinas___MVC_ASP_NET.Enums.SituacaoVacinacaoEnum.VacinacaoCompleta:
                    //        situacaoVacinacao = "Vacinação Completa";
                    //        detalhesVacinacao = $"{cadastro.TotalDosesAplicadas} Doses de {cadastro.TotalTiposDeVacinas} Vacinas Aplicadas";
                    //        break;

                    //    case ControleVacinas___MVC_ASP_NET.Enums.SituacaoVacinacaoEnum.VacinacaoAusente:
                    //        situacaoVacinacao = "Vacinação Ausente";
                    //        detalhesVacinacao = "";
                    //        break;

                    //    case ControleVacinas___MVC_ASP_NET.Enums.SituacaoVacinacaoEnum.VacinacaoParcial:
                    //        situacaoVacinacao = "Vacinação Parcial";
                    //        detalhesVacinacao = $"{cadastro.TotalTiposDeVacinasAplicadas} de {cadastro.TotalTiposDeVacinas} Vacinas Aplicadas\n" +
                    //                            $"{cadastro.TotalDosesAplicadas} de {cadastro.TotalDoses} Doses Aplicadas";
                    //        break;

                    //    default:
                    //        situacaoVacinacao = "Situação Desconhecida";
                    //        detalhesVacinacao = "";
                    //        break;
                    //}
                    //// Agora, vamos adicionar as informações detalhadas à tabela no PDF
                    //string conteudo = situacaoVacinacao + "\n" + detalhesVacinacao; // Concatenando com <br> para quebra de linha ou Environment.NewLine ou \n

                    ////table.AddCell(conteudo); // Adicionando a célula com as informações em múltiplas linhas

                    //// Situação de vacinação
                    ////string situacao = cadastro.SituacaoVacinacao.ToString();
                    //table.AddCell(new PdfPCell(new Phrase(conteudo, fonteTabela)));

                    // Exibir os Totais de Aplicação de Doses e Vacinas
                    string totalDosesAplicadas = $"{cadastro.TotalDosesAplicadas}";
                    string totalTiposDeVacinasAplicadas = $"{cadastro.TotalTiposDeVacinasAplicadas}";
                    string totais = totalDosesAplicadas + " / " + totalTiposDeVacinasAplicadas;
                    table.AddCell(new PdfPCell(new Phrase(totais, fonteTabela))
                    {
                        VerticalAlignment = Element.ALIGN_MIDDLE,
                        HorizontalAlignment = Element.ALIGN_CENTER
                    });

                    Id++;
                }

                // Rodapé da tabela - Primeira linha de totais
                // Totais por DOSE
                table.AddCell(new PdfPCell(new Phrase("Total Servidores / DOSE", fonteTabela))
                {
                    Colspan = 6,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    BackgroundColor = new BaseColor(211, 211, 211)
                });

                table.AddCell(new PdfPCell(new Phrase(totaisPorDose.TotalDose1HepatiteB.ToString(), fonteTabela))
                {
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    BackgroundColor = new BaseColor(211, 211, 211)
                });
                table.AddCell(new PdfPCell(new Phrase(totaisPorDose.TotalDose2HepatiteB.ToString(), fonteTabela))
                {
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    BackgroundColor = new BaseColor(211, 211, 211)
                });
                table.AddCell(new PdfPCell(new Phrase(totaisPorDose.TotalDose3HepatiteB.ToString(), fonteTabela))
                {
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    BackgroundColor = new BaseColor(211, 211, 211)
                });
                table.AddCell(new PdfPCell(new Phrase(totaisPorDose.TotalReforcoHepatiteB.ToString(), fonteTabela))
                {
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    BackgroundColor = new BaseColor(211, 211, 211)
                });
                table.AddCell(new PdfPCell(new Phrase(totaisPorDose.TotalExameAntiHBS.ToString(), fonteTabela))
                {
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    BackgroundColor = new BaseColor(211, 211, 211)
                });
                table.AddCell(new PdfPCell(new Phrase(totaisPorDose.TotalResultadoAntiHBS.ToString(), fonteTabela))
                {
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    BackgroundColor = new BaseColor(211, 211, 211)
                });
                table.AddCell(new PdfPCell(new Phrase(totaisPorDose.TotalDose1DifteriaTetano.ToString(), fonteTabela))
                {
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    BackgroundColor = new BaseColor(211, 211, 211)
                });
                table.AddCell(new PdfPCell(new Phrase(totaisPorDose.TotalDose2DifteriaTetano.ToString(), fonteTabela))
                {
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    BackgroundColor = new BaseColor(211, 211, 211)
                });
                table.AddCell(new PdfPCell(new Phrase(totaisPorDose.TotalDose3DifteriaTetano.ToString(), fonteTabela))
                {
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    BackgroundColor = new BaseColor(211, 211, 211)
                });
                table.AddCell(new PdfPCell(new Phrase(totaisPorDose.TotalReforcoDifteriaTetano.ToString(), fonteTabela))
                {
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    BackgroundColor = new BaseColor(211, 211, 211)
                });
                table.AddCell(new PdfPCell(new Phrase(totaisPorDose.TotalDose1TripliceViral.ToString(), fonteTabela))
                {
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    BackgroundColor = new BaseColor(211, 211, 211)
                });
                table.AddCell(new PdfPCell(new Phrase(totaisPorDose.TotalDose2TripliceViral.ToString(), fonteTabela))
                {
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    BackgroundColor = new BaseColor(211, 211, 211)
                });
                table.AddCell(new PdfPCell(new Phrase(totaisPorDose.TotalDose1Covid.ToString(), fonteTabela))
                {
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    BackgroundColor = new BaseColor(211, 211, 211)
                });
                table.AddCell(new PdfPCell(new Phrase(totaisPorDose.TotalDose2Covid.ToString(), fonteTabela))
                {
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    BackgroundColor = new BaseColor(211, 211, 211)
                });
                table.AddCell(new PdfPCell(new Phrase(totaisPorDose.TotalDose3Covid.ToString(), fonteTabela))
                {
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    BackgroundColor = new BaseColor(211, 211, 211)
                });
                table.AddCell(new PdfPCell(new Phrase(totaisPorDose.TotalReforco1Covid.ToString(), fonteTabela))
                {
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    BackgroundColor = new BaseColor(211, 211, 211)
                });
                table.AddCell(new PdfPCell(new Phrase(totaisPorDose.TotalReforco2Covid.ToString(), fonteTabela))
                {
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    BackgroundColor = new BaseColor(211, 211, 211)
                });
                table.AddCell(new PdfPCell(new Phrase(totaisPorDose.TotalDoseUnicaFebreAmarela.ToString(), fonteTabela))
                {
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    BackgroundColor = new BaseColor(211, 211, 211)
                });
                table.AddCell(new PdfPCell(new Phrase(totaisPorDose.TotalDoseAnualInfluenza.ToString(), fonteTabela))
                {
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    BackgroundColor = new BaseColor(211, 211, 211)
                });
                table.AddCell(new PdfPCell(new Phrase("", fonteTabela))
                {
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    BackgroundColor = new BaseColor(211, 211, 211)
                });

                Font fonteRodapeLinhaDois = new Font(Font.FontFamily.HELVETICA, 7, Font.NORMAL, BaseColor.WHITE);
                // Rodapé da tabela - Segunda linha de totais
                // Totais por VACINA
                table.AddCell(new PdfPCell(new Phrase("Total Servidores / VACINA", fonteRodapeLinhaDois))
                {
                    Colspan = 6,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    BackgroundColor = BaseColor.GRAY                                  
                });
                table.AddCell(new PdfPCell(new Phrase(totaisPorVacina.TotalHepatiteB.ToString(), fonteRodapeLinhaDois))
                {
                    Colspan = 4,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    BackgroundColor = BaseColor.GRAY
                });
                table.AddCell(new PdfPCell(new Phrase(totaisPorVacina.TotalExameAntiHBS.ToString(), fonteRodapeLinhaDois))
                {
                    Colspan = 2,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    BackgroundColor = BaseColor.GRAY
                });
                table.AddCell(new PdfPCell(new Phrase(totaisPorVacina.TotalDifteriaTetano.ToString(), fonteRodapeLinhaDois))
                {
                    Colspan = 4,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    BackgroundColor = BaseColor.GRAY
                });
                table.AddCell(new PdfPCell(new Phrase(totaisPorVacina.TotalTripliceViral.ToString(), fonteRodapeLinhaDois))
                {
                    Colspan = 2,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    BackgroundColor = BaseColor.GRAY
                });
                table.AddCell(new PdfPCell(new Phrase(totaisPorVacina.TotalCovid.ToString(), fonteRodapeLinhaDois))
                {
                    Colspan = 5,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    BackgroundColor = BaseColor.GRAY
                });
                table.AddCell(new PdfPCell(new Phrase(totaisPorVacina.TotalFebreAmarela.ToString(), fonteRodapeLinhaDois))
                {
                    Colspan = 1,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    BackgroundColor = BaseColor.GRAY
                });
                table.AddCell(new PdfPCell(new Phrase(totaisPorVacina.TotalInfluenza.ToString(), fonteRodapeLinhaDois))
                {
                    Colspan = 1,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    BackgroundColor = BaseColor.GRAY
                });
                table.AddCell(new PdfPCell(new Phrase("", fonteRodapeLinhaDois))
                {
                    Colspan = 1,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    BackgroundColor = BaseColor.GRAY
                });

                return table;
            }
        }
        //Classe para o cabeçalho
        public class Header : PdfPageEventHelper
        {
            public override void OnStartPage(PdfWriter writer, Document doc)
            {
                base.OnStartPage(writer, doc);

                // Definir o cabeçalho
                Font headerFont = new Font(Font.FontFamily.COURIER, 7);

                // Primeira linha do cabeçalho
                ColumnText.ShowTextAligned(
                    writer.DirectContent,
                    Element.ALIGN_CENTER,                     // Alinhamento horizontal
                    new Phrase("Fundação Centro de Hemoterapia e Hematologia do Pará - Hemopa", headerFont), // Texto
                    doc.PageSize.Width / 2,                  // Centralizar na largura
                    doc.PageSize.Height - 15,                // Coordenada Y (primeira linha)
                    0);                                      // Sem rotação

                // Segunda linha do cabeçalho
                ColumnText.ShowTextAligned(
                    writer.DirectContent,
                    Element.ALIGN_CENTER,                     // Alinhamento horizontal
                    new Phrase("Serviço de Atendimento à Saúde do Servidor - SASS", headerFont), // Texto
                    doc.PageSize.Width / 2,                  // Centralizar na largura
                    doc.PageSize.Height - 25,                // Coordenada Y (segunda linha, abaixo da primeira)
                    0);                                      // Sem rotação

                // Terceira linha do cabeçalho
                ColumnText.ShowTextAligned(
                    writer.DirectContent,
                    Element.ALIGN_CENTER,                     // Alinhamento horizontal
                    new Phrase("Sistema de Cadastro de Vacinação dos Servidores - SCVS", headerFont), // Texto
                    doc.PageSize.Width / 2,                  // Centralizar na largura
                    doc.PageSize.Height - 35,                // Coordenada Y (segunda linha, abaixo da primeira)
                    0);                                      // Sem rotação

                //Font headerFont = new Font(Font.FontFamily.HELVETICA, 12, Font.BOLD);
                //Phrase header = new Phrase("Fundação Centro de Hemoterapia e Hematologia do Pará - Hemopa\nGerencia de Tecnologia da Informação - Getin\n", headerFont);
                //ColumnText.ShowTextAligned(writer.DirectContent, Element.ALIGN_CENTER, header, 300, 800, 0); // Ajuste a posição conforme necessário

                // Adicionar linha separadora abaixo do cabeçalho
                PdfContentByte canvas = writer.DirectContent;
                canvas.SetColorStroke(BaseColor.BLACK);
                canvas.SetLineWidth(0.1f);

                //canvas.MoveTo(35, doc.PageSize.Height - 40); // Posição inicial da linha
                //canvas.LineTo(doc.PageSize.Width - 35, doc.PageSize.Height - 40); // Posição final da linha
                //canvas.Stroke(); // Desenhar a linha

                // Posição inicial da linha (margem esquerda) e final da linha (margem direita)
                float marginLeft = doc.LeftMargin;
                float marginRight = doc.RightMargin;
                float yPosition = doc.PageSize.Height - 42; // Posição Y da linha separadora

                canvas.MoveTo(marginLeft, yPosition);  // Posição inicial (margem esquerda)
                canvas.LineTo(doc.PageSize.Width - marginRight, yPosition);  // Posição final (margem direita)
                canvas.Stroke(); // Desenhar a linha
            }
        }

        //Classe para o rodapé
        public class Footer : PdfPageEventHelper
        {
            //variáveis do template do rodapé
            private PdfTemplate footerTemplate;// This is the contentbyte object of the writer
            private BaseFont bf; // we will put the final number of pages in a template            
            private string nomeArquivo; //variável do nome do arquivo
            private Font footerFont;

            public Footer(string nomeArquivo)
            {
                this.nomeArquivo = nomeArquivo;
                this.footerFont = new Font(Font.FontFamily.COURIER, 7, Font.NORMAL);
            }
            public override void OnOpenDocument(PdfWriter writer, Document document)
            {
                try
                {
                    bf = BaseFont.CreateFont(BaseFont.COURIER, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                    PdfContentByte cb = writer.DirectContent;
                    footerTemplate = cb.CreateTemplate(50, 50);
                }
                catch (DocumentException de)
                {
                    //handle exception here
                    Console.WriteLine(de.Message);
                }
                catch (System.IO.IOException ioe)
                {
                    //handle exception here
                    Console.WriteLine(ioe.Message);
                }
            }

            //Rodapé
            public override void OnEndPage(PdfWriter writer, Document doc)
            {
                base.OnEndPage(writer, doc);

                // Rodapé esquerdo: nome do arquivo (alinhado à esquerda)
                ColumnText.ShowTextAligned(writer.DirectContent, Element.ALIGN_LEFT, new Phrase(nomeArquivo, footerFont), doc.LeftMargin, doc.Bottom - 6, 0); // Exibir o nome do arquivo alinhado à esquerda
                //ColumnText.ShowTextAligned(writer.DirectContent, Element.ALIGN_LEFT, new Phrase(nomeArquivo, footerFont), 40, 30, 0); // Exibir o nome do arquivo alinhado à esquerda

                //Ródapé direito: Página 1 de 10 páginas (alinhado à direita)
                PdfContentByte cb = writer.DirectContent;
                string text = "Página " + writer.PageNumber + " de ";

                Phrase pageNumberPhrase = new Phrase(text, footerFont);
                float textWidth = bf.GetWidthPoint(text, footerFont.Size);

                // Alinhamento à direita: Posição considerando a margem direita
                float rightAlignedX = doc.PageSize.GetRight(doc.RightMargin);
                ColumnText.ShowTextAligned(cb, Element.ALIGN_LEFT, pageNumberPhrase, rightAlignedX - textWidth - 5, doc.Bottom - 6, 0);
                //ColumnText.ShowTextAligned(cb, Element.ALIGN_LEFT, pageNumberPhrase, doc.PageSize.GetRight(100), 30, 0);

                // Adicionar o template para o número total de páginas
                cb.AddTemplate(footerTemplate, rightAlignedX - 4, doc.Bottom - 6); // Ajuste o espaço necessário
                //cb.AddTemplate(footerTemplate, doc.PageSize.GetRight(100) + textWidth + 10, 30); // Adicionar o template para o total de páginas
            }

            public override void OnCloseDocument(PdfWriter writer, Document doc)
            {
                base.OnCloseDocument(writer, doc);

                // Escrever o número total de páginas no template
                footerTemplate.BeginText();
                footerTemplate.SetFontAndSize(bf, footerFont.Size);
                //footerTemplate.SetFontAndSize(bf, 8);
                footerTemplate.SetTextMatrix(0, 0);
                footerTemplate.ShowText((writer.PageNumber).ToString());
                //footerTemplate.ShowText((writer.PageNumber - 1).ToString()); //Lógica errada
                footerTemplate.EndText();
            }
        }

        public static string HifenizarTexto(string texto, int maxLength = 20)
        {
            // Dividir o texto em palavras
            var palavras = texto.Split(' ');

            var textoHifenizado = new StringBuilder();

            foreach (var palavra in palavras)
            {
                // Se a palavra for maior que o tamanho máximo permitido, divida-a com hifens
                if (palavra.Length > maxLength)
                {
                    int start = 0;
                    while (start < palavra.Length)
                    {
                        // Se ainda há texto restante, adicione a parte da palavra até o tamanho máximo com hifenização
                        int length = Math.Min(maxLength, palavra.Length - start);
                        textoHifenizado.Append(palavra.Substring(start, length));

                        if (start + length < palavra.Length)
                        {
                            textoHifenizado.Append('-'); // Hifenização
                        }

                        start += length;
                    }
                }
                else
                {
                    textoHifenizado.Append(palavra); // Se não for grande, apenas adicione a palavra
                }

                textoHifenizado.Append(" "); // Separador de palavras
            }

            return textoHifenizado.ToString().Trim();
        }

    }
}
