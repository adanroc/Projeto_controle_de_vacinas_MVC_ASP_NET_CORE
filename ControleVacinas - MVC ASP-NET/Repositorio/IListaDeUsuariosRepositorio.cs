using ControleVacinas___MVC_ASP_NET.Models;

namespace ControleVacinas___MVC_ASP_NET.Repositorio
{
    public interface IListaDeUsuariosRepositorio
    {
        ListaDeUsuariosModel BuscarPorLogin(string login);
        ListaDeUsuariosModel BuscarPorEmailELogin(string email, string login);
        ListaDeUsuariosModel ListarPorId(int id);
        List<ListaDeUsuariosModel> BuscarTodos();
        ListaDeUsuariosModel Adicionar(ListaDeUsuariosModel listaDeUsuariosModel);
        ListaDeUsuariosModel Atualizar(ListaDeUsuariosModel listaDeUsuariosModel);
        ListaDeUsuariosModel AlterarSenha(AlterarSenhaModel alterarSenhaModel);

        bool Apagar(int id);


    }
}
