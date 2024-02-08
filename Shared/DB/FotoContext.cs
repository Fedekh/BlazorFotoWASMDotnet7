using BlazorFotoWASMDotnet7.Shared.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace BlazorFotoWASMDotnet7.Shared.DB
{
    public class FotoContext : IdentityDbContext
    {
        public DbSet<Foto> Foto { get; set; }

        private readonly IConfiguration _configuration;

        public FotoContext(DbContextOptions<FotoContext> options, IConfiguration configuration)
        {
            _configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
       
            //if (!optionsBuilder.IsConfigured)
            //{
            //    string connectionString = _configuration.GetConnectionString("DefaultConnection");
            //    optionsBuilder.UseSqlServer(connectionString, b => b.MigrationsAssembly("BlazorFotoWASMDotnet7.Server")); //dato che abbiamo db in shared
            //}
            if (!optionsBuilder.IsConfigured)
            {
                string connectionString = _configuration.GetConnectionString("OfficeConnection");
                optionsBuilder.UseSqlServer(connectionString, b => b.MigrationsAssembly("BlazorFotoWASMDotnet7.Server")); //dato che abbiamo db in shared
            }

        }
    }
}