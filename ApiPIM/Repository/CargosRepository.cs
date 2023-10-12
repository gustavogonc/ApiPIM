using ApiPIM.Context;
using ApiPIM.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiPIM.Repository
{
    public class CargosRepository : ICargosRepository
    {
        private readonly AppDbContext _db;
        public CargosRepository(AppDbContext context) {
            _db = context;
        }

        public async Task<List<Cargos>> Get()
        {
            return await _db.Cargos.ToListAsync();
        }

        public async Task<List<Cargos>> GetComDepartamentos()
        {
            return await _db.Cargos.Include(a => a.Departamento).ToListAsync();
        }

        public async Task<bool> NovoCargo(Cargos cargo)
        {
            var result = await _db.Cargos.SingleOrDefaultAsync(a => a.nome_cargo == cargo.nome_cargo);
            if (result != null)
            {
                return false;
            }

            var novoCargo = new Cargos
            {
                nome_cargo = cargo.nome_cargo,
                descricao_cargo = cargo.descricao_cargo,
                salario = cargo.salario,
                DepartamentoId = cargo.DepartamentoId
            };


            await _db.Cargos.AddAsync(novoCargo);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}