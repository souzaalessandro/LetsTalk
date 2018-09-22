using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessObject;
using Entity;

namespace BusinessLogicalLayer
{
   public class LoginBLL
    {
        public BLLResponse<Usuario> IsLoginValido(Usuario item)
        {
            BLLResponse<Usuario> response = new BLLResponse<Usuario>();
            response.Erros = new List<ErrorField>();

            using (LTContext ctx = new LTContext())
            {
                Usuario userDoBanco = ctx.Usuarios.FirstOrDefault(u => u.Email == item.Email);
                if (userDoBanco == null)
                {
                    response.Erros.Add(new ErrorField(fieldName: nameof(userDoBanco.Email),
                        message: MensagensPadrao.UserOuSenhaInvalidosMessage()));
                    return response;
                }
                response.Sucesso = Criptografia.Verificar(item.Senha, userDoBanco.Salt, userDoBanco.Hash);
                if (response.Sucesso)
                {
                    response.Data = userDoBanco;
                }
            }
            return response;
        }

    }
}
