using ApiPIM.Models;

namespace ApiPIM.Repository
{
    public interface IUsuarioRepository
    {
        List<Usuarios> Get();
        Usuarios Get(int id);
        Usuarios Login(Autenticacao auth);
        bool Registrar(Usuarios usuario);
        void Inativar(int id);
        Task<bool> Excluir(int id);
        Task<Usuarios> Editar(int id, Usuarios user);
    }
}
