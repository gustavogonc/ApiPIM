using ApiPIM.Context;
using ApiPIM.Models;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace ApiPIM.Repository
{
    public class FuncionarioRepository : IFuncionarioRepository
    {
        private readonly AppDbContext _db;

        public FuncionarioRepository(AppDbContext db)
        {
            _db = db;
        }

        public List<Funcionarios> Get()
        {
            return _db.Funcionarios.Include(a => a.endereco).ToList();
        }
        public Funcionarios GetById(int id)
        {
            Funcionarios funcionario = _db.Funcionarios.Include(a => a.endereco).SingleOrDefault(a => a.id_funcionario == id)!;

            return funcionario!;
        }

        public Funcionarios GetByName(string name)
        {
            Funcionarios funcionario = _db.Funcionarios.Include(a => a.endereco).SingleOrDefault(a => a.nome_funcionario == name)!;

            return funcionario!;
        }
        public Funcionarios Add(Funcionarios funcionario)
        {
            _db.Add(funcionario);
            _db.SaveChanges();

            return funcionario;
        }

        public Funcionarios Update(Funcionarios funcionarios)
        {
            Funcionarios f = _db.Funcionarios.SingleOrDefault(a => a.id_funcionario == funcionarios.id_funcionario);

            Funcionarios funcionarioAlterado = new Funcionarios();
            if(f != null)
            {
                var novoFuncionario = new Funcionarios{
                    nome_funcionario = funcionarios.nome_funcionario,
                    cpf = funcionarios.cpf,
                    cargo_id = funcionarios.cargo_id,
                    data_contratacao = funcionarios.data_contratacao,
                    sexo = funcionarios.sexo,
                    estado_civil = funcionarios.estado_civil
                };

                _db.Update(novoFuncionario);
                _db.SaveChanges();
                return novoFuncionario;
            }
            else
            {
                return funcionarioAlterado;
            }
        }

        public bool Delete(int id)
        {
            Funcionarios f = _db.Funcionarios.SingleOrDefault(a => a.id_funcionario == id);

            if(f == null)
            {
                _db.Remove(f);
                _db.SaveChanges();

                return true;
            }   


            return false;
        }

        public IEnumerable<object> FuncionariosCompleto()
        {
            var lista = from f in _db.Funcionarios
                        join end in _db.Enderecos on f.id_funcionario equals end.funcionario_id into end_fun
                        from fun in end_fun.DefaultIfEmpty()
                        join tel in _db.ContatosFuncionario on f.id_funcionario equals tel.funcionario_id into tel_fun
                        from fun_tel in tel_fun.DefaultIfEmpty()
                        join car in _db.Cargos on f.cargo_id equals car.id_cargo into car_fun
                        from cargo in car_fun.DefaultIfEmpty()
                        join dep in _db.Departamentos on cargo.DepartamentoId equals dep.id_departamento into dep_carg
                        from departamento in dep_carg.DefaultIfEmpty()
                        select (new
                        {
                            f.id_funcionario,
                            f.nome_funcionario,
                            f.sexo,
                            f.estado_civil,
                            f.cargo_id,
                            departamento.nome_departamento,
                            cargo.nome_cargo,
                            f.data_contratacao,
                            fun.tipo_endereco,
                            fun.rua,
                            fun.bairro,
                            fun.cep,
                            fun.cidade,
                            fun_tel.tipo_telefone,
                            fun_tel.numero_contato
                        });

            var groupedResults = lista
                        .GroupBy(l => l.id_funcionario)
                        .Select(g => new {
                            Funcionario = new
                            {
                                id_funcionario = g.Key,
                                nome_funcionario = g.First().nome_funcionario,
                                sexo = g.First().sexo,
                                estado_civil = g.First().estado_civil,
                                cargo_id = g.First().cargo_id,
                                departamento = g.First().nome_departamento,
                                cargo = g.First().nome_cargo,
                                data_contratacao = g.First().data_contratacao,
                            },
                            Enderecos = g
                                .Where(e => e.rua != null) // Para filtrar possíveis nulls
                                .Select(e => new {
                                    e.tipo_endereco,
                                    e.rua,
                                    e.bairro,
                                    e.cep,
                                    e.cidade
                                })
                                .Distinct() // Para evitar endereços duplicados
                                .ToList(),
                            Contatos = g
                                .Where(c => c.tipo_telefone != null) // Para filtrar possíveis nulls
                                .Select(c => new {
                                    c.tipo_telefone,
                                    c.numero_contato
                                })
                                .Distinct() // Para evitar contatos duplicados
                                .ToList()
                        }).ToList();


            return groupedResults;
        }

        public async Task<bool> NovoFuncionario(FuncionarioDTO fun)
        {
            try
            {
                var funcionario = new Funcionarios
                {
                    nome_funcionario = fun.nome,
                    cpf = fun.cpf,
                    sexo = fun.sexo,
                    cargo_id = fun.cargo_id,
                    data_contratacao = fun.data_contratacao,
                    estado_civil = fun.estado_civil,
                };

                await _db.Funcionarios.AddAsync(funcionario);
                await _db.SaveChangesAsync();

                var endereco = new Endereco
                {
                    funcionario_id = funcionario.id_funcionario,
                    tipo_endereco = fun.tipo_endereco,
                    rua = fun.rua,
                    cep = fun.cep,
                    bairro = fun.bairro,
                    num_endereco = fun.num_endereco,
                    cidade = fun.cidade,
                    uf_estado = fun.uf_estado,
                    data_cadastro = DateTime.Now
                };

                await _db.Enderecos.AddAsync(endereco);
                await _db.SaveChangesAsync();

                var telefone = new ContatoFuncionario
                {
                    funcionario_id = funcionario.id_funcionario,
                    tipo_telefone = fun.tipo_telefone,
                    numero_contato = fun.numero_contato,
                    data_cadastro = DateTime.Now
                };

                await _db.ContatosFuncionario.AddAsync(telefone);
                await _db.SaveChangesAsync();


                return true;
            }
            catch (Exception ex)
            {

                return false;
                throw;
            }
        }
    }
}
