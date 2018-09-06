using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicalLayer.Interfaces
{
    public interface ISearchable<T> where T : class
    {
        BLLResponse<T> LerTodos();
        BLLResponse<T> LerPorId(int id);
    }
}
