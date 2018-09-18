using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Entity;
using DataAccessObject;
using Entity.Enums;

namespace BusinessLogicalLayer
{
    public class FiltroUsuariosBLL
    {
        public BLLResponse<Usuario> GetUsuariosSemFiltro()
        {
            BLLResponse<Usuario> response = new BLLResponse<Usuario>();
            using (LTContext ctx = new LTContext())
            {
                var users = ctx.Usuarios.ToList();
                if (users.Any())
                {
                    response.DataList = users;
                    response.Sucesso = true;
                    return response;
                }
            }
            return response;
        }

        //implementar filtro da coordenada
        public BLLResponse<Usuario> GetUsuariosComFiltro
            (byte minTags = 1, byte idadeMin = 18, byte idadeMax = 80, Genero generos = Genero.Masculino | Genero.Feminino | Genero.Indeterminado)
        {
            BLLResponse<Usuario> response = new BLLResponse<Usuario>();
            using (LTContext ctx = new LTContext())
            {
                var filtroIdade = ctx.Usuarios.Where(u=>u.DataNascimento == DateTime.Now);

            }
            return response;
        }
    }
}
