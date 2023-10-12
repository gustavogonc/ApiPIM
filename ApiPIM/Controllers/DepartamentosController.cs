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
                throw;
            }
        }

        [HttpGet("listarDepartamentos")]
        public async Task<ActionResult> RetornaDepartamentos()
        {
            try
            {
                List<Departamentos> dep = await _departamentosRepository.Get();
                if(dep.Count == 0)
                {
                    return NotFound("Não foram encontrados departamentos");
                }

                return Ok(dep);
            }
            catch (Exception ex)
            {
                return Problem("Ocorreu um erro ao retornar departamentos.", null, 500, "Erro retornar departamentos.", null);
                throw;
            }
        }
    }
}
