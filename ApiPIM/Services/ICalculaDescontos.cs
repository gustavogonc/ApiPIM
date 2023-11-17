namespace ApiPIM.Services
{
    public interface ICalculaDescontos
    {
        public abstract decimal CalculaInss(decimal salario);
        public abstract decimal CalculaFGTS(decimal salario);
        public abstract decimal CalculaIrrf(decimal salario, decimal descontoInss);
    }
}
