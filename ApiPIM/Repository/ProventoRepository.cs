using ApiPIM.Context;
using ApiPIM.Models;

namespace ApiPIM.Repository
{
    public class ProventoRepository : IProventoRepository
    {
        private readonly AppDbContext _db;
        public ProventoRepository(AppDbContext db)
        {
            _db = db;
        }
        public async Task AdicionaProvento(ProventosModel p)
        {
            await _db.Proventos.AddAsync(p);
            await _db.SaveChangesAsync();
            return;
        }
    }
}
