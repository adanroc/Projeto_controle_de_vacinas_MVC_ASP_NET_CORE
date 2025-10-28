using ControleVacinas___MVC_ASP_NET.Data;
using ControleVacinas___MVC_ASP_NET.Models;
using Microsoft.EntityFrameworkCore;

namespace ControleVacinas___MVC_ASP_NET.Repositorio
{
    public class ListaDeCadastrosRepositorio : IListaDeCadastrosRepositorio
    {
        private readonly BancoContext _bancoContext;
        public ListaDeCadastrosRepositorio(BancoContext bancoContext)
        {
            _bancoContext = bancoContext;
        }

        public ListaDeCadastrosModel ListarPorId(int id)
        {
            return _bancoContext.ListaDeCadastros.FirstOrDefault(x => x.Id == id);
        }

        //-------------Métodos Sobrecarregados---------------------

        //Busca todos os cadastros independente do usuario logado (método original)
        public List<ListaDeCadastrosModel> BuscarTodos()
        {
            return _bancoContext.ListaDeCadastros.ToList();
        }

        //Busca somente os cadastros do usuario logado (sobrecarga 1)
        public List<ListaDeCadastrosModel> BuscarTodos(int usuarioId)
        {
            return _bancoContext.ListaDeCadastros.Where(x => x.UsuarioId == usuarioId).ToList();
        }

        //Busca somente os cadastros do usuario logado (sem sobrecarga)
        //public List<ListaDeCadastrosModel> BuscarTodosOsCadastrosDoUsuario(int usuarioId)
        //{
        //    return _bancoContext.ListaDeCadastros.Where(x => x.UsuarioId == usuarioId).ToList();
        //}

        //Busca somente os cadastros marcados como Normal (sobrecarga 2)
        //namespace ControleVacinas___MVC_ASP_NET.Enums{public enum ApagadoEnum: {Normal = 1,Apagado = 2}}
        //public ApagadoEnum? ApagadoUsuarioPadrao { get; set; }
        public List<ListaDeCadastrosModel> BuscarTodosUsuarioPadrao()
        {
            return _bancoContext.ListaDeCadastros
                   .Where(x => x.ApagadoUsuarioPadrao == Enums.ApagadoEnum.Normal)
                   .ToList();             
        }
        
        //-------------Métodos Sobrecarregados---------------------

        public ListaDeCadastrosModel Adicionar(ListaDeCadastrosModel listaDeCadastrosModel)
        {
            listaDeCadastrosModel.ApagadoUsuarioPadrao = Enums.ApagadoEnum.Normal;
            listaDeCadastrosModel.DataCadastro = DateTime.Now;
            // Insere no banco
            _bancoContext.ListaDeCadastros.Add(listaDeCadastrosModel);
            // Salva os valores
            _bancoContext.SaveChanges();

            return listaDeCadastrosModel;
        }

        public ListaDeCadastrosModel Atualizar(ListaDeCadastrosModel listaDeCadastrosModel)
        {
            ListaDeCadastrosModel listaDeCadastrosDB = ListarPorId(listaDeCadastrosModel.Id);

            if (listaDeCadastrosDB == null) throw new Exception("Houve um Erro na atualização do cadastro!");

            // Dados do servidor ativo

            listaDeCadastrosDB.NumMatricula = listaDeCadastrosModel.NumMatricula;
            listaDeCadastrosDB.Nome = listaDeCadastrosModel.Nome;            
            listaDeCadastrosDB.DataNascimento = listaDeCadastrosModel.DataNascimento;
            listaDeCadastrosDB.Setor = listaDeCadastrosModel.Setor;
            listaDeCadastrosDB.Cargo = listaDeCadastrosModel.Cargo;

            // Data das doses das vacinas

            // Data - Hepatite B (HB)
            listaDeCadastrosDB.DataDose1HepatiteB = listaDeCadastrosModel.DataDose1HepatiteB;
            listaDeCadastrosDB.DataDose2HepatiteB = listaDeCadastrosModel.DataDose2HepatiteB;
            listaDeCadastrosDB.DataDose3HepatiteB = listaDeCadastrosModel.DataDose3HepatiteB;
            listaDeCadastrosDB.DataDoseReforcoHepatiteB = listaDeCadastrosModel.DataDoseReforcoHepatiteB;

            // Data - Exame Anti-HBS (Hepatite B)
            listaDeCadastrosDB.DataExameAntiHBS = listaDeCadastrosModel.DataExameAntiHBS;
            listaDeCadastrosDB.ResultadoExameAntiHBS = listaDeCadastrosModel.ResultadoExameAntiHBS;

            // Data - Difteria e Tétano (DT)
            listaDeCadastrosDB.DataDose1DifteriaTetano = listaDeCadastrosModel.DataDose1DifteriaTetano;
            listaDeCadastrosDB.DataDose2DifteriaTetano = listaDeCadastrosModel.DataDose2DifteriaTetano;
            listaDeCadastrosDB.DataDose3DifteriaTetano = listaDeCadastrosModel.DataDose3DifteriaTetano;
            listaDeCadastrosDB.DataDoseReforcoDifteriaTetano = listaDeCadastrosModel.DataDoseReforcoDifteriaTetano;

            // Data - Tríplice Viral
            listaDeCadastrosDB.DataDose1TripliceViral = listaDeCadastrosModel.DataDose1TripliceViral;
            listaDeCadastrosDB.DataDose2TripliceViral = listaDeCadastrosModel.DataDose2TripliceViral;

            // Data - Covid-19
            listaDeCadastrosDB.DataDose1Covid = listaDeCadastrosModel.DataDose1Covid;
            listaDeCadastrosDB.DataDose2Covid = listaDeCadastrosModel.DataDose2Covid;
            listaDeCadastrosDB.DataDose3Covid = listaDeCadastrosModel.DataDose3Covid;
            listaDeCadastrosDB.DataDoseReforco1Covid = listaDeCadastrosModel.DataDoseReforco1Covid;
            listaDeCadastrosDB.DataDoseReforco2Covid = listaDeCadastrosModel.DataDoseReforco2Covid;

            // Data - Febre amarela
            listaDeCadastrosDB.DataDoseUnicaFebreAmarela = listaDeCadastrosModel.DataDoseUnicaFebreAmarela;

            // Data - Influenza
            listaDeCadastrosDB.DataDoseAnualInfluenza = listaDeCadastrosModel.DataDoseAnualInfluenza;

            listaDeCadastrosDB.DataAtualizacao = DateTime.Now;

            _bancoContext.ListaDeCadastros.Update(listaDeCadastrosDB);
            _bancoContext.SaveChanges();
            return listaDeCadastrosDB;
        }

