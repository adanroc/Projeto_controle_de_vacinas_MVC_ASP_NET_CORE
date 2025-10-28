using ControleVacinas___MVC_ASP_NET.Data;
using ControleVacinas___MVC_ASP_NET.Models;
using Microsoft.EntityFrameworkCore;

namespace ControleVacinas___MVC_ASP_NET.Repositorio
{
    public class ListaDeUsuariosRepositorio : IListaDeUsuariosRepositorio
    {
        private readonly BancoContext _bancoContext;
        public ListaDeUsuariosRepositorio(BancoContext bancoContext)
        {
            _bancoContext = bancoContext;
        }

        public ListaDeUsuariosModel BuscarPorLogin(string login)
        {
            return _bancoContext.ListaDeUsuarios.FirstOrDefault(x => x.Login.ToUpper() == login.ToUpper());
        }

        public ListaDeUsuariosModel BuscarPorEmailELogin(string email, string login)
        {
            return _bancoContext.ListaDeUsuarios.FirstOrDefault(x => x.Email.ToUpper() == email.ToUpper() && x.Login.ToUpper() == login.ToUpper());

        }

        public ListaDeUsuariosModel ListarPorId(int id)
        {
            return _bancoContext.ListaDeUsuarios.FirstOrDefault(x => x.Id == id);
        }

        //* 
        public List<ListaDeUsuariosModel> BuscarTodos()
        {
            //Busca todo o conteúdo da lista de usuários
            //return _bancoContext.ListaDeUsuarios.ToList();

            //Busca todo o conteúdo da lista de usuários e seus cadastros filhos
            return _bancoContext.ListaDeUsuarios.Include(x => x.ListaDeCadastros).ToList();            
        }

        public ListaDeUsuariosModel Adicionar(ListaDeUsuariosModel listaDeUsuariosModel)
        {
            // Gravar no BD
            listaDeUsuariosModel.DataCadastro = DateTime.Now;
            listaDeUsuariosModel.SetSenhaHash();            
            _bancoContext.ListaDeUsuarios.Add(listaDeUsuariosModel);
            _bancoContext.SaveChanges();
            return listaDeUsuariosModel;
        }

        public ListaDeUsuariosModel Atualizar(ListaDeUsuariosModel listaDeUsuariosModel)
        {
            ListaDeUsuariosModel listaDeUsuariosDB = ListarPorId(listaDeUsuariosModel.Id);

            if (listaDeUsuariosDB == null) throw new Exception("Houve um erro na atualização do usuário!");

            listaDeUsuariosDB.Nome = listaDeUsuariosModel.Nome;
            listaDeUsuariosDB.Email = listaDeUsuariosModel.Email;
            listaDeUsuariosDB.Login = listaDeUsuariosModel.Login;
            listaDeUsuariosDB.Perfil = listaDeUsuariosModel.Perfil;
            listaDeUsuariosDB.DataAtualizacao = DateTime.Now;

            _bancoContext.ListaDeUsuarios.Update(listaDeUsuariosDB);
            _bancoContext.SaveChanges();
            return listaDeUsuariosDB;
        }

        public ListaDeUsuariosModel AlterarSenha(AlterarSenhaModel alterarSenhaModel)
        {
            ListaDeUsuariosModel listaDeUsuariosDB = ListarPorId(alterarSenhaModel.Id);

            if (listaDeUsuariosDB == null) throw new Exception("Houve um erro na atualização da senha, usuário não encontrado!");
            if (!listaDeUsuariosDB.SenhaValida(alterarSenhaModel.SenhaAtual)) throw new Exception("Senha atual não confere!");
            if (listaDeUsuariosDB.SenhaValida(alterarSenhaModel.NovaSenha)) throw new Exception("Nova senha deve ser diferente da senha atual!");

            listaDeUsuariosDB.SetNovaSenha(alterarSenhaModel.NovaSenha);
            listaDeUsuariosDB.DataAtualizacao = DateTime.Now;

            _bancoContext.ListaDeUsuarios.Update(listaDeUsuariosDB);
            _bancoContext.SaveChanges();

            return listaDeUsuariosDB;
        }

        public bool Apagar(int id)
        {
            ListaDeUsuariosModel listaDeUsuariosDB = ListarPorId(id);

            if (listaDeUsuariosDB == null) throw new Exception("Houve um Erro ao apagar o usuário!");
            _bancoContext.ListaDeUsuarios.Remove(listaDeUsuariosDB);
            _bancoContext.SaveChanges();
            return true;
        }
    }
}
