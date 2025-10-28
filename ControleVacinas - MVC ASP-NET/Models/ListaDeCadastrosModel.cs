using ControleVacinas___MVC_ASP_NET.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControleVacinas___MVC_ASP_NET.Models
{
    public class ListaDeCadastrosModel
    {
        // Atributos (colunas do BD)

        // Id - Código do cadastro
        // Cada novo cadastro inserido vai ser gerado um código sequencial automaticamene        
        public int Id { get; set; }

        // Dados do servidor ativo
        [Required(ErrorMessage = "O Código Matrícula é obrigatório!")]
        //[Required(ErrorMessage = "O Código Matrícula informado é inválido!")]
        public int NumMatricula { get; set; }

        [Required(ErrorMessage = "O Nome é obrigatório!")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "A Data de Nascimento é obrigatória!")]
        public DateTime DataNascimento { get; set; }

        [Required(ErrorMessage = "O nome do Setor obrigatório!")]
        public string Setor { get; set; }

        [Required(ErrorMessage = "O nome do Cargo obrigatório!")]
        public string Cargo { get; set; }
        public SituacaoVacinacaoEnum? SituacaoVacinacao { get; set; }        

        // Data das doses das vacinas

        // Data - Hepatite B (HB)
        public DateTime? DataDose1HepatiteB { get; set; }
        public DateTime? DataDose2HepatiteB { get; set; }
        public DateTime? DataDose3HepatiteB { get; set; }
        public DateTime? DataDoseReforcoHepatiteB { get; set; }

        // Data - Exame Anti-HBS (Hepatite B)
        public DateTime? DataExameAntiHBS { get; set; }
        public string? ResultadoExameAntiHBS { get; set; }

        // Data - Difteria e Tétano (DT) B
        public DateTime? DataDose1DifteriaTetano { get; set; }
        public DateTime? DataDose2DifteriaTetano { get; set; }
        public DateTime? DataDose3DifteriaTetano { get; set; }
        public DateTime? DataDoseReforcoDifteriaTetano { get; set; }

        // Data - Tríplice Viral
        public DateTime? DataDose1TripliceViral { get; set; }
        public DateTime? DataDose2TripliceViral { get; set; }

        // Data - Covid-19
        public DateTime? DataDose1Covid { get; set; }
        public DateTime? DataDose2Covid { get; set; }
        public DateTime? DataDose3Covid { get; set; }
        public DateTime? DataDoseReforco1Covid { get; set; }
        public DateTime? DataDoseReforco2Covid { get; set; }

        // Data - Febre amarela
        public DateTime? DataDoseUnicaFebreAmarela { get; set; }

        // Data - Influenza
        public DateTime? DataDoseAnualInfluenza { get; set; }

        // Informações dos cadastros
        public DateTime? DataCadastro { get; set; }
        public DateTime? DataAtualizacao { get; set; }
        public ApagadoEnum? ApagadoUsuarioPadrao { get; set; }
        public DateTime? DataApagado { get; set; }
        public int? UsuarioId { get; set; }
        public ListaDeUsuariosModel? Usuario { get; set; }

        // Calcular totais

        public int? TotalTiposDeVacinas { get; set; }
        public int? TotalTiposDeVacinasAplicadas { get; set; }
        public int? TotalTiposDeVacinasFaltando { get; set; }

        public int? TotalDoses { get; set; }
        public int? TotalDosesAplicadas { get; set; }
        public int? TotalDosesFaltando { get; set; }

        public int? TotalDosesHepatiteB { get; set; }
        public int? TotalDosesAplicadasHepatiteB { get; set; }
        public int? TotalDosesFaltandoHepatiteB { get; set; }

        public int? TotalDosesExameAntiHBS { get; set; }
        public int? TotalDosesAplicadasExameAntiHBS { get; set; }
        public int? TotalDosesFaltandoExameAntiHBS { get; set; }

        public int? TotalDosesDifteriaTetano { get; set; }
        public int? TotalDosesAplicadasDifteriaTetano { get; set; }
        public int? TotalDosesFaltandoDifteriaTetano { get; set; }

        public int? TotalDosesTripliceViral { get; set; }
        public int? TotalDosesAplicadasTripliceViral { get; set; }
        public int? TotalDosesFaltandoTripliceViral { get; set; }

        public int? TotalDosesCovid { get; set; }
        public int? TotalDosesAplicadasCovid { get; set; }
        public int? TotalDosesFaltandoCovid { get; set; }

        public int? TotalDosesFebreAmarela { get; set; }
        public int? TotalDosesAplicadasFebreAmarela { get; set; }
        public int? TotalDosesFaltandoFebreAmarela { get; set; }

        public int? TotalDosesInfluenza { get; set; }
        public int? TotalDosesAplicadasInfluenza { get; set; }
        public int? TotalDosesFaltandoInfluenza { get; set; }
    }
}
