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
                    cargo = funcionarios.cargo,
                    departamento = funcionarios.departamento,
                    data_cont = funcionarios.data_cont,
                    inf_cont = funcionarios.inf_cont,
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

    }
}
