using ApiPIM.Context;
using ApiPIM.Models;
using ApiPIM.Services;

namespace ApiPIM.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly AppDbContext _db;
        private readonly SenhaServices _senhaServices;

        public UsuarioRepository(AppDbContext db, SenhaServices senhaServices)
        {
            _db = db;
            _senhaServices = senhaServices;
        }
        public List<Usuarios> Get()
        {
            return _db.Usuarios.ToList();
        }

        public Usuarios Get(int id)
        {
            return _db.Usuarios.FirstOrDefault(a => a.usuarioId == id)!;
        }

        public void Inativar(int id)
        {
            var usuario = _db.Usuarios.FirstOrDefault(a => a.usuarioId == id);
            usuario!.ativo = 0;

            _db.Usuarios.Update(usuario);
            _db.SaveChanges();
        }

        public Usuarios Login(Autenticacao auth)
        {
            string senha = _senhaServices.ComputeHash(auth.senha);
            var usuario = _db.Usuarios.FirstOrDefault(u => (u.email == auth.email) && (u.senha == senha));

            if (usuario == null)
            {
                return null;
            }
            return usuario;
        }

        public int Registrar(Usuarios usuario)
        {
            var verificaRegistro = _db.Usuarios.Contains(usuario);

            if (verificaRegistro)
            {
                return 0;
            }
            var novoUsuario = new Usuarios
            {
                usuarioId = usuario.usuarioId,
                nome = usuario.nome,
                email = usuario.email,
                senha = _senhaServices.ComputeHash(usuario.senha),
                administrador = usuario.administrador,
                ativo = 1
            };

            _db.Usuarios.Add(novoUsuario);
            _db.SaveChanges();
            return novoUsuario.usuarioId;
        }
        
    }
}