        public bool ApagarUsuarioPadrao(int id)
        {
            ListaDeCadastrosModel listaDeCadastrosDB = ListarPorId(id);

            if (listaDeCadastrosDB == null) throw new Exception("Houve um erro ao apagar o cadastro!");

            listaDeCadastrosDB.ApagadoUsuarioPadrao = Enums.ApagadoEnum.Apagado;

            listaDeCadastrosDB.DataApagado = DateTime.Now;

            _bancoContext.ListaDeCadastros.Update(listaDeCadastrosDB); 
            _bancoContext.SaveChanges();

            return true;
        }

        public bool Apagar(int id)
        {
            ListaDeCadastrosModel listaDeCadastrosDB = ListarPorId(id);

            if (listaDeCadastrosDB == null) throw new Exception("Houve um erro ao apagar o cadastro!");

            _bancoContext.ListaDeCadastros.Remove(listaDeCadastrosDB);
            _bancoContext.SaveChanges();

            return true;
        }

                public List<ListaDeCadastrosModel> BuscarComFiltro(int? numMatricula = null, string nome = null, DateTime? dataNascimentoInicio = null,
DateTime? dataNascimentoFim = null, string setor = null, string cargo = null)
        {
            // Começa a consulta base
            var query = _bancoContext.ListaDeCadastros.AsQueryable();

            // Aplica os filtros apenas se os valores forem fornecidos
            if (numMatricula.HasValue)
                query = query.Where(x => x.NumMatricula == numMatricula.Value);

            if (!string.IsNullOrWhiteSpace(nome))
                query = query.Where(x => x.Nome.Contains(nome));

            if (dataNascimentoInicio.HasValue)
                query = query.Where(x => x.DataNascimento >= dataNascimentoInicio.Value);

            if (dataNascimentoFim.HasValue)
                query = query.Where(x => x.DataNascimento <= dataNascimentoFim.Value);

            if (!string.IsNullOrWhiteSpace(setor))
                query = query.Where(x => x.Setor.Contains(setor));

            if (!string.IsNullOrWhiteSpace(cargo))
                query = query.Where(x => x.Cargo.Contains(cargo));

            // Retorna a lista final filtrada
            return query.ToList();
        }

        public bool ExisteCadastroIdentico(ListaDeCadastrosModel novoCadastro)
        {
            return _bancoContext.ListaDeCadastros.Any(c =>
                c.NumMatricula == novoCadastro.NumMatricula &&
                c.Nome == novoCadastro.Nome &&
                c.DataNascimento == novoCadastro.DataNascimento &&
                c.Setor == novoCadastro.Setor &&
                c.Cargo == novoCadastro.Cargo &&
                c.DataDose1HepatiteB == novoCadastro.DataDose1HepatiteB &&
                c.DataDose2HepatiteB == novoCadastro.DataDose2HepatiteB &&
                c.DataDose3HepatiteB == novoCadastro.DataDose3HepatiteB &&
                c.DataDoseReforcoHepatiteB == novoCadastro.DataDoseReforcoHepatiteB &&
                c.DataExameAntiHBS == novoCadastro.DataExameAntiHBS &&
                c.ResultadoExameAntiHBS == novoCadastro.ResultadoExameAntiHBS &&
                c.DataDose1DifteriaTetano == novoCadastro.DataDose1DifteriaTetano &&
                c.DataDose2DifteriaTetano == novoCadastro.DataDose2DifteriaTetano &&
                c.DataDose3DifteriaTetano == novoCadastro.DataDose3DifteriaTetano &&
                c.DataDoseReforcoDifteriaTetano == novoCadastro.DataDoseReforcoDifteriaTetano &&
                c.DataDose1TripliceViral == novoCadastro.DataDose1TripliceViral &&
                c.DataDose2TripliceViral == novoCadastro.DataDose2TripliceViral &&
                c.DataDose1Covid == novoCadastro.DataDose1Covid &&
                c.DataDose2Covid == novoCadastro.DataDose2Covid &&
                c.DataDose3Covid == novoCadastro.DataDose3Covid &&
                c.DataDoseReforco1Covid == novoCadastro.DataDoseReforco1Covid &&
                c.DataDoseReforco2Covid == novoCadastro.DataDoseReforco2Covid &&
                c.DataDoseUnicaFebreAmarela == novoCadastro.DataDoseUnicaFebreAmarela &&
                c.DataDoseAnualInfluenza == novoCadastro.DataDoseAnualInfluenza);
        }

