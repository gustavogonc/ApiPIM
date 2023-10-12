using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiPIM.Models
{
    [Table("tb_contato_funcionario")]
    public class ContatoFuncionario
    {
        [Key]
        public int? id_contato { get; set; }
        public string? tipo_telefone { get; set; }
        public string? numero_contato { get; set; }
        public DateTime? data_cadastro { get; set; }

        [ForeignKey("Funcionarios")]
        public int? funcionario_id { get; set; }
        public Funcionarios? Funcionarios { get; set; }

    }
}
