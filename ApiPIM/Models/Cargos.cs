using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiPIM.Models
{
    [Table("tb_cargos")]
    public class Cargos
    {
        [Key]
        public int? id_cargo { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "O título do cargo deve ter entre 5 e 100 caracteres", MinimumLength = 5)]
        public string nome_cargo { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "A descrição do cargo deve ter entre 15 e 100 caracteres", MinimumLength = 15)]
        public string descricao_cargo { get; set; }
        public decimal salario { get; set; }

        [ForeignKey("DepartamentoId")]
        public int? DepartamentoId { get; set; }
        public Departamentos? Departamento { get; set; }

    }
}