        public bool VerificarAlgumCampoPreenchido(ListaDeCadastrosModel listaDeCadastrosModel)
        {
            return (listaDeCadastrosModel.NumMatricula == null || listaDeCadastrosModel.NumMatricula == 0) &&
                   string.IsNullOrWhiteSpace(listaDeCadastrosModel.Nome) &&
                   listaDeCadastrosModel.DataNascimento == null &&
                   string.IsNullOrWhiteSpace(listaDeCadastrosModel.Setor) &&
                   string.IsNullOrWhiteSpace(listaDeCadastrosModel.Cargo) &&
                   listaDeCadastrosModel.DataDose1HepatiteB == null &&
                   listaDeCadastrosModel.DataDose2HepatiteB == null &&
                   listaDeCadastrosModel.DataDose3HepatiteB == null &&
                   listaDeCadastrosModel.DataDoseReforcoHepatiteB == null &&
                   listaDeCadastrosModel.DataExameAntiHBS == null &&
                   string.IsNullOrWhiteSpace(listaDeCadastrosModel.ResultadoExameAntiHBS) &&
                   listaDeCadastrosModel.DataDose1DifteriaTetano == null &&
                   listaDeCadastrosModel.DataDose2DifteriaTetano == null &&
                   listaDeCadastrosModel.DataDose3DifteriaTetano == null &&
                   listaDeCadastrosModel.DataDoseReforcoDifteriaTetano == null &&
                   listaDeCadastrosModel.DataDose1TripliceViral == null &&
                   listaDeCadastrosModel.DataDose2TripliceViral == null &&
                   listaDeCadastrosModel.DataDose1Covid == null &&
                   listaDeCadastrosModel.DataDose2Covid == null &&
                   listaDeCadastrosModel.DataDose3Covid == null &&
                   listaDeCadastrosModel.DataDoseReforco1Covid == null &&
                   listaDeCadastrosModel.DataDoseReforco2Covid == null &&
                   listaDeCadastrosModel.DataDoseUnicaFebreAmarela == null &&
                   listaDeCadastrosModel.DataDoseAnualInfluenza == null;
        }

