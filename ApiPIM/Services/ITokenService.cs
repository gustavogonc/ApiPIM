using ApiPIM.Models;

namespace ApiPIM.Services
{
    public interface ITokenService
    {
        public Bearer GeraToken(Autenticacao auth);
    }
}
