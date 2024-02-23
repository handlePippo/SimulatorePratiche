using GestionePratiche.Models;
using Microsoft.EntityFrameworkCore;

namespace GestionePratiche.Repository
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=PC-Filippo;Database=Gestione_Pratiche;Trusted_Connection=True;TrustServerCertificate=True;");
        }
        public DbSet<Pratiche> ListPratiche { get; set; }
    }
}