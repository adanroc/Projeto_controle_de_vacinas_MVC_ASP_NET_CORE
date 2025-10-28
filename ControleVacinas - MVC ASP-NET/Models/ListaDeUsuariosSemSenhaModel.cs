using ControleVacinas___MVC_ASP_NET.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace ControleVacinas___MVC_ASP_NET.Models
{
    public class ListaDeUsuariosSemSenhaModel
    {
        public int Id {  get; set; }

        [Required(ErrorMessage = "O nome do usuário é obrigatório!")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O login é obrigatório!")]
        public string Login {  get; set; }

        [Required(ErrorMessage = "O email é obrigatório!")]
        [EmailAddress(ErrorMessage = "O email informado é inválido!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O perfil é obrigatório!")]
        public PerfilEnum? Perfil { get; set; }
    }
}
