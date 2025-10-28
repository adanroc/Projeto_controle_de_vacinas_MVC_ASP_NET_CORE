using System.ComponentModel.DataAnnotations;

namespace ControleVacinas___MVC_ASP_NET.Models
{
    public class RedefinirSenhaModel
    {
            [Required(ErrorMessage = "O login é obrigatório!")]
            public string Login { get; set; }

            [Required(ErrorMessage = "O email é obrigatória!")]
            public string Email { get; set; }        
    }
}
