using ApiPIM.Models;

namespace ApiPIM.Repository
{
    public interface IAppRepository
    {
        Task<IQueryable> RetornaMesesFuncionario(int id);
    }
}
