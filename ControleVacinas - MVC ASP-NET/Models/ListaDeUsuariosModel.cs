using ControleVacinas___MVC_ASP_NET.Enums;
using ControleVacinas___MVC_ASP_NET.Helper;
using System;
using System.ComponentModel.DataAnnotations;

namespace ControleVacinas___MVC_ASP_NET.Models
{
    public class ListaDeUsuariosModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome do usuário é obrigatório!")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O login é obrigatório!")]
        public string Login { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória!")]
        public string Senha { get; set; }

        [Required(ErrorMessage = "O email é obrigatório!")]
        [EmailAddress(ErrorMessage = "O email informado é inválido!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O perfil é obrigatório!")]
        public PerfilEnum? Perfil { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime? DataAtualizacao { get; set; }

        public virtual List<ListaDeCadastrosModel>? ListaDeCadastros {  get; set; }

        public bool SenhaValida(string senha) 
        {
            return Senha == senha.GerarHash();
        }
        public void SetSenhaHash()
        {
            Senha = Senha.GerarHash();
        }
        public void SetNovaSenha(string novaSenha)
        {
            Senha = novaSenha.GerarHash();
        }

        public string GerarNovaSenha()
        {
            string novaSenha = Guid.NewGuid().ToString().Substring(0, 8);
            Senha = novaSenha.GerarHash();
            return novaSenha;
        }
    }
}
