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
        public string? tipo_endereco { get; set; }
        public string? num_endereco { get; set; }
        public string? bairro { get; set; }
        public string? cep { get; set; }
        public string? cidade { get; set; }
        public string? uf_estado { get; set; }

        [ForeignKey("Funcionarios")]
        public int? funcionario_id { get; set; }
        public Funcionarios? Funcionarios { get; set; }
        public DateTime? data_cadastro { get; set; }
    }
}
