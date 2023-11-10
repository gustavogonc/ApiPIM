using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiPIM.Models
{
    [Table("tb_info_pagamento")]
    public class ProventosModel
    {
        [Key]
        public int? id { get; set; }
        public int? id_funcionario { get; set; }
        public string? nome_valor { get; set; }
        public string? tipo_valor { get; set; }
        public decimal? valor { get; set; }
        public string? mes { get; set; }
        public string? ano  { get; set; }
    }
}
