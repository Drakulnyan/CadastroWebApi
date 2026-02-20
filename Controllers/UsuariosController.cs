using CadastroWebApi.Application.Services;
using CadastroWebApi.Application.DTOs;  
using Microsoft.AspNetCore.Mvc;
using CadastroWebApi.Domain.Entities;
using CadastroWebApi.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace CadastroWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        public UsuariosController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UsuarioDto usuarioDto)
        {
            if (usuarioDto == null || string.IsNullOrEmpty(usuarioDto.Nome) || string.IsNullOrEmpty(usuarioDto.Email))
            {
                return BadRequest("Dados inválidos.");
            }
            var usuario = await _usuarioService.AdicionarUsuarioAsync(usuarioDto);
            return CreatedAtAction(nameof(Post), new { id = usuario.Id }, usuario);
        }

        [HttpGet]
        public async Task<ActionResult<List<Usuario>>> GetUsuarios()
        {
            var usuarios = await _usuarioService.ListarUsuariosAsync();
            return Ok(usuarios);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UsuarioDto usuarioDto)
        {
            if (usuarioDto == null)
                return BadRequest("Dados inválidos.");

            var usuarioAtualizado = await _usuarioService.AtualizarUsuarioAsync(id, usuarioDto);
            if (usuarioAtualizado == null)
                return NotFound("Usuário não encontrado.");

            return Ok(usuarioAtualizado);
        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> Delete(int id)
        {
            var resultado = await _usuarioService.DeletarUsuarioAsync(id);
            if (!resultado)
                return NotFound("Usuário não encontrado.");
            return NoContent();
        }

    }
}
