using ApiPIM.Models;

namespace ApiPIM.Repository
{
    public interface IProventoRepository
    {
        Task AdicionaProvento(List<ProventosModel> provento);
        Task AdicionaTotal(List<ProventosModel> proventos);
        Task<bool> VerificaCaluloExistente(List<ProventosModel> proventos);
    }
}
