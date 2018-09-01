using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;

namespace BusinessLogicalLayer
{
    public class BLLResponse<T> where T : class
    {
        public bool Sucesso { get; set; }
        public List<T> DataList { get; set; }
        public T Data { get; set; }
        public string Mensagem { get; set; }
        public List<ErrorField> Erros { get; set; }
        internal bool HasErros { get { return Erros.Count > 0; } }

    }
}
