using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiPIM.Models
{
    [Table("usuarios")]
    public class Usuarios
    {
        [Key]
        public int usuarioId { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "O nome deve ter entre 5 e 20 caracteres", MinimumLength = 5)]
        public string? nome { get; set; }

        [Required]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Informe um email válido...")]
        public string? email { get; set; }


        [Required]
        [DataType(DataType.Password)]
        public string? senha { get; set; }

        public Int16? ativo { get; set; }
        public Int16? administrador { get; set; }
        public string? token { get; set; }
        public string? expiration_token { get; set; }
    }
}
