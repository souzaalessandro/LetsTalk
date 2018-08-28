using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;

namespace Entity.Interfaces
{
    public interface IInsertable<T> where T : class
    {
        BLLResponse<T> Insert(T item);
    }
}
