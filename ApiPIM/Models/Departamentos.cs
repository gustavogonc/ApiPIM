using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ApiPIM.Models
{
    [Table("tb_departamento")]
    public class Departamentos
    {
        [Key]
        public int? id_departamento { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "O nome deve ter entre 5 e 100 caracteres", MinimumLength = 5)]
        public string? nome_departamento { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "A descrição precisa ter entre 15 e 100 caracteres.", MinimumLength = 10)]
        public string? descricao_departamento { get; set; }

        public DateTime? data_criacao { get; set; }

    }
}