        public class DatasMinMax
        {
            public DateTime? MinDataNascimento { get; set; }
            public DateTime? MaxDataNascimento { get; set; }
            public DateTime? MinDataDose1HepatiteB { get; set; }
            public DateTime? MaxDataDose1HepatiteB { get; set; }
            public DateTime? MinDataDose2HepatiteB { get; set; }
            public DateTime? MaxDataDose2HepatiteB { get; set; }
            public DateTime? MinDataDose3HepatiteB { get; set; }
            public DateTime? MaxDataDose3HepatiteB { get; set; }
            public DateTime? MinDataDoseReforcoHepatiteB { get; set; }
            public DateTime? MaxDataDoseReforcoHepatiteB { get; set; }
            public DateTime? MinDataExameAntiHBS { get; set; }
            public DateTime? MaxDataExameAntiHBS { get; set; }
            public DateTime? MinDataDose1DifteriaTetano { get; set; }
            public DateTime? MaxDataDose1DifteriaTetano { get; set; }
            public DateTime? MinDataDose2DifteriaTetano { get; set; }
            public DateTime? MaxDataDose2DifteriaTetano { get; set; }
            public DateTime? MinDataDose3DifteriaTetano { get; set; }
            public DateTime? MaxDataDose3DifteriaTetano { get; set; }
            public DateTime? MinDataDoseReforcoDifteriaTetano { get; set; }
            public DateTime? MaxDataDoseReforcoDifteriaTetano { get; set; }
            public DateTime? MinDataDose1TripliceViral { get; set; }
            public DateTime? MaxDataDose1TripliceViral { get; set; }
            public DateTime? MinDataDose2TripliceViral { get; set; }
            public DateTime? MaxDataDose2TripliceViral { get; set; }
            public DateTime? MinDataDose1Covid { get; set; }
            public DateTime? MaxDataDose1Covid { get; set; }
            public DateTime? MinDataDose2Covid { get; set; }
            public DateTime? MaxDataDose2Covid { get; set; }
            public DateTime? MinDataDose3Covid { get; set; }
            public DateTime? MaxDataDose3Covid { get; set; }
            public DateTime? MinDataDoseReforco1Covid { get; set; }
            public DateTime? MaxDataDoseReforco1Covid { get; set; }
            public DateTime? MinDataDoseReforco2Covid { get; set; }
            public DateTime? MaxDataDoseReforco2Covid { get; set; }
            public DateTime? MinDataDoseUnicaFebreAmarela { get; set; }
            public DateTime? MaxDataDoseUnicaFebreAmarela { get; set; }
            public DateTime? MinDataDoseAnualInfluenza { get; set; }
            public DateTime? MaxDataDoseAnualInfluenza { get; set; }
            public DateTime? MinDataGeral { get; set; }
            public DateTime? MaxDataGeral { get; set; }
        }
        public DatasMinMax ObterMinMaxDatas()
        {
            try
            {
                var servidores = BuscarTodosUsuarioPadrao();


                // Verificar se a lista de servidores está vazia ou nula
                if (servidores == null || !servidores.Any())
                {
                    // Caso não haja dados no banco, retornamos um objeto DatasMinMax com todas as datas nulas
                    // Está apresentando uma mensagem no datable:
                    return new DatasMinMax
                    {
                        MinDataNascimento = null,
                        MaxDataNascimento = null,
                        MinDataDose1HepatiteB = null,
                        MaxDataDose1HepatiteB = null,
                        MinDataDose2HepatiteB = null,
                        MaxDataDose2HepatiteB = null,
                        MinDataDose3HepatiteB = null,
                        MaxDataDose3HepatiteB = null,
                        MinDataDoseReforcoHepatiteB = null,
                        MaxDataDoseReforcoHepatiteB = null,
                        MinDataExameAntiHBS = null,
                        MaxDataExameAntiHBS = null,
                        MinDataDose1DifteriaTetano = null,
                        MaxDataDose1DifteriaTetano = null,
                        MinDataDose2DifteriaTetano = null,
                        MaxDataDose2DifteriaTetano = null,
                        MinDataDose3DifteriaTetano = null,
                        MaxDataDose3DifteriaTetano = null,
                        MinDataDoseReforcoDifteriaTetano = null,
                        MaxDataDoseReforcoDifteriaTetano = null,
                        MinDataDose1TripliceViral = null,
                        MaxDataDose1TripliceViral = null,
                        MinDataDose2TripliceViral = null,
                        MaxDataDose2TripliceViral = null,
                        MinDataDose1Covid = null,
                        MaxDataDose1Covid = null,
                        MinDataDose2Covid = null,
                        MaxDataDose2Covid = null,
                        MinDataDose3Covid = null,
                        MaxDataDose3Covid = null,
                        MinDataDoseReforco1Covid = null,
                        MaxDataDoseReforco1Covid = null,
                        MinDataDoseReforco2Covid = null,
                        MaxDataDoseReforco2Covid = null,
                        MinDataDoseUnicaFebreAmarela = null,
                        MaxDataDoseUnicaFebreAmarela = null,
                        MinDataDoseAnualInfluenza = null,
                        MaxDataDoseAnualInfluenza = null,
                        MinDataGeral = null,
                        MaxDataGeral = null
                    };
                }


                // Unir todas as datas relevantes para calcular o Min e Max de Data Geral
                var todasAsDatas = new List<DateTime?>()
            {
            servidores.Min(m => m.DataNascimento),
            servidores.Min(m => m.DataDose1HepatiteB),
            servidores.Min(m => m.DataDose2HepatiteB),
            servidores.Min(m => m.DataDose3HepatiteB),
            servidores.Min(m => m.DataDoseReforcoHepatiteB),
            servidores.Min(m => m.DataExameAntiHBS),
            servidores.Min(m => m.DataDose1DifteriaTetano),
            servidores.Min(m => m.DataDose2DifteriaTetano),
            servidores.Min(m => m.DataDose3DifteriaTetano),
            servidores.Min(m => m.DataDoseReforcoDifteriaTetano),
            servidores.Min(m => m.DataDose1TripliceViral),
            servidores.Min(m => m.DataDose2TripliceViral),
            servidores.Min(m => m.DataDose1Covid),
            servidores.Min(m => m.DataDose2Covid),
            servidores.Min(m => m.DataDose3Covid),
            servidores.Min(m => m.DataDoseReforco1Covid),
            servidores.Min(m => m.DataDoseReforco2Covid),
            servidores.Min(m => m.DataDoseUnicaFebreAmarela),
            servidores.Min(m => m.DataDoseAnualInfluenza)
            };

                // Remover valores nulos da lista e calcular o min e max
                var datasValidas = todasAsDatas.Where(d => d.HasValue).Select(d => d.Value).ToList();

                // Verificar se há dados válidos para calcular Min e Max (nãp precisa)
                //DateTime? minDataGeral = datasValidas.Any() ? datasValidas.Min() : (DateTime?)null;
                //DateTime? maxDataGeral = datasValidas.Any() ? datasValidas.Max() : (DateTime?)null;

                var minDataGeral = datasValidas.Min();
                var maxDataGeral = datasValidas.Max();

                var datas = new DatasMinMax
                {
                    MinDataNascimento = servidores.Min(m => m.DataNascimento),
                    MaxDataNascimento = servidores.Max(m => m.DataNascimento),
                    MinDataDose1HepatiteB = servidores.Min(m => m.DataDose1HepatiteB),
                    MaxDataDose1HepatiteB = servidores.Max(m => m.DataDose1HepatiteB),
                    MinDataDose2HepatiteB = servidores.Min(m => m.DataDose2HepatiteB),
                    MaxDataDose2HepatiteB = servidores.Max(m => m.DataDose2HepatiteB),
                    MinDataDose3HepatiteB = servidores.Min(m => m.DataDose3HepatiteB),
                    MaxDataDose3HepatiteB = servidores.Max(m => m.DataDose3HepatiteB),
                    MinDataDoseReforcoHepatiteB = servidores.Min(m => m.DataDoseReforcoHepatiteB),
                    MaxDataDoseReforcoHepatiteB = servidores.Max(m => m.DataDoseReforcoHepatiteB),
                    MinDataExameAntiHBS = servidores.Min(m => m.DataExameAntiHBS),
                    MaxDataExameAntiHBS = servidores.Max(m => m.DataExameAntiHBS),
                    MinDataDose1DifteriaTetano = servidores.Min(m => m.DataDose1DifteriaTetano),
                    MaxDataDose1DifteriaTetano = servidores.Max(m => m.DataDose1DifteriaTetano),
                    MinDataDose2DifteriaTetano = servidores.Min(m => m.DataDose2DifteriaTetano),
                    MaxDataDose2DifteriaTetano = servidores.Max(m => m.DataDose2DifteriaTetano),
                    MinDataDose3DifteriaTetano = servidores.Min(m => m.DataDose3DifteriaTetano),
                    MaxDataDose3DifteriaTetano = servidores.Max(m => m.DataDose3DifteriaTetano),
                    MinDataDoseReforcoDifteriaTetano = servidores.Min(m => m.DataDoseReforcoDifteriaTetano),
                    MaxDataDoseReforcoDifteriaTetano = servidores.Max(m => m.DataDoseReforcoDifteriaTetano),
                    MinDataDose1TripliceViral = servidores.Min(m => m.DataDose1TripliceViral),
                    MaxDataDose1TripliceViral = servidores.Max(m => m.DataDose1TripliceViral),
                    MinDataDose2TripliceViral = servidores.Min(m => m.DataDose2TripliceViral),
                    MaxDataDose2TripliceViral = servidores.Max(m => m.DataDose2TripliceViral),
                    MinDataDose1Covid = servidores.Min(m => m.DataDose1Covid),
                    MaxDataDose1Covid = servidores.Max(m => m.DataDose1Covid),
                    MinDataDose2Covid = servidores.Min(m => m.DataDose2Covid),
                    MaxDataDose2Covid = servidores.Max(m => m.DataDose2Covid),
                    MinDataDose3Covid = servidores.Min(m => m.DataDose3Covid),
                    MaxDataDose3Covid = servidores.Max(m => m.DataDose3Covid),
                    MinDataDoseReforco1Covid = servidores.Min(m => m.DataDoseReforco1Covid),
                    MaxDataDoseReforco1Covid = servidores.Max(m => m.DataDoseReforco1Covid),
                    MinDataDoseReforco2Covid = servidores.Min(m => m.DataDoseReforco2Covid),
                    MaxDataDoseReforco2Covid = servidores.Max(m => m.DataDoseReforco2Covid),
                    MinDataDoseUnicaFebreAmarela = servidores.Min(m => m.DataDoseUnicaFebreAmarela),
                    MaxDataDoseUnicaFebreAmarela = servidores.Max(m => m.DataDoseUnicaFebreAmarela),
                    MinDataDoseAnualInfluenza = servidores.Min(m => m.DataDoseAnualInfluenza),
                    MaxDataDoseAnualInfluenza = servidores.Max(m => m.DataDoseAnualInfluenza),
                    MinDataGeral = minDataGeral,
                    MaxDataGeral = maxDataGeral
                };
                return datas;
            }
            catch (Exception ex) 
            {
                // Logar o erro (caso tenha algum mecanismo de log)
                // Console.WriteLine(ex.Message);
                throw new InvalidOperationException("Erro ao obter as datas mínimas e máximas", ex);
            } 
        }
        public List<string> CalcularIdadeDosCadastros()
        {
            // Busca todos os servidores
            var servidores = BuscarTodosUsuarioPadrao();
            var idades = new List<string>();

            foreach (var servidor in servidores)
            {
                if (servidor.DataNascimento != null) // Como DataNascimento é DateTime, não precisa de HasValue
                {
                    var hoje = DateTime.Today;
                    var dataNascimento = servidor.DataNascimento;

                    // Calcular idade em anos
                    int anos = hoje.Year - dataNascimento.Year;
                    if (hoje < dataNascimento.AddYears(anos)) anos--;

                    // Calcular diferença restante em dias
                    int dias = (hoje - dataNascimento.AddYears(anos)).Days;

                    // Adicionar formato dependendo do resultado
                    if (dias == 0)
                    {
                        idades.Add($"{anos} anos");
                    }
                    else
                    {
                        idades.Add($"{anos} anos e {dias} dias");
                    }
                }
                else
                {
                    idades.Add("Data de nascimento não informada");
                }
            }

            return idades;
        }

