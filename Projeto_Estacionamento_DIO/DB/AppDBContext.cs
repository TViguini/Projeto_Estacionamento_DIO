using Microsoft.EntityFrameworkCore;

namespace Projeto_Estacionamento_DIO.DB
{
    public class AppDBContext : DbContext
    {
        public DbSet<Registro> Registro { get; set; }
        public DbSet<Historico> Historico { get; set; }

        private static readonly string DbPath = Path.Combine(AppContext.BaseDirectory, "database.db");

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");
    }
}