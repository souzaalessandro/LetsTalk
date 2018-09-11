using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;

namespace DataAccessObject
{
    public class LTContext : DbContext
    {
        public LTContext() : base("LTContext")
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Diretorio> Diretorios { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>().Ignore(s => s.Senha);

            modelBuilder.Entity<Usuario>().Property(p => p.Latitude).HasPrecision(18, 6);
            modelBuilder.Entity<Usuario>().Property(p => p.Longitude).HasPrecision(18, 6);
        }
    }
}
