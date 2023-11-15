using ApiPIM.Context;
using ApiPIM.Models;

namespace ApiPIM.Repository
{
    public class AppRepository : IAppRepository
    {
        private readonly AppDbContext _context;
        public AppRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IQueryable> RetornaMesesFuncionario(int id)
        {
            var lista = from calculos in _context.ValoresPagamentos
                        where calculos.id_funcionario == id
                        orderby Convert.ToInt32(calculos.ano), Convert.ToInt32(calculos.mes) descending
                        select (new
                        {
                            calculos.mes,
                            calculos.ano,
                            calculos.valor_liquido,
                            calculos.total_descontos,
                            calculos.total_proventos
                        });

            return lista;
        }
    }
}
