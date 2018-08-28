using Entity;
using Entity.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicalLayer
{
    public class TagBLL : IInsertable<Tag>, IUpdatable<Tag>, ISearchable<Tag>, IDeletable<Tag>
    {
        public BLLResponse<Tag> Delete(Tag item)
        {
            throw new NotImplementedException();
        }

        public BLLResponse<Tag> Insert(Tag item)
        {
            throw new NotImplementedException();
        }

        public BLLResponse<Tag> LerPorId(Tag item)
        {
            throw new NotImplementedException();
        }

        public BLLResponse<Tag> LerTodos()
        {
            throw new NotImplementedException();
        }

        public BLLResponse<Tag> Update(Tag item)
        {
            throw new NotImplementedException();
        }
    }
}
