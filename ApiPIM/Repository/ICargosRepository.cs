using ApiPIM.Models;

namespace ApiPIM.Repository
{
    public interface ICargosRepository
    {
        Task<bool> NovoCargo(Cargos cargo);
        Task<List<Cargos>> Get();
    }
}
