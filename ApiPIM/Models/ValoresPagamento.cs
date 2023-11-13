using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiPIM.Models
{
    [Table("tb_valores_pagamento")]
    public class ValoresPagamento
    {
        [Key]
        public int? id { get; set; }
        public int? id_funcionario { get; set; }
        public decimal? total_proventos { get; set; }
        public decimal? total_descontos { get; set; }
        public decimal? valor_liquido { get; set; }
        public string? mes { get; set; }
        public string? ano { get; set; }
        public DateTime? data_pagamento { get; set; }
    }
}
