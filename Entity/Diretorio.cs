using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Diretorio
    {
        public int ID { get; set; }
        public string FullPath { get; set; }
        public Usuario Usuario { get; set; }
    }
}
