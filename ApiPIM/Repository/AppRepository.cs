using ApiPIM.Context;
using ApiPIM.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiPIM.Repository
{
    public class AppRepository : IAppRepository
    {
        private readonly AppDbContext _context;
        public AppRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<object>> RetornaMesesFuncionario(int id)
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

            return await lista.ToListAsync();
        }

        public async Task<IEnumerable<object>> RetornaDetalhesMeses(DetalhesPagamentoFuncionario model)
        {
            var lista = from valores in _context.ValoresPagamentos
                        join p in _context.Proventos on valores.id_funcionario equals p.id_funcionario
                        where p.id_funcionario == model.id_funcionario && p.mes == model.mes && p.ano == model.ano
                        select (new
                        {
                            valores.total_proventos,
                            valores.total_descontos,
                            valores.valor_liquido,
                            valores.ano,
                            valores.mes,
                            valores.data_pagamento,
                            p.nome_valor,
                            p.tipo_valor,
                            p.valor,
                        });

            var resultadoAgrupado = lista.Where(v => (v.ano == model.ano) && (v.mes == model.mes)).GroupBy(v => v.mes).Select(g => new
            {
                Mes = new
                {
                    mes = g.First().mes,
                    ano = g.First().ano,
                    total_descontos = g.First().total_descontos,
                    total_proventos = g.First().total_proventos,
                    valor_liquido = g.First().valor_liquido,
                    data_pagamento = DateTime.Parse(g.First().data_pagamento.ToString()!).ToString("dd/MM/yyyy")
                },
                Detalhes = g
                .Where(d => d.total_proventos != null)
                .Select(d => new
                {
                    d.tipo_valor,
                    d.nome_valor,
                    d.valor
                })
            }); 

            return  await resultadoAgrupado.ToListAsync();
        }
    }
}
