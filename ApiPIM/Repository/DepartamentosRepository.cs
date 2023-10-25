using ApiPIM.Context;
using ApiPIM.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiPIM.Repository
{
    public class DepartamentosRepository : IDepartamentosRepository
    {
        private readonly AppDbContext _db;
        public DepartamentosRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<List<Departamentos>> Get()
        {
            return await _db.Departamentos.ToListAsync();
        }
        public async Task<List<Departamentos>> GetComCargos()
        {
            return await _db.Departamentos.Include(a => a.cargos).ToListAsync();
        }
        public async Task<Departamentos> Get(int id)
        {
            return await _db.Departamentos.SingleOrDefaultAsync(a => a.id_departamento == id);
        }

        public async Task<bool> Novo(Departamentos dep)
        {
            var search = await _db.Departamentos.SingleOrDefaultAsync(a=> a.nome_departamento == dep.nome_departamento);
            if(search != null)
            {
                return false;
            }

            var novoDep = new Departamentos
            {
                nome_departamento = dep.nome_departamento,
                descricao_departamento = dep.descricao_departamento,
                data_criacao = DateTime.Now
            };

            _db.Departamentos.Add(novoDep);
            await _db.SaveChangesAsync();

            return true;

        }
        public async Task<Departamentos> Atualizar(Departamentos departamento)
        {
            Departamentos dep = new Departamentos();

            var resultado = await _db.Departamentos.SingleOrDefaultAsync(a => a.id_departamento == departamento.id_departamento);
            if(resultado == null)
            {
                return dep;
            }

            dep = resultado;

            resultado.nome_departamento = departamento.nome_departamento;
            resultado.descricao_departamento = departamento.descricao_departamento;

            _db.Departamentos.Update(dep);
            await _db.SaveChangesAsync();
            return dep;
        }
        public async Task<Departamentos> Deletar(int id)
        {
            var departamento = await _db.Departamentos.SingleOrDefaultAsync(a => a.id_departamento == id);

            _db.Departamentos.Remove(departamento!);
            await _db.SaveChangesAsync();
            return departamento!;
        }
    }
}
