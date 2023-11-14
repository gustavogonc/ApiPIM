using ApiPIM.Models;
using ApiPIM.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiPIM.Controllers
{
    [Route("api/[controller]")]
    //[Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    public class AutenticacaoController : ControllerBase
    {
        private readonly IUsuarioRepository _repository;

        public AutenticacaoController(IUsuarioRepository repository)
        {
            _repository = repository;
        }

        [AllowAnonymous]
        [HttpGet("listarUsuarios")]
        public IActionResult RetornaUsuarios()
        {
            List<Usuarios> listarUsuarios = _repository.Get();
            return Ok(listarUsuarios);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Autenticar(Autenticacao auth)
        {
            try
            {
                var usuario = _repository.Login(auth);
                if(usuario != null)
                {
                    return Ok(new
                    {
                        usuario = new
                        {
                            usuario.usuario_id,
                            usuario.email,
                            usuario.nome,
                            usuario.administrador,
                            usuario.ativo,
                            usuario.token,
                            usuario.expiration_token
                        }
                    });
                }
                else
                {
                    return BadRequest("Usuário não encontrado!");
                }
            }
            catch (Exception)
            {

                return Problem("Ocorreu um erro ao validar o acesso", null, 500, "Erro Login", null);
            }
        }

        [AllowAnonymous]
        [HttpPost("registrar")]
        public IActionResult CadastrarUsuario(Usuarios usuario)
        {
            try
            {
                var criarUsuario = _repository.Registrar(usuario);
                if (!criarUsuario)
                {
                    return BadRequest("Usuário já cadastrado");
                }
                return Created("", usuario);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
