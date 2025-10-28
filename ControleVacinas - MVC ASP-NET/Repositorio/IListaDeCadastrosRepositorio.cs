using ControleVacinas___MVC_ASP_NET.Models;
using static ControleVacinas___MVC_ASP_NET.Repositorio.ListaDeCadastrosRepositorio;

namespace ControleVacinas___MVC_ASP_NET.Repositorio
{
    public interface IListaDeCadastrosRepositorio
    {
        //Buscar  todos os cadastros independente do usuario logado 
        List<ListaDeCadastrosModel> BuscarTodos();

        //Buscar  somente os cadastros do usuario logado (sobrecarga 1)
        List<ListaDeCadastrosModel> BuscarTodos(int usuarioId);

        //Busca somente os cadastros marcados como Normal ("sobrecarga" 2)
        public List<ListaDeCadastrosModel> BuscarTodosUsuarioPadrao();

        //Buscar  somente os cadastros do usuario logado (sem sobrecarga do método)
        //List<ListaDeCadastrosModel> BuscarTodosOsCadastrosDoUsuario(int usuarioId);
        ListaDeCadastrosModel ListarPorId(int id);
        ListaDeCadastrosModel Adicionar(ListaDeCadastrosModel listaDeCadastrosModel);
        ListaDeCadastrosModel Atualizar(ListaDeCadastrosModel listaDeCadastrosModel);        
        bool Apagar(int id);
        bool ApagarUsuarioPadrao(int id);
        public bool ExisteCadastroIdentico(ListaDeCadastrosModel novoCadastro);
        public bool VerificarAlgumCampoPreenchido(ListaDeCadastrosModel listaDeCadastrosModel);

        void CalcularTotalTiposDeVacinasTodosCadastros();

        void CalcularTotalDosesTodosCadastros();
        TotaisPorDose CalcularTotaisPorDose();
        TotaisPorVacina CalcularTotaisPorVacina();
        BalancoVacinacao CalcularBalancoDeVacinacaoDosServidores();

        public List<string> CalcularIdadeDosCadastros();
        public DatasMinMax ObterMinMaxDatas();

        public List<ListaDeCadastrosModel> BuscarComFiltro(
            int? numMatricula = null,
            string nome = null,
            DateTime? dataNascimentoInicio = null,
            DateTime? dataNascimentoFim = null,
            string setor = null,
            string cargo = null);
    }
}

