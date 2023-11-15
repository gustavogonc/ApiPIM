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

        [HttpGet]
        [Route("retornaCargo/{id:int}")]
        public async Task<ActionResult> RetornarCargo(int id)
        {
            try
            {
                var cargo = await _cargosRepository.Get(id);

                if(cargo == null)
                {
                    return NotFound();
                }

                return Ok(cargo);
            }
            catch (Exception ex)
            {
                return Problem("Ocorreu um erro ao retornar cargos.", null, 500, "Erro retornar cargos.", null);
            }
        }

        [HttpPost]
        [Route("atualizaCargo")]
        public async Task<ActionResult> AtualizaCargo([FromBody] Cargos cargo)
        {
            try
            {
                bool result = await _cargosRepository.AtualizaCargo(cargo);
                if (!result)
                {
                    return BadRequest("Não foi possível atulizar o cargo!");
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return Problem("Ocorreu um erro ao atualizar cargo.", null, 500, "Erro atualizar cargos.", null);
            }
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

        [HttpPost]
        [Route("excluir/{id:int}")]
        public async Task<ActionResult> DeleteCargo(int id)
        {
            try
            {
                var result = await _cargosRepository.Get(id);
                if (result == null)
                {
                    return NotFound("Id de cargo não encontrado");
                }

                await _cargosRepository.DeleteCargo(id);
                return Ok();

            }
            catch (Exception ex)
            {
                return Problem("Ocorreu um erro ao deletar cargo.", null, 500, "Erro deletar cargos.", null);
            }
        }
    }
}
