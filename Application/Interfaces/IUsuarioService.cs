using CadastroWebApi.Application.DTOs;
using CadastroWebApi.Domain.Entities;

namespace CadastroWebApi.Application.Interfaces
{
    public interface IUsuarioService
    {
        Task<Usuario> AdicionarUsuarioAsync(UsuarioDto usuarioDto);
        Task<List<Usuario>> ListarUsuariosAsync();
        Task<Usuario?> AtualizarUsuarioAsync(int id, UsuarioDto usuarioDto);
        Task<bool> DeletarUsuarioAsync(int id);
    }
}
