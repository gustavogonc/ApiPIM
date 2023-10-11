using ApiPIM.Models;
using ApiPIM.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiPIM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CargosController : ControllerBase
    {
        private ICargosRepository _cargosRepository;
        public CargosController(ICargosRepository cargosRepository)
        {
            _cargosRepository = cargosRepository;
        }

        [HttpPost]
        [Route("novoCargo")]
        public async Task<ActionResult> PostNovoCargo(Cargos cargo)
        {
            try
            {
                bool result = await _cargosRepository.NovoCargo(cargo);
                if (!result)
                {
                    return BadRequest("Já existe um cargo com esse nome.");
                }
                return Ok(cargo);

            }
            catch (Exception ex)
            {
                return Problem("Ocorreu um erro ao cadastrar cargo.", null, 500, "Erro cadastrar cargos.", null);
            }
        }

        [HttpGet]
        [Route("retornaCargos")]
        public async Task<ActionResult> RetornarCargos()
        {
            try
            {
                List<Cargos> lista = await _cargosRepository.Get();

                return Ok(lista);
            }
            catch (Exception ex)
            {
                return Problem("Ocorreu um erro ao retornar cargos.", null, 500, "Erro retornar cargos.", null);
            }
        }
    }
}
