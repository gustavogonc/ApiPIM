namespace ApiPIM.Models
{
    public class DetalhesFuncionarioModel
    {
        public string NomeFuncionario { get; set; }
        public string Departamento { get; set; }
        public string Cargo { get; set; }
        public decimal Salario { get; set; }
        public DateTime DataContratacao { get; set; }
        public decimal DescontoINSS { get; set; }
        public decimal DescontoIRRF { get; set; }
    }
}
