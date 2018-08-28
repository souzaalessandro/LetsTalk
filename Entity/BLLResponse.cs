using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class BLLResponse<T> where T: class
    {
        public bool Sucesso { get; set; }
        public List<T> DataList { get; set; }
        public T Data { get; set; }
        public string Mensagem { get; set; }
    }
}
