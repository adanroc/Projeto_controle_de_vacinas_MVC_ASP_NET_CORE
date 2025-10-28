using System.ComponentModel.DataAnnotations;

namespace ControleVacinas___MVC_ASP_NET.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "O login é obrigatório!")]
        public string Login { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória!")]
        public string Senha { get; set; }
    }
}
