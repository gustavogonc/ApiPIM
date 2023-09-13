using ApiPIM.Models;

namespace ApiPIM.Repository
{
    public interface IFuncionarioRepository
    {
        List<Funcionarios> Get();
        Funcionarios GetById(int id);
        Funcionarios GetByName(string name);
        Funcionarios Add(Funcionarios funcionario);
        Funcionarios Update(Funcionarios funcionario);
        bool Delete(int id);
    }
}
