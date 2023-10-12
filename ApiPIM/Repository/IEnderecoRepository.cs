using ApiPIM.Models;

namespace ApiPIM.Repository
{
    public interface IEnderecoRepository
    {
        Task<bool> Cadastro(Endereco end);
    }
}
