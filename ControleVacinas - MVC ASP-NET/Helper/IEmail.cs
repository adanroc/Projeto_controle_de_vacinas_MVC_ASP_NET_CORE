namespace ControleVacinas___MVC_ASP_NET.Helper
{
    public interface IEmail
    {
        bool Enviar(string email, string assunto, string mensagem);
    }
}
