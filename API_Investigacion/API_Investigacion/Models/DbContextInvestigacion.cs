using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace API_Investigacion.Models
{
    public class DbContextInvestigacion : DbContext
    {
        // Constructor con parametros para manejar la configuracion del ORM
        public DbContextInvestigacion(DbContextOptions<DbContextInvestigacion> options) : base(options)
        {

        }
        public DbSet<Producto> Productos { get; set; }

    }
}