        //Método auxiliares, para calcular totais

        //Contar total por tipo de vacina (de cada servidor cadastrado)
        public ListaDeCadastrosModel CalcularTotalTiposDeVacinasPorCadastro(ListaDeCadastrosModel listaDeCadastrosModel)
        {
            // Buscar o cadastro pelo ID
            ListaDeCadastrosModel cadastro = ListarPorId(listaDeCadastrosModel.Id);

            if (cadastro == null) throw new Exception("Cadastro não encontrado!");

            // Lista com os indicadores de cada tipo de vacina
            var vacinas = new[]
            {
                // Hepatite B
                listaDeCadastrosModel.DataDose1HepatiteB.HasValue || listaDeCadastrosModel.DataDose2HepatiteB.HasValue || listaDeCadastrosModel.DataDose3HepatiteB.HasValue || listaDeCadastrosModel.DataDoseReforcoHepatiteB.HasValue,        
                // Exame Anti-HBS (Hepatite B) - conta como uma vacina distinta (é um exame na verdade)
                listaDeCadastrosModel.DataExameAntiHBS.HasValue,
                // Difteria e Tétano
                listaDeCadastrosModel.DataDose1DifteriaTetano.HasValue || listaDeCadastrosModel.DataDose2DifteriaTetano.HasValue || listaDeCadastrosModel.DataDose3DifteriaTetano.HasValue || listaDeCadastrosModel.DataDoseReforcoDifteriaTetano.HasValue,        
                // Tríplice Viral
                listaDeCadastrosModel.DataDose1TripliceViral.HasValue || listaDeCadastrosModel.DataDose2TripliceViral.HasValue,
                // Covid-19
                listaDeCadastrosModel.DataDose1Covid.HasValue || listaDeCadastrosModel.DataDose2Covid.HasValue || listaDeCadastrosModel.DataDose3Covid.HasValue || listaDeCadastrosModel.DataDoseReforco1Covid.HasValue || listaDeCadastrosModel.DataDoseReforco2Covid.HasValue,
                // Febre Amarela
                listaDeCadastrosModel.DataDoseUnicaFebreAmarela.HasValue,
                // Influenza
                listaDeCadastrosModel.DataDoseAnualInfluenza.HasValue
            }; // .Count(vacina => vacina); // Conta quantos valores são "true"

            // Total de tipos de vacinas: (todas as categorias possíveis)
            cadastro.TotalTiposDeVacinas =  vacinas.Length;

            // Total de vacinas aplicadas 
            cadastro.TotalTiposDeVacinasAplicadas = vacinas.Count(v => v);

            // Total de vacinas faltantes
            cadastro.TotalTiposDeVacinasFaltando = cadastro.TotalTiposDeVacinas - cadastro.TotalTiposDeVacinasAplicadas;

            // Salvar as mudanças no banco de dados
            _bancoContext.ListaDeCadastros.Update(cadastro);
            _bancoContext.SaveChanges();

            return cadastro;
        }

        public void CalcularTotalTiposDeVacinasTodosCadastros()
        {
            //List<ListaDeCadastrosModel> cadastros = BuscarTodos(); // Obtém todos os cadastros

            List<ListaDeCadastrosModel> cadastros = BuscarTodosUsuarioPadrao(); 

            foreach (ListaDeCadastrosModel cadastro in cadastros)
            {
                CalcularTotalTiposDeVacinasPorCadastro(cadastro); // Recalcula para cada cadastro
            }
        }

