using ApiPIM.Models;
using ApiPIM.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiPIM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;
        public UsuariosController(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        [HttpGet]
        [Route("listarUsuarios")]
        public IActionResult ListaUsuarios()
        {
            try
            {
                var listaUsuarios = _usuarioRepository.Get();

                if(listaUsuarios.Count == 0)
                {
                    return NotFound("Não foram encontrados usuários!");
                }

                return Ok(listaUsuarios);
            }
            catch (Exception ex)
            {

                return Problem($"Erro ao retornar os usuários {ex.Message}", null, 500, "Erro retornar usuários");
            }
        }

        [HttpPut]
        [Route("editar/{id:int}")]
        public async Task<IActionResult> EditarUsuario([FromRoute] int id, [FromBody] Usuarios user)
        {
            try
            {
                Usuarios editado = await _usuarioRepository.Editar(id, user);

                if(editado == null)
                {
                    return NotFound("Usuário não encontrado");
                }

                return Ok("Usuário editado com sucesso!");
            }
            catch (Exception ex)
            {

                return Problem($"Erro ao editar o usuário {ex.Message}", null, 500, "Erro editar usuário");
            }
        }

        [HttpDelete]
        [Route("excluir/{id:int}")]
        public async Task<IActionResult> DeletaUsuario(int id)
        {
            try
            {
                bool usuarioExcluido = await _usuarioRepository.Excluir(id);

                if (!usuarioExcluido)
                {
                    return NotFound("Usuário não encontrado");
                }

                return Ok("Excluído com sucesso!");
            }
            catch (Exception ex)
            {

                return Problem($"Erro ao excluir usuário {ex.Message}", null, 500, "Erro deletar usuário");
            }
        }
    }
}
