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
    }
}