        //Contar total de todas as doses de todos os tipos de vacinas(de cada servidor cadastrado)
        public ListaDeCadastrosModel CalcularTotalDosesPorCadastro(ListaDeCadastrosModel listaDeCadastrosModel)
         {
                // Buscar o cadastro pelo ID
                ListaDeCadastrosModel cadastro = ListarPorId(listaDeCadastrosModel.Id);

                if (cadastro == null) throw new Exception("Cadastro não encontrado!");

            // Calcular doses para cada tipo de vacina

            // Hepatite B
            var dosesHepatiteB = new[]
                {
                    listaDeCadastrosModel.DataDose1HepatiteB,
                    listaDeCadastrosModel.DataDose2HepatiteB,
                    listaDeCadastrosModel.DataDose3HepatiteB,
                    listaDeCadastrosModel.DataDoseReforcoHepatiteB
                };
                cadastro.TotalDosesHepatiteB = dosesHepatiteB.Length;
                cadastro.TotalDosesAplicadasHepatiteB = dosesHepatiteB.Count(d => d.HasValue);
                cadastro.TotalDosesFaltandoHepatiteB = cadastro.TotalDosesHepatiteB - cadastro.TotalDosesAplicadasHepatiteB;

                // Exame Anti HBS (Hepatite B)
                var exameAntiHBS = new[] { listaDeCadastrosModel.DataExameAntiHBS };
                cadastro.TotalDosesExameAntiHBS = exameAntiHBS.Length;
                cadastro.TotalDosesAplicadasExameAntiHBS = exameAntiHBS.Count(d => d.HasValue);
                cadastro.TotalDosesFaltandoExameAntiHBS = cadastro.TotalDosesExameAntiHBS - cadastro.TotalDosesAplicadasExameAntiHBS;

                // Difteria e Tétano
                var dosesDifteriaTetano = new[]
                {
                    listaDeCadastrosModel.DataDose1DifteriaTetano,
                    listaDeCadastrosModel.DataDose2DifteriaTetano,
                    listaDeCadastrosModel.DataDose3DifteriaTetano,
                    listaDeCadastrosModel.DataDoseReforcoDifteriaTetano
                };
                cadastro.TotalDosesDifteriaTetano = dosesDifteriaTetano.Length;
                cadastro.TotalDosesAplicadasDifteriaTetano = dosesDifteriaTetano.Count(d => d.HasValue);
                cadastro.TotalDosesFaltandoDifteriaTetano = cadastro.TotalDosesDifteriaTetano - cadastro.TotalDosesAplicadasDifteriaTetano;

                // Tríplice Viral
                var dosesTripliceViral = new[]
                {
                    listaDeCadastrosModel.DataDose1TripliceViral,
                    listaDeCadastrosModel.DataDose2TripliceViral
                };
                cadastro.TotalDosesTripliceViral = dosesTripliceViral.Length;
                cadastro.TotalDosesAplicadasTripliceViral = dosesTripliceViral.Count(d => d.HasValue);
                cadastro.TotalDosesFaltandoTripliceViral = cadastro.TotalDosesTripliceViral - cadastro.TotalDosesAplicadasTripliceViral;

                // Covid-19
                var dosesCovid = new[]
                {
                    listaDeCadastrosModel.DataDose1Covid,
                    listaDeCadastrosModel.DataDose2Covid,
                    listaDeCadastrosModel.DataDose3Covid,
                    listaDeCadastrosModel.DataDoseReforco1Covid,
                    listaDeCadastrosModel.DataDoseReforco2Covid
                };
                cadastro.TotalDosesCovid = dosesCovid.Length;
                cadastro.TotalDosesAplicadasCovid = dosesCovid.Count(d => d.HasValue);
                cadastro.TotalDosesFaltandoCovid = cadastro.TotalDosesCovid - cadastro.TotalDosesAplicadasCovid;

                // Febre Amarela
                var dosesFebreAmarela = new[]
                {
                    listaDeCadastrosModel.DataDoseUnicaFebreAmarela
                };
                cadastro.TotalDosesFebreAmarela = dosesFebreAmarela.Length;
                cadastro.TotalDosesAplicadasFebreAmarela = dosesFebreAmarela.Count(d => d.HasValue);
                cadastro.TotalDosesFaltandoFebreAmarela = cadastro.TotalDosesFebreAmarela - cadastro.TotalDosesAplicadasFebreAmarela;

                // Influenza
                var dosesInfluenza = new[]
                {
                    listaDeCadastrosModel.DataDoseAnualInfluenza
                };
                cadastro.TotalDosesInfluenza = dosesInfluenza.Length;
                cadastro.TotalDosesAplicadasInfluenza = dosesInfluenza.Count(dose => dose.HasValue);
                cadastro.TotalDosesFaltandoInfluenza = cadastro.TotalDosesInfluenza - cadastro.TotalDosesAplicadasInfluenza;

                //Calcular para todas as doses de todas as vacinas

                // Lista com todas as doses possíveis
                var doses = new[]
                {
                    listaDeCadastrosModel.DataDose1HepatiteB,
                    listaDeCadastrosModel.DataDose2HepatiteB,
                    listaDeCadastrosModel.DataDose3HepatiteB,
                    listaDeCadastrosModel.DataDoseReforcoHepatiteB,

                    listaDeCadastrosModel.DataExameAntiHBS,  // Conta como vacina distinta

                    listaDeCadastrosModel.DataDose1DifteriaTetano,
                    listaDeCadastrosModel.DataDose2DifteriaTetano,
                    listaDeCadastrosModel.DataDose3DifteriaTetano,
                    listaDeCadastrosModel.DataDoseReforcoDifteriaTetano,

                    listaDeCadastrosModel.DataDose1TripliceViral,
                    listaDeCadastrosModel.DataDose2TripliceViral,

                    listaDeCadastrosModel.DataDose1Covid,
                    listaDeCadastrosModel.DataDose2Covid,
                    listaDeCadastrosModel.DataDose3Covid,
                    listaDeCadastrosModel.DataDoseReforco1Covid,
                    listaDeCadastrosModel.DataDoseReforco2Covid,

                    listaDeCadastrosModel.DataDoseUnicaFebreAmarela,
                    listaDeCadastrosModel.DataDoseAnualInfluenza
                };
                cadastro.TotalDoses = doses.Length;// Total de doses (contando todas as doses possíveis)
                cadastro.TotalDosesAplicadas = doses.Count(d => d.HasValue);// Total de doses aplicadas (contando as doses com valor diferente de null)
                cadastro.TotalDosesFaltando = cadastro.TotalDoses - cadastro.TotalDosesAplicadas;// Total de doses faltantes (calculando a diferença entre doses totais e aplicadas)

                if (cadastro.TotalDosesAplicadas == cadastro.TotalDoses)
                {
                    cadastro.SituacaoVacinacao = Enums.SituacaoVacinacaoEnum.VacinacaoCompleta;
                }
                else if (cadastro.TotalDosesFaltando == cadastro.TotalDoses)
                {
                cadastro.SituacaoVacinacao = Enums.SituacaoVacinacaoEnum.VacinacaoAusente;
                }
                else if ((cadastro.TotalDosesAplicadas > 0) && (cadastro.TotalDosesAplicadas < cadastro.TotalDoses))
                    {
                    cadastro.SituacaoVacinacao = Enums.SituacaoVacinacaoEnum.VacinacaoParcial;
                }

                // Salvar as mudanças no banco de dados
                _bancoContext.ListaDeCadastros.Update(cadastro);
                _bancoContext.SaveChanges();

                return cadastro;
        }

