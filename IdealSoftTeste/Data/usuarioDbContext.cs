using IdealSoftTeste.Models;
using Microsoft.EntityFrameworkCore;

namespace IdealSoftTeste.Data
{
    public class usuarioDbContext : DbContext
    {
        public usuarioDbContext(DbContextOptions<usuarioDbContext> options) : base(options)
        {
        }
        public DbSet<Usuario> Usuarios => Set<Usuario>();
    }
}
