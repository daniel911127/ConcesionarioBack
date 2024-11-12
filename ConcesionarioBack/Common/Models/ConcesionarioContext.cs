using Microsoft.EntityFrameworkCore;

namespace ConcesionarioBack.Common.Models
{
    public class ConcesionarioContext : DbContext
    {
        public ConcesionarioContext(DbContextOptions<ConcesionarioContext> options) : base(options)
        {
        }

        public DbSet<Carro> Carros { get; set; }
        public DbSet<Moto> Motos { get; set; }
        public DbSet<Venta> Ventas { get; set; }
        public DbSet<ListadoCarro> ListadoCarros { get; set; }
        public DbSet<ListadoMoto> ListadoMotos { get; set; }
    }
}
