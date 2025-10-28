using System.ComponentModel.DataAnnotations;

namespace ControleVacinas___MVC_ASP_NET.Models
{
    public class AlterarSenhaModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage="A senha atual é obrigatória")]
        public string SenhaAtual { get; set; }
        [Required(ErrorMessage = "A nova senha é obrigatória")]
        public string NovaSenha { get; set; }
        [Required(ErrorMessage = "A confirmação da nova senha é obrigatória")]
        [Compare("NovaSenha", ErrorMessage ="A senha não confere com sua nova senha")]
        public string ConfirmarNovaSenha { get; set; }


    }
}
