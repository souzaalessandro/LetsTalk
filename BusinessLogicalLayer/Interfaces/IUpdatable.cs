using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicalLayer.Interfaces
{
    public interface IUpdatable<T> where T : class
    {
        BLLResponse<T> Update(T item);
    }
}