        public void CalcularTotalDosesTodosCadastros()
        {
            //List<ListaDeCadastrosModel> cadastros = BuscarTodos(); // Obtém todos os cadastros

            List<ListaDeCadastrosModel> cadastros = BuscarTodosUsuarioPadrao(); // Obtém todos os cadastros            

            foreach (ListaDeCadastrosModel cadastro in cadastros)
            {
                CalcularTotalDosesPorCadastro(cadastro); // Recalcula para cada cadastro
            }
        }

        // Cálcular totais por vacina do banco inteiro
        // Classe para totais por vacina
        public class TotaisPorDose
        {
            public int TotalDose1HepatiteB { get; set; }
            public int TotalDose2HepatiteB { get; set; }
            public int TotalDose3HepatiteB { get; set; }
            public int TotalReforcoHepatiteB { get; set; }
            public int TotalExameAntiHBS { get; set; }
            public int TotalResultadoAntiHBS { get; set; }
            public int TotalDose1DifteriaTetano { get; set; }
            public int TotalDose2DifteriaTetano { get; set; }
            public int TotalDose3DifteriaTetano { get; set; }
            public int TotalReforcoDifteriaTetano { get; set; }
            public int TotalDose1TripliceViral { get; set; }
            public int TotalDose2TripliceViral { get; set; }
            public int TotalDose1Covid { get; set; }
            public int TotalDose2Covid { get; set; }
            public int TotalDose3Covid { get; set; }
            public int TotalReforco1Covid { get; set; }
            public int TotalReforco2Covid { get; set; }
            public int TotalDoseUnicaFebreAmarela { get; set; }
            public int TotalDoseAnualInfluenza { get; set; }
        }
        public TotaisPorDose CalcularTotaisPorDose()
        {
            // Obtém todos os registros
            //List<ListaDeCadastrosModel> servidores = BuscarTodos();

            List<ListaDeCadastrosModel> servidores = BuscarTodosUsuarioPadrao();            

            // Inicializa os totais
            var totais = new TotaisPorDose
            {
                // Hepatite B
                TotalDose1HepatiteB = servidores.Count(m => m.DataDose1HepatiteB.HasValue),
                TotalDose2HepatiteB = servidores.Count(m => m.DataDose2HepatiteB.HasValue),
                TotalDose3HepatiteB = servidores.Count(m => m.DataDose3HepatiteB.HasValue),
                TotalReforcoHepatiteB = servidores.Count(m => m.DataDoseReforcoHepatiteB.HasValue),

                // Exame Anti-HBS
                TotalExameAntiHBS = servidores.Count(m => m.DataExameAntiHBS.HasValue),
                TotalResultadoAntiHBS = servidores.Count(m => !string.IsNullOrEmpty(m.ResultadoExameAntiHBS)),

                // Difteria e Tétano
                TotalDose1DifteriaTetano = servidores.Count(m => m.DataDose1DifteriaTetano.HasValue),
                TotalDose2DifteriaTetano = servidores.Count(m => m.DataDose2DifteriaTetano.HasValue),
                TotalDose3DifteriaTetano = servidores.Count(m => m.DataDose3DifteriaTetano.HasValue),
                TotalReforcoDifteriaTetano = servidores.Count(m => m.DataDoseReforcoDifteriaTetano.HasValue),

                // Tríplice Viral
                TotalDose1TripliceViral = servidores.Count(m => m.DataDose1TripliceViral.HasValue),
                TotalDose2TripliceViral = servidores.Count(m => m.DataDose2TripliceViral.HasValue),

                // Covid
                TotalDose1Covid = servidores.Count(m => m.DataDose1Covid.HasValue),
                TotalDose2Covid = servidores.Count(m => m.DataDose2Covid.HasValue),
                TotalDose3Covid = servidores.Count(m => m.DataDose3Covid.HasValue),
                TotalReforco1Covid = servidores.Count(m => m.DataDoseReforco1Covid.HasValue),
                TotalReforco2Covid = servidores.Count(m => m.DataDoseReforco2Covid.HasValue),

                // Febre Amarela
                TotalDoseUnicaFebreAmarela = servidores.Count(m => m.DataDoseUnicaFebreAmarela.HasValue),

                // Influenza
                TotalDoseAnualInfluenza = servidores.Count(m => m.DataDoseAnualInfluenza.HasValue)
            };

            return totais;
        }

        public class TotaisPorVacina
        {
            public int TotalHepatiteB { get; set; }
            public int TotalExameAntiHBS { get; set; }
            public int TotalDifteriaTetano { get; set; }
            public int TotalTripliceViral { get; set; }
            public int TotalCovid { get; set; }
            public int TotalFebreAmarela { get; set; }
            public int TotalInfluenza { get; set; }
        }

