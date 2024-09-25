using Microsoft.EntityFrameworkCore;

namespace DBCodeFirst
{
    public class CodeFirstContext: DbContext
    {
        public CodeFirstContext(DbContextOptions<CodeFirstContext> options)
            : base(options)
        {
        }

        public DbSet<Listado> Listados { get; set; }
        public DbSet<ListadoTemporal> ListadosTemporal { get; set; }

        
    }
}
