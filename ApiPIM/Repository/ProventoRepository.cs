using ApiPIM.Context;
using ApiPIM.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiPIM.Repository
{
    public class ProventoRepository : IProventoRepository
    {
        private readonly AppDbContext _db;
        public ProventoRepository(AppDbContext db)
        {
            _db = db;
        }
        public async Task AdicionaProvento(List<ProventosModel> p)
        {
            try
            {
                p.ForEach( async provento => {
                    await _db.Proventos.AddAsync(provento);
                });

                await _db.SaveChangesAsync();
                return;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task AdicionaTotal(List<ProventosModel> proventos)
        {
            decimal totalDescontos = 0;
            decimal totalProventos = 0;
            decimal totalLiquido = 0;

            proventos.ForEach(p =>{
                if (p.tipo_valor == "Provento")
                {
                    totalProventos += p.valor;
                }
                else if (p.tipo_valor == "Desconto")
                {
                    totalDescontos += p.valor;
                }
            });

            totalLiquido = totalProventos - totalDescontos;

            var dataPgto = $"{proventos[0].ano}-{proventos[0].mes}-{05} 00:00:00.000";

            ValoresPagamento total = new()
                {id_funcionario = proventos[0].id_funcionario,
                total_proventos = totalProventos,
                total_descontos = totalDescontos, 
                valor_liquido = totalLiquido,
                mes = proventos[0].mes,
                ano = proventos[0].ano,
                data_pagamento = DateTime.Parse(dataPgto)
            };

            await _db.ValoresPagamentos.AddAsync(total);
            await _db.SaveChangesAsync();
        }

        public async Task<bool> VerificaCaluloExistente(List<ProventosModel> proventos)
        {
            var info = await _db.ValoresPagamentos.SingleOrDefaultAsync(c => (c.ano == proventos[0].ano) && (c.mes == proventos[0].mes) && (c.id_funcionario == proventos[0].id_funcionario));

            if(info != null)
            {
                return true;
            }

            return false;
        }
    }
}
