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
        IEnumerable<object> FuncionariosCompleto();
        Task<bool> NovoFuncionario(FuncionarioDTO fun);
        Task<bool> AtualizaFuncionario(int id, FuncionarioDTO fun);
        IEnumerable<object> FuncionarioCompleto(int id);
        IEnumerable<object> FuncionarioSalario(int id);
    }
}
