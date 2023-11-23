namespace ApiPIM.Models.FuncionarioDTO
{
    public class FuncionarioEdicaoDTO
    {
        public FuncionarioEdicaoDTO()
        {
            enderecos = new List<EnderecoDTO>();
            telefones = new List<TelefoneDTO>();
        }
        public string? nome { get; set; }
        public string? cpf { get; set; }
        public string? sexo { get; set; }
        public int? cargo_id { get; set; }
        public DateTime data_contratacao { get; set; }
        public string? estado_civil { get; set; }
        public string? email_usuario { get; set; }
        public List<EnderecoDTO>? enderecos { get; set; }
        public List<TelefoneDTO>?  telefones { get; set; }

    }
}
