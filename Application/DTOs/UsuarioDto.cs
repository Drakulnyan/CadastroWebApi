namespace CadastroWebApi.Application.DTOs
{
    public class UsuarioDto
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public int Idade { get; set; }
        public string Telefone { get; set; }
        public string Endereco { get; set; }
        public DateTime DataNascimento { get; set; }
    }
}
