namespace ApiPIM.Services
{
    public class CalculaDescontos : ICalculaDescontos
    {
        public decimal CalculaInss(decimal salario)
        {
            if (salario <= 1100.00M) return salario * 0.075M;
            if (salario <= 2203.48M) return salario * 0.09M;
            if (salario <= 3305.22M) return salario * 0.12M;
            if (salario <= 6433.57M) return salario * 0.14M;
            return salario * 0.14M; 
        }

        public decimal CalculaIrrf(decimal salario, decimal descontoInss)
        {
            var baseCalculo = salario - descontoInss;

            if (baseCalculo <= 1903.98M) return 0M;
            if (baseCalculo <= 2826.65M) return baseCalculo * 0.075M - 142.80M;
            if (baseCalculo <= 3751.05M) return baseCalculo * 0.15M - 354.80M;
            return baseCalculo * 0.15M - 354.80M;
        }
    }
}
