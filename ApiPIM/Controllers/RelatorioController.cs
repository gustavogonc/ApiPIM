
using ApiPIM.Models;
using ApiPIM.Repository;
using ApiPIM.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ApiPIM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RelatorioController : ControllerBase
    {
        private readonly ObterPagamentoRepository _repository;
        private readonly FuncionarioDeducoesRepository _deucoesRepository;

        public RelatorioController(ObterPagamentoRepository repository, FuncionarioDeducoesRepository deucoesRepository)
        {
            _repository = repository;
            _deucoesRepository = deucoesRepository;
        }

        [HttpGet]
        [Route("listaRelatorio/{mes:int}")]
        public async Task<ActionResult> RetornaUsuarios(int mes)
        {
            var pagamentosFiltrados = _repository.ObterPagamentosFiltrados(mes);
            return Ok(pagamentosFiltrados);
        }

        [HttpGet]
        [Route("listaFuncionario")]
        public IActionResult Funcionario()
        {

            var funcionarioBasicoList = _deucoesRepository.ObterFuncionariosBasicos();
            return Ok(funcionarioBasicoList);
        }

        [HttpGet]
        [Route("funcionarioDeducoes/{funcionario}/{mesSelecionado}")]
        public IActionResult FuncionarioDeducoes(string Funcionario, int mesSelecionado)
        {

            var funcionarioBasicoList = _deucoesRepository.ObterFuncionariosBasicos();

            var funcionarioDeducoesList = _deucoesRepository.ObterFuncionarioDeducoes();
            var funcionarioDeducoesFiltrados = funcionarioDeducoesList.Where(f => f.CPF == Funcionario && f.mes == mesSelecionado).First();

            calculaFGTS(funcionarioDeducoesFiltrados);
            funcionarioDeducoesFiltrados.FGTS = Math.Round(funcionarioDeducoesFiltrados.FGTS, 2);

            CalculaINSS(funcionarioDeducoesFiltrados);
            funcionarioDeducoesFiltrados.INSS = Math.Round(funcionarioDeducoesFiltrados.INSS, 2);

            calculaIR(funcionarioDeducoesFiltrados);
            funcionarioDeducoesFiltrados.IR = Math.Round(funcionarioDeducoesFiltrados.IR, 2);

           

            calculoDesconto(funcionarioDeducoesFiltrados);
            funcionarioDeducoesFiltrados.Descontos = Math.Round(funcionarioDeducoesFiltrados.Descontos, 2);

            calculoLiquido(funcionarioDeducoesFiltrados);
            funcionarioDeducoesFiltrados.ValorLiquido = Math.Round(funcionarioDeducoesFiltrados.ValorLiquido, 2);

            funcionarioDeducoesFiltrados.Salario = Math.Round(funcionarioDeducoesFiltrados.Salario, 2);
            funcionarioDeducoesFiltrados.VR = Math.Round(funcionarioDeducoesFiltrados.VR, 2);
            funcionarioDeducoesFiltrados.VT = Math.Round(funcionarioDeducoesFiltrados.VT, 2);

            var listaDeducao = new List<FuncionarioDeducoes>();
            listaDeducao.Add(funcionarioDeducoesFiltrados);

            var viewModel = new FuncionarioViewModel
            {
                FuncionarioDeducoesList = listaDeducao,
                FuncionarioBasicoList = funcionarioBasicoList
            };

            return Ok(viewModel);
        }

        private static void calculoLiquido(FuncionarioDeducoes funcionarioDeducoesFiltrados)
        {
            funcionarioDeducoesFiltrados.ValorLiquido = funcionarioDeducoesFiltrados.Salario - funcionarioDeducoesFiltrados.Descontos;
        }

        private static void calculoDesconto(FuncionarioDeducoes funcionarioDeducoesFiltrados)
        {
            funcionarioDeducoesFiltrados.Descontos = funcionarioDeducoesFiltrados.INSS + funcionarioDeducoesFiltrados.IR;
        }

        private static void calculaFGTS(FuncionarioDeducoes funcionarioDeducoesFiltrados)
        {
            funcionarioDeducoesFiltrados.FGTS = funcionarioDeducoesFiltrados.Salario * 0.08M;
        }

        private static void calculaIR(FuncionarioDeducoes funcionarioDeducoesFiltrados)
        {
            if (funcionarioDeducoesFiltrados.Salario > 2112.01M && funcionarioDeducoesFiltrados.Salario < 2826.66M)
            {
                funcionarioDeducoesFiltrados.IR = (funcionarioDeducoesFiltrados.Salario - funcionarioDeducoesFiltrados.INSS) * 0.075M - 158.40M;

            }
            else if (funcionarioDeducoesFiltrados.Salario > 2826.65M && funcionarioDeducoesFiltrados.Salario < 3751.06M)
            {
                funcionarioDeducoesFiltrados.IR = (funcionarioDeducoesFiltrados.Salario - funcionarioDeducoesFiltrados.INSS) * 0.15M - 370.40M;
            }
            else if (funcionarioDeducoesFiltrados.Salario > 3751.05M && funcionarioDeducoesFiltrados.Salario < 4664.69M)
            {
                funcionarioDeducoesFiltrados.IR = (funcionarioDeducoesFiltrados.Salario - funcionarioDeducoesFiltrados.INSS) * 0.225M - 651.73M;
            }
            else if (funcionarioDeducoesFiltrados.Salario > 4664.68M)
            {
                funcionarioDeducoesFiltrados.IR = (funcionarioDeducoesFiltrados.Salario - funcionarioDeducoesFiltrados.INSS) * 0.275M - 884.96M;
            }
            else
            {
                funcionarioDeducoesFiltrados.IR = 0;
            }
        }

        private static void CalculaINSS(FuncionarioDeducoes funcionarioDeducoesFiltrados)
        {
            if (funcionarioDeducoesFiltrados.Salario > 1100.01M && funcionarioDeducoesFiltrados.Salario < 2203.49M)
            {
                funcionarioDeducoesFiltrados.INSS = funcionarioDeducoesFiltrados.Salario * 0.09M;

            }
            else if (funcionarioDeducoesFiltrados.Salario > 2203.49M && funcionarioDeducoesFiltrados.Salario < 3305.23M)
            {
                funcionarioDeducoesFiltrados.INSS = funcionarioDeducoesFiltrados.Salario * 0.12M;
            }
            else if (funcionarioDeducoesFiltrados.Salario > 3305.22M)
            {
                funcionarioDeducoesFiltrados.INSS = funcionarioDeducoesFiltrados.Salario * 0.14M;
            }
            else
            {
                funcionarioDeducoesFiltrados.INSS = funcionarioDeducoesFiltrados.Salario * 0.075M;
            }
        }

        [HttpGet]
        [Route("dashboard")]
        public IActionResult Dashboard()
        {
            var data = _repository.ObterPagamentoMensal();
            var departamento = _repository.ObterPagamentosDepartamento();
            var viewModel = new GraficosViewModel
            {
                pagamentoMensal = departamento,
                pagamentoAnoMes = data
            };
            return Ok(viewModel);
        }

    }
}
