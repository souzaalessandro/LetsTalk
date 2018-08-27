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
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Diretorio> Diretorios { get; set; }
        public DbSet<Tag> Tags { get; set; }
    }
}
