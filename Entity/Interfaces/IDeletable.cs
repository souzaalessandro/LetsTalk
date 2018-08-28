﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Interfaces
{
    public interface IDeletable<T> where T : class
    {
        BLLResponse<T> Delete(T item);
    }
}
