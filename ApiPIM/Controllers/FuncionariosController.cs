using ApiPIM.Models.FuncionarioDTO;
using ApiPIM.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiPIM.Controllers
{
    [Route("api/[controller]")]
    //[Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    public class FuncionariosController : ControllerBase
    {
        private readonly IFuncionarioRepository _funcionarioRepository;

        public FuncionariosController(IFuncionarioRepository funcionarioRepository)
        {
            _funcionarioRepository= funcionarioRepository;
        }

        [HttpGet]
        public ActionResult GetListFuncionarios()
        {
            var funcionarios = _funcionarioRepository.Get();

            if(funcionarios == null)
            {
                return NotFound();
            }
            return Ok(funcionarios);
        }

        [HttpGet]
        [Route("dadosFuncionarioCompleto")]
        public async Task<ActionResult> RetornaDadosCompletos()
        {
            try
            {
                var infos = _funcionarioRepository.FuncionariosCompleto();

                if (infos == null)
                {
                    return NotFound("Nenhum dado encontrado.");
                }
                return Ok(infos);

            }
            catch (Exception ex)
            {

                return Problem("Ocorreu um erro ao retornar dados completos dos funcionários.", null, 500, "Erro retornar funcionários.", null);
                throw;
            }
        }

        [HttpGet]
        [Route("dadosFuncionarioCompleto/{id:int}")]
        public async Task<ActionResult> RetornaDadosCompletos(int id)
        {
            try
            {
                var infos = _funcionarioRepository.FuncionarioCompleto(id);

                if (infos == null)
                {
                    return NotFound("Nenhum dado encontrado.");
                }
                return Ok(infos);

            }
            catch (Exception ex)
            {

                return Problem("Ocorreu um erro ao retornar dados completos do funcionário.", null, 500, "Erro retornar funcionário.", null);
                throw;
            }
        }

        [HttpGet]
        [Route("dadosFuncionarioCalculo/{id:int}")]
        public async Task<ActionResult> RetornaDadosCalculo(int id)
        {
            try
            {
                var infos = _funcionarioRepository.FuncionarioSalario(id);

                if (infos == null)
                {
                    return NotFound("Nenhum dado encontrado.");
                }
                return Ok(infos);

            }
            catch (Exception ex)
            {

                return Problem("Ocorreu um erro ao retornar dados completos do funcionário.", null, 500, "Erro retornar funcionário.", null);
                throw;
            }
        }

        [HttpPost]
        [Route("novoFuncionario")]
        public async Task<ActionResult> PostNovoFuncionario(FuncionarioDTO funcionarios)
        {
            try
            {
                bool f = await _funcionarioRepository.NovoFuncionario(funcionarios);

                if (f == false)
                {
                    return BadRequest("Houve um erro ao inserir o funcionario");
                }
                return Created("", f);
            }
            catch (Exception ex)
            {
                return Problem("Ocorreu um erro ao inserir funcionário.", null, 500, "Erro inserir funcionário.", null);
                throw;
            }
        }

        [HttpPut]
        [Route("atualizaFuncionario/{id:int}")]
        public async Task<ActionResult> PutAtualizaFuncionario(int id, FuncionarioEdicaoDTO func)
        {
            try
            {
                //bool f = await _funcionarioRepository.AtualizaFuncionario(id, func);

                if (!f)
                {
                    return BadRequest("Houve um erro ao atualizar o funcionário");
                }

                return Ok("Atualizado com sucesso!");
            }
            catch (Exception)
            {
                return Problem("Ocorreu um erro ao editar funcionário.", null, 500, "Erro editar funcionário.", null);
            }
        }

        [HttpDelete]
        [Route("excluir/{id:int}")]
        public async Task<ActionResult> DeleteFuncionario(int id)
        {
            try
            {
                bool f = await _funcionarioRepository.Delete(id);

                if (!f)
                {
                    return NotFound("Funcionário não encontrado");
                }

                return Ok();
            }
            catch (Exception)
            {
                return Problem("Ocorreu um erro ao deletar funcionário.", null, 500, "Erro deletar funcionário.", null);
            }
        }
    }
}
