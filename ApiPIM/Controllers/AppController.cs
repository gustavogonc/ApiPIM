using ApiPIM.Models;
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

                if(infos.Count() == 0)
                {
                    return NotFound();
                }
                return Ok(infos);
            }
            catch (Exception ex)
            {

                return Problem("Erro ao buscar os meses do colaborador", null, 500, "Erro meses do colaborador");
            }
        }

        [HttpPost]
        [Route("valoresMes")]
        public async Task<IActionResult> GetValoresPorMes([FromBody] DetalhesPagamentoFuncionario detalhes)
        {
            try
            {
                var listaProvento = await _app.RetornaDetalhesMeses(detalhes);

                if(listaProvento.Count() == 0)
                {
                    return NotFound();
                }
                return Ok(listaProvento);
                
            }
            catch (Exception ex)
            {

                return Problem("Erro ao buscar os proventos do mes do colaborador", null, 500, "Erro provento mes do colaborador");
            }
        }

        [HttpPost]
        [Route("loginApp")]
        public async Task<IActionResult> LoginApp([FromBody] Autenticacao auth)
        {
            try
            {
                var info = await _app.Login(auth);

                if(info == null)
                {
                    return NotFound("Usuário ou senha inválidos");
                }

                return Ok(info);
            }
            catch (Exception ex)
            {

                return Problem($"Erro ao logar colaborador  {ex.Message}", null, 500, "Erro logar colaborador");
            }
        }

        [HttpPut]
        [Route("atualizaSenha")]
        public async Task<IActionResult> AlterarSenha(AlteracaoSenha novaSenha)
        {
            try
            {
                bool atualizou = await _app.AlterarSenha(novaSenha);

                if (!atualizou)
                {
                    return BadRequest("Senha atual incorreta");
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return Problem($"Erro ao alterar senha colaborador {ex.Message}", null, 500, "Erro alterar senha");
            }


        }
    }
}
