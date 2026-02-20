using CadastroWebApi.Application.DTOs;
using CadastroWebApi.Infrastructure.Data;
using CadastroWebApi.Domain.Entities;
using CadastroWebApi.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;

namespace CadastroWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class LoginController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly AuthService _authService;

        public LoginController(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _authService = new AuthService(configuration["Jwt:Key"]!);
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Email == loginDto.Email && u.Senha == loginDto.Senha);

            if (usuario == null)
                return Unauthorized("Usuário ou senha inválidos.");

            var token = _authService.GenerateToken(usuario);

            // Adiciona o token como cookie seguro e HttpOnly
            Response.Cookies.Append("AuthToken", token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTimeOffset.UtcNow.AddHours(1)
            });
            return Ok(new { token });
        }

        [HttpPost("logout")]
        [Authorize]
        public IActionResult Logout()
        {
            // Remove o cookie de autenticação
            Response.Cookies.Append("AuthToken", "", new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTimeOffset.UtcNow.AddDays(-1) // Expira o cookie imediatamente
            });
            return Ok("Logout realizado com sucesso.");
        }
    }
}
