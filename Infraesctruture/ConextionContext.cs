using ClientsAPI.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace ClientsAPI.Infraesctruture
{
    public class ConextionContext
    {
        public class ConectionContext : DbContext
        {
            public DbSet<Superusuario> superusuarios { get; set; }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql(
                "Server=localhost;" +
                "Port=5432;" +
                "Database=BaseClientesAPI;" +
                "User Id=postgres;" +
                "Password='';");
        }
    }
}
