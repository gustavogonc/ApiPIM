using ApiPIM.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiPIM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppController : ControllerBase
    {
        private readonly IAppRepository _app;
        public AppController(IAppRepository app)
        {
            _app = app;
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetMesesPagamento(int id)
        {
            try
            {
                var infos = await _app.RetornaMesesFuncionario(id);
                return Ok(infos);
            }
            catch (Exception ex)
            {

                return Problem("Erro ao buscar os meses do colaborador", "", 500, "Erro meses do colaborador");
            }
        }
    }
}
