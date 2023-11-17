namespace ApiPIM.Models
{
    public class HistPagamentoModel
    {
        public int IdHist { get; set; }
        public int IdFuncionario { get; set; }
        public DateTime DataPagamento { get; set; }
        public decimal Valor { get; set; }
        
        public string Texto { get; set; }
    }

}
