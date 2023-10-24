using ApiPIM.Models;

namespace ApiPIM.Repository
{
    public interface ICargosRepository
    {
        Task<bool> NovoCargo(Cargos cargo);
        Task<List<Cargos>> Get();
        Task<IQueryable> Get(int id);
        Task<List<Cargos>> GetComDepartamentos();
    }
}
