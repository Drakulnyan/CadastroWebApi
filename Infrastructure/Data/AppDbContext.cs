using CadastroWebApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CadastroWebApi.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }

        // Se precisar, adicione métodos de configuração extra aqui
        // protected override void OnModelCreating(ModelBuilder modelBuilder)
        // {
        //     base.OnModelCreating(modelBuilder);
        // }
    }
}
