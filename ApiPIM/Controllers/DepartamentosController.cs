using ApiPIM.Models;
using ApiPIM.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiPIM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartamentosController : ControllerBase
    {
        private IDepartamentosRepository _departamentosRepository;
        public DepartamentosController(IDepartamentosRepository departamentosRepository)
        {
            _departamentosRepository = departamentosRepository;
        }

        [HttpGet("listarDepartamentos")]
        public async Task<ActionResult> RetornaDepartamentos()
        {
            try
            {
                List<Departamentos> dep = await _departamentosRepository.Get();
                if (dep.Count == 0)
                {
                    return NotFound("Não foram encontrados departamentos");
                }

                return Ok(dep);
            }
            catch (Exception ex)
            {
                return Problem("Ocorreu um erro ao retornar departamentos.", null, 500, "Erro retornar departamentos.", null);
            }
        }

        [HttpGet("departamentoId/{id:int}")]
        public async Task<ActionResult> RetornaDepartamento(int id)
        {
            try
            {
                Departamentos dep = await _departamentosRepository.Get(id);

                if (dep == null)
                {
                    return NotFound();
                }

                return Ok(dep);
            }
            catch (Exception ex)
            {
                return Problem("Ocorreu um erro ao retornar departamento por id.", null, 500, "Erro retornar departamento.", null);
            }
        }

        [HttpPut("atualizaDepartamento")]
        public async Task<ActionResult> AtualizaDepartamento(Departamentos dep)
        {
            try
            {
                Departamentos depart = await _departamentosRepository.Atualizar(dep);

                if(depart == null)
                {
                    return BadRequest();
                }

                return Ok();

            }
            catch (Exception ex)
            {
                return Problem("Ocorreu um erro ao editar departamento.", null, 500, "Erro editar departamento.", null);
            }
        }

        [HttpPost("novoDepartamento")]
        public async Task<ActionResult> NovoDepartamento(Departamentos dep)
        {
            try
            {
                bool cadastro = await _departamentosRepository.Novo(dep);
                if (!cadastro)
                {
                    return BadRequest("Cadastro já existente.");
                }
                return Created("", dep);
            }
            catch (Exception ex)
            {
                return Problem("Ocorreu um erro ao inserir departamento.", null, 500, "Erro novo departamento.", null);
            }
        }

        [HttpDelete("excluir/{id:int}")]
        public async Task<ActionResult> DeletarDepartamento(int id)
        {
            try
            {
                Departamentos dep = await _departamentosRepository.Get(id);

                if (dep == null)
                {
                    return NotFound("Departamento não encontrado.");
                }

                await _departamentosRepository.Deletar(id);

                return Ok();
            }
            catch (Exception ex)
            {
                return Problem("Ocorreu um erro ao deletar departamento.", null, 500, "Erro deletar departamento.", null);
            }
        }
    }
}
