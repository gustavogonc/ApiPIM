using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ApiPIM.Models
{
    [Table("tb_endereco")]
    public class Endereco
    {
        [Key]
        public int? id_endereco { get; set; }
        public string? rua { get; set; }
        public byte? num_end { get; set; }
        public string? bairro { get; set; }
        public string? CEP { get; set; }
        public string? telefone { get; set; }
        public string? celular { get; set; }

        [ForeignKey("Funcionarios")]
        public int? id_funcionario { get; set; }
        public Funcionarios? Funcionarios { get; set; }
    }
}
