using ApiPIM.Models;

namespace ApiPIM.Services
{
    public class CalculaDescontos : ICalculaDescontos
    {
        public decimal CalculaInss(decimal salario)
        {
            //if (salario <= 1100.00M) return salario * 0.075M;
            //if (salario <= 2203.48M) return salario * 0.09M;
            //if (salario <= 3305.22M) return salario * 0.12M;
            //if (salario <= 6433.57M) return salario * 0.14M;
            //return salario * 0.14M;

            if (salario > 1320.01M && salario < 2571.29M)
            {
              return salario * 0.09M;

            }
            else if (salario > 2571.30M && salario < 3856.94M)
            {
                return salario * 0.12M;
            }
            else if (salario > 3856.95M)
            {
                return salario * 0.14M;
            }
            else
            {
                return salario * 0.075M;
            }
        }

        //public decimal CalculaIrrf(decimal salario, decimal descontoInss)
        //{
        //    var baseCalculo = salario - descontoInss;

        //    if (baseCalculo <= 1903.98M) return 0M;
        //    if (baseCalculo <= 2826.65M) return baseCalculo * 0.075M - 142.80M;
        //    if (baseCalculo <= 3751.05M) return baseCalculo * 0.15M - 354.80M;
        //    return baseCalculo * 0.15M - 354.80M;
        //}

        public decimal CalculaIrrf(decimal salario, decimal descontoInss)
        {
            if (salario > 2112.01M && salario < 2826.66M)
            {
                return  (salario - descontoInss) * 0.075M - 158.40M;

            }
            else if (salario > 2826.65M && salario < 3751.06M)
            {
                return (salario - descontoInss) * 0.15M - 370.40M;
            }
            else if (salario > 3751.05M && salario < 4664.69M)
            {
                return (salario - descontoInss) * 0.225M - 651.73M;
            }
            else if (salario > 4664.68M)
            {
                return (salario - descontoInss) * 0.275M - 884.96M;
            }
            else
            {
               return 0;
            }
        }

        public decimal CalculaFGTS(decimal salario)
        {
            return salario * 0.08M;
        }

        
    }
}
