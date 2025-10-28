using ControleVacinas___MVC_ASP_NET.Models;

namespace ControleVacinas___MVC_ASP_NET.Helper
{
    public interface ISessao
    {
        void CriarSessaoDoUsuario(ListaDeUsuariosModel usuario);
        void RemoverSessaoDoUsuario();
        ListaDeUsuariosModel BuscarSessaoDoUsuario();
    }
}
