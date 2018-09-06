using BusinessLogicalLayer.Interfaces;
using DataAccessObject;
using Entity;
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

            BLLResponse<Tag> response = new BLLResponse<Tag>();
            using (LTContext ctx = new LTContext())
            {
                ctx.Tags.Remove(item);
                ctx.SaveChanges();
            }
            response.Sucesso = true;
            return response;

        }

        public BLLResponse<Tag> Insert(Tag item)
        {

            BLLResponse<Tag> response = new BLLResponse<Tag>();
            using (LTContext ctx = new LTContext())
            {
                ctx.Tags.Add(item);
                ctx.SaveChanges();
            }
            response.Sucesso = true;
            return response;
        }

        public BLLResponse<Tag> LerPorId(int id)
        {
            BLLResponse<Tag> response = new BLLResponse<Tag>();
            Tag tag = new Tag();
            using (LTContext ctx = new LTContext())
            {
                tag = ctx.Tags.FirstOrDefault(u => u.ID == id);
            }
            response.Sucesso = tag != null;
            response.Data = tag;
            return response;
        }

        public BLLResponse<Tag> LerTodos()
        {
            BLLResponse<Tag> response = new BLLResponse<Tag>();
            using (LTContext ctx = new LTContext())
            {
                response.DataList = ctx.Tags.ToList();
            }
            return response;
        }

        public BLLResponse<Tag> Update(Tag item)
        {
            BLLResponse<Tag> response = new BLLResponse<Tag>();
            Tag tag = new Tag();
            using (LTContext ctx = new LTContext())
            {
                tag = ctx.Tags.FirstOrDefault(u => u.ID == item.ID);
                response.Sucesso = tag != null;
                tag.ClonarDe(item);
                ctx.SaveChanges();
            }
            return response;
        }
    }
}