        public TotaisPorVacina CalcularTotaisPorVacina()
        {
            //List<ListaDeCadastrosModel> servidores = BuscarTodos(); // Obtém todos os cadastros

            List<ListaDeCadastrosModel> servidores = BuscarTodosUsuarioPadrao();             

            var totais = new TotaisPorVacina();

            // Total Hepatite B
            totais.TotalHepatiteB = servidores.Sum(m => new[]
            {
                m.DataDose1HepatiteB,
                m.DataDose2HepatiteB,
                m.DataDose3HepatiteB,
                m.DataDoseReforcoHepatiteB
            }.Count(d => d.HasValue));

            // Total Exame Anti-HBS
            totais.TotalExameAntiHBS = servidores.Count(m => m.DataExameAntiHBS.HasValue);

            // Total Difteria e Tétano
            totais.TotalDifteriaTetano = servidores.Sum(m => new[]
            {
                m.DataDose1DifteriaTetano,
                m.DataDose2DifteriaTetano,
                m.DataDose3DifteriaTetano,
                m.DataDoseReforcoDifteriaTetano
            }.Count(d => d.HasValue));

            // Total Tríplice Viral
            totais.TotalTripliceViral = servidores.Sum(m => new[]
            {
                m.DataDose1TripliceViral,
                m.DataDose2TripliceViral
            }.Count(d => d.HasValue));

            // Total Covid
            totais.TotalCovid = servidores.Sum(m => new[]
            {
                m.DataDose1Covid,
                m.DataDose2Covid,
                m.DataDose3Covid,
                m.DataDoseReforco1Covid,
                m.DataDoseReforco2Covid
            }.Count(d => d.HasValue));

            // Total Febre Amarela
            totais.TotalFebreAmarela = servidores.Count(m => m.DataDoseUnicaFebreAmarela.HasValue);

            // Total Influenza
            totais.TotalInfluenza = servidores.Count(m => m.DataDoseAnualInfluenza.HasValue);

            return totais;
        }

        public class BalancoVacinacao
        {
            public int totalServidoresCadastrados { get; set; }
            public int totalVacinadosCompletamente { get; set; }
            public int totalVacinadosParcialmente { get; set; }
            public int totalNaoVacinados { get; set; }
        }

        public BalancoVacinacao CalcularBalancoDeVacinacaoDosServidores()
        {
            //List<ListaDeCadastrosModel> servidores = BuscarTodos(); // Obtém todos os cadastros

            List<ListaDeCadastrosModel> servidores = BuscarTodosUsuarioPadrao();            

            var balanco = new BalancoVacinacao
            {
            totalServidoresCadastrados = servidores.Count,

            totalVacinadosCompletamente = servidores.Count(m =>
                new[]
                {
                    m.DataDose1HepatiteB, m.DataDose2HepatiteB, m.DataDose3HepatiteB, m.DataDoseReforcoHepatiteB,
                    m.DataExameAntiHBS,
                    m.DataDose1DifteriaTetano, m.DataDose2DifteriaTetano, m.DataDose3DifteriaTetano, m.DataDoseReforcoDifteriaTetano,
                    m.DataDose1TripliceViral, m.DataDose2TripliceViral,
                    m.DataDose1Covid, m.DataDose2Covid, m.DataDose3Covid, m.DataDoseReforco1Covid, m.DataDoseReforco2Covid,
                    m.DataDoseUnicaFebreAmarela, 
                    m.DataDoseAnualInfluenza
                }.All(d => d.HasValue)),

            totalVacinadosParcialmente = servidores.Count(m =>
                new[]
                {
                    m.DataDose1HepatiteB, m.DataDose2HepatiteB, m.DataDose3HepatiteB, m.DataDoseReforcoHepatiteB,
                    m.DataExameAntiHBS,
                    m.DataDose1DifteriaTetano, m.DataDose2DifteriaTetano, m.DataDose3DifteriaTetano, m.DataDoseReforcoDifteriaTetano,
                    m.DataDose1TripliceViral, m.DataDose2TripliceViral,
                    m.DataDose1Covid, m.DataDose2Covid, m.DataDose3Covid, m.DataDoseReforco1Covid, m.DataDoseReforco2Covid,
                    m.DataDoseUnicaFebreAmarela,
                    m.DataDoseAnualInfluenza
                }.Any(d => d.HasValue) &&
                !new[]
                {
                    m.DataDose1HepatiteB, m.DataDose2HepatiteB, m.DataDose3HepatiteB, m.DataDoseReforcoHepatiteB,
                    m.DataExameAntiHBS,
                    m.DataDose1DifteriaTetano, m.DataDose2DifteriaTetano, m.DataDose3DifteriaTetano, m.DataDoseReforcoDifteriaTetano,
                    m.DataDose1TripliceViral, m.DataDose2TripliceViral,
                    m.DataDose1Covid, m.DataDose2Covid, m.DataDose3Covid, m.DataDoseReforco1Covid, m.DataDoseReforco2Covid,
                    m.DataDoseUnicaFebreAmarela, 
                    m.DataDoseAnualInfluenza
                }.All(d => d.HasValue)),

            totalNaoVacinados = servidores.Count(m =>
                new[]
                {
                    m.DataDose1HepatiteB, m.DataDose2HepatiteB, m.DataDose3HepatiteB, m.DataDoseReforcoHepatiteB,
                    m.DataExameAntiHBS,
                    m.DataDose1DifteriaTetano, m.DataDose2DifteriaTetano, m.DataDose3DifteriaTetano, m.DataDoseReforcoDifteriaTetano,
                    m.DataDose1TripliceViral, m.DataDose2TripliceViral,
                    m.DataDose1Covid, m.DataDose2Covid, m.DataDose3Covid, m.DataDoseReforco1Covid, m.DataDoseReforco2Covid,
                    m.DataDoseUnicaFebreAmarela,
                    m.DataDoseAnualInfluenza
                }.All(d => !d.HasValue))
            };

            return balanco;
        }
    }
}