using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessObject;
using Entity;
using Entity.Interfaces;

namespace BusinessLogicalLayer
{
    public class UsuarioBLL : IInsertable<Usuario>, IUpdatable<Usuario>, ISearchable<Usuario>, IDeletable<Usuario>
    {
        public BLLResponse<Usuario> Delete(Usuario item)
        {
            throw new NotImplementedException();
        }

        public BLLResponse<Usuario> Insert(Usuario item)
        {
            BLLResponse<Usuario> response = new BLLResponse<Usuario>();
            using (LTContext ctx = new LTContext())
            {
                ctx.Usuarios.Add(item);
                ctx.SaveChanges();
            }
        }

        public BLLResponse<Usuario> LerPorId(Usuario item)
        {
            throw new NotImplementedException();
        }

        public BLLResponse<Usuario> LerTodos()
        {
            throw new NotImplementedException();
        }

        public BLLResponse<Usuario> Update(Usuario item)
        {
            throw new NotImplementedException();
        }
    }
}
