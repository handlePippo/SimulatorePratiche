using GestionePratiche.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace GestionePratiche.Repository
{
    public class DataContext : DbContext
    {
        private IConfiguration _configuration; 
        public DataContext(DbContextOptions<DataContext> options, IConfiguration configuration) : base(options) {
            this._configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(_configuration.GetSection("ConnectionString").Value);
        }
        public DbSet<Pratica> ListPratiche { get; set; }
    }
}