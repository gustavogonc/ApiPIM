using ApiPIM.Models;
using ApiPIM.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiPIM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalculoController : ControllerBase
    {
        private readonly IProventoRepository _proventoRepository;
        public CalculoController(IProventoRepository proventoRepository)
        {
            _proventoRepository = proventoRepository;
        }

        [HttpPost]
        [Route("AdicionaValores")]
        public async Task<IActionResult> AdicionarValores([FromBody] List<ProventosModel> proventos)
        {
            try
            {
               await _proventoRepository.AdicionaProvento(proventos);

                return Ok();
            }
            catch (Exception ex)
            {
                return Problem("Ocorreu um erro adicionar valores.", null, 500, "Erro adicionar valores.", null);
            }
        }
    }
}
