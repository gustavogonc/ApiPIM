using ApiPIM.Models;

namespace ApiPIM.Repository
{
    public interface IUsuarioRepository
    {
        List<Usuarios> Get();
        Usuarios Get(int id);
        Usuarios Login(Autenticacao auth);
        int Registrar(Usuarios usuario);
        void Inativar(int id);
    }
}
