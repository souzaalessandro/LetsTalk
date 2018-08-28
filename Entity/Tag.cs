using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Tag
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public List<Usuario> PerfilUsuarios { get; set; }
    }
}
