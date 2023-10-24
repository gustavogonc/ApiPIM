using ApiPIM.Models;

namespace ApiPIM.Repository
{
    public interface IDepartamentosRepository
    {
        Task<List<Departamentos>> Get();
        Task<List<Departamentos>> GetComCargos();
        Task<Departamentos> Get(int id);
        Task<bool> Novo(Departamentos departamento);
        Task<Departamentos> Atualizar(int id, Departamentos departamento);
        Task<Departamentos> Deletar(int id);


    }
}
