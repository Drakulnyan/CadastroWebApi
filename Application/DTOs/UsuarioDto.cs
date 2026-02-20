namespace CadastroWebApi.Application.DTOs
{
    public class UsuarioDto
    {
        public required string Nome { get; set; }
        public required string Email { get; set; }
        public int Idade { get; set; }
        public required string Telefone { get; set; }
        public required string Endereco { get; set; }
        public DateTime DataNascimento { get; set; }
        public required string Senha { get; set; }
    }
}
