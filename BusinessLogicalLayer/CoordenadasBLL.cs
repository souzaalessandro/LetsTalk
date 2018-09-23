using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessObject;
using Entity;

namespace BusinessLogicalLayer
{
    public class CoordenadasBLL
    {
        public BLLResponse<Coordenada> SalvarCoordenadas(Coordenada coordenada, int id)
        {
            BLLResponse<Coordenada> response = new BLLResponse<Coordenada>();

            using (LTContext ctx = new LTContext())
            {
                Usuario userDoDb = ctx.Usuarios.Find(id);
                if (userDoDb != null)
                {
                    userDoDb.Latitude = coordenada.Latitude;
                    userDoDb.Longitude = coordenada.Longitude;
                    ctx.SaveChanges();

                    response.Mensagem = "Local salvo com sucesso";
                    response.Sucesso = true;
                    return response;
                }
            }
            response.Mensagem = "Algo de errado ocorreu";
            return response;
        }
    }
}
