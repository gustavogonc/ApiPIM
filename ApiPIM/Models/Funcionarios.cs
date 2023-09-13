using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ApiPIM.Models
{
    [Table("funcionario")]
    public class Funcionarios
    {
        public Funcionarios()
        {
            endereco = new Collection<Endereco>();
        }
        [Key]
        public int? id_funcionario { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "O nome deve ter entre 10 e 100 caracteres", MinimumLength = 10)]
        public string? nome_funcionario { get; set; }

        [Required]
        [StringLength(15, ErrorMessage = "O CPF precisa ter 15 dígitos incluindo pontos e traços")]
        public string? cpf { get; set; }

        [Required]
        public string? cargo { get; set; }

        [Required]
        public string? departamento { get; set; }

        [Required]
        public DateTime? data_cont { get; set; }

        public string? inf_cont { get; set; }

        [Required]
        public string? sexo { get; set; }

        [Required]
        public string? estado_civil { get; set; }


        public ICollection<Endereco>? endereco {get;set;}
    }
}
