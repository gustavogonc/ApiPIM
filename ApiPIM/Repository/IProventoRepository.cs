using ApiPIM.Models;

namespace ApiPIM.Repository
{
    public interface IProventoRepository
    {
        Task AdicionaProvento(ProventosModel provento);
    }
}
