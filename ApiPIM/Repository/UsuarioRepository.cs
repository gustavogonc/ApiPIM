using ApiPIM.Context;
using ApiPIM.Models;
using ApiPIM.Services;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace ApiPIM.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly AppDbContext _db;
        private readonly SenhaServices _senhaServices;
        private readonly TokenService _tokenService;

        public UsuarioRepository(AppDbContext db, SenhaServices senhaServices, TokenService tokenService)
        {
            _db = db;
            _senhaServices = senhaServices;
            _tokenService = tokenService;
        }
        public List<Usuarios> Get()
        {
            return _db.Usuarios.ToList();
        }

        public Usuarios Get(int id)
        {
            return _db.Usuarios.FirstOrDefault(a => a.usuario_id == id)!;
        }

        public void Inativar(int id)
        {
            var usuario = _db.Usuarios.FirstOrDefault(a => a.usuario_id == id);
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

            Bearer bearer = _tokenService.GeraToken(auth);
            AtualizarTokenUsuario(usuario, bearer);
            return usuario;
        }

        public bool Registrar(Usuarios usuario)
        {
            var verificaRegistro = _db.Usuarios.SingleOrDefault(u => u.email == usuario.email);

            if (verificaRegistro != null)
            {
                return false;
            }
            var novoUsuario = new Usuarios
            {
                nome = usuario.nome,
                email = usuario.email,
                senha = _senhaServices.ComputeHash(usuario.senha),
                administrador = usuario.administrador,
                ativo = 1
            };

            _db.Usuarios.Add(novoUsuario);
            _db.SaveChanges();
            return true;
        }

        public void AtualizarTokenUsuario(Usuarios usuario, Bearer bearer)
        {
            var user = _db.Usuarios.SingleOrDefault(u => u.usuario_id == usuario.usuario_id);
            if(user != null)
            {
                user.token = bearer.AccessKey;
                user.expiration_token = bearer.Validade;
                _db.SaveChanges();
            }
        }

        public async Task<bool> Excluir(int id)
        {
            var user = await _db.Usuarios.SingleOrDefaultAsync(u => u.usuario_id == id);

            if(user == null)
            {
                return false;
            }

            _db.Usuarios.Remove(user);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<Usuarios> Editar(int id, Usuarios user)
        {
            if(id != user.usuario_id)
            {
                return null;
            }

            var usuario = await _db.Usuarios.SingleOrDefaultAsync(u => u.usuario_id == id);

            if(usuario == null)
            {
                return null;
            }

            usuario.email = user.email;
            usuario.nome = user.nome;
            usuario.administrador = user.administrador;
            usuario.ativo = user.ativo;

            if(user.senha != "")
            {
                var senha = _senhaServices.ComputeHash(user.senha);
                usuario.senha = senha;
            }

            _db.Usuarios.Update(usuario);
            await _db.SaveChangesAsync();

            return usuario;
        }
    }
}
