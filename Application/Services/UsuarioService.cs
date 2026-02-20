using CadastroWebApi.Application.DTOs;
using CadastroWebApi.Infrastructure.Data;
using CadastroWebApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using CadastroWebApi.Application.Interfaces;

namespace CadastroWebApi.Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly AppDbContext _context;

        public UsuarioService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Usuario> AdicionarUsuarioAsync(UsuarioDto usuarioDto)
        {
            var usuario = new Usuario 
            {
                Nome = usuarioDto.Nome, 
                Email = usuarioDto.Email,
                Idade = usuarioDto.Idade,
                Telefone = usuarioDto.Telefone,
                Endereco = usuarioDto.Endereco,
                DataNascimento = usuarioDto.DataNascimento,
                Senha = usuarioDto.Senha // Certifique-se de que a senha está sendo tratada de forma segura (hash, etc.)

            };
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            return usuario;
        }

        public async Task<List<Usuario>> ListarUsuariosAsync()
        {
            return await _context.Usuarios.ToListAsync();
        }

        public async Task<Usuario?> AtualizarUsuarioAsync(int id, UsuarioDto usuarioDto)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
                return null;

            usuario.Nome = usuarioDto.Nome;
            usuario.Email = usuarioDto.Email;
            usuario.Idade = usuarioDto.Idade;
            usuario.Telefone = usuarioDto.Telefone;
            usuario.Endereco = usuarioDto.Endereco;
            usuario.DataNascimento = usuarioDto.DataNascimento;

            await _context.SaveChangesAsync();
            return usuario;
        }

        public async Task<bool> DeletarUsuarioAsync(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
                return false;
            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Usuario?> LoginAsync(LoginDto loginDto)
        {
            // Busca o usuário pelo e-mail
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Email == loginDto.Email); 

            if(usuario == null || usuario.Senha != loginDto.Senha)
            {
                return null; // Retorna null se o usuário não for encontrado ou a senha não corresponder
            }   

            return usuario;
        }

    }
}
