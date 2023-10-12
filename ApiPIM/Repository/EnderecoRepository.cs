using ApiPIM.Context;
using ApiPIM.Models;

namespace ApiPIM.Repository
{
    public class EnderecoRepository : IEnderecoRepository
    {
        private readonly AppDbContext _context;

        public EnderecoRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Cadastro(Endereco end)
        {
            var resultado = await _context.Enderecos.AddAsync(end);
            await _context.SaveChangesAsync();

            return resultado != null ? true : false;
        }
    }
}
