using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicalLayer.Interfaces;
using DataAccessObject;
using Entity;
using Entity.Enums;
using Entity.Extensions;

namespace BusinessLogicalLayer
{
    public partial class UsuarioBLL : IUpdatable<Usuario>, ISearchable<Usuario>, IDeletable<Usuario>
    {
        public BLLResponse<Usuario> Registrar(Usuario item, string senhaRepetida)
        {
            List<ErrorField> erros = ValidarUsuarioParaRegistro(item, senhaRepetida);
            BLLResponse<Usuario> response = new BLLResponse<Usuario>();
            response.Erros = erros;
            if (response.HasErros)
            {
                response.Sucesso = false;
                return response;
            }

            EncriptografarEGuardarSalt(item);

            using (LTContext ctx = new LTContext())
            {
                ctx.Usuarios.Add(item);
                ctx.SaveChanges();
            }
            response.Sucesso = true;
            return response;
        }

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
                        message: Utilities.UserOuSenhaInvalidosMessage()));
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

        private void EncriptografarEGuardarSalt(Usuario item)
        {
            byte[] salt;
            item.Hash = Criptografia.Encriptar(item.Senha, out salt);
            item.Salt = salt;
        }

        public BLLResponse<Usuario> Update(Usuario item)
        {
            //Fazer validações

            BLLResponse<Usuario> response = new BLLResponse<Usuario>();
            Usuario user = new Usuario();
            using (LTContext ctx = new LTContext())
            {
                user = ctx.Usuarios.FirstOrDefault(u => u.ID == item.ID);
                response.Sucesso = user != null;
                user.ClonarDe(item);
                ctx.SaveChanges();
            }
            return response;
        }

        public BLLResponse<Usuario> Delete(Usuario item)
        {
            BLLResponse<Usuario> response = new BLLResponse<Usuario>();
            using (LTContext ctx = new LTContext())
            {
                Usuario user = ctx.Usuarios.FirstOrDefault(u => u.ID == item.ID);
                ctx.Usuarios.Remove(user);
                ctx.SaveChanges();
            }
            response.Sucesso = true;
            response.Mensagem = "Deletado com sucesso.";
            return response;
        }

        public BLLResponse<Usuario> LerPorId(Usuario item)
        {
            BLLResponse<Usuario> response = new BLLResponse<Usuario>();
            Usuario user = new Usuario();
            using (LTContext ctx = new LTContext())
            {
                user = ctx.Usuarios.FirstOrDefault(u => u.ID == item.ID);
            }
            response.Sucesso = user != null;
            response.Data = user;
            return response;
        }

        public BLLResponse<Usuario> LerTodos()
        {
            BLLResponse<Usuario> response = new BLLResponse<Usuario>();
            using (LTContext ctx = new LTContext())
            {
                response.DataList = ctx.Usuarios.ToList();
            }
            return response;
        }

        private List<ErrorField> ValidarUsuarioParaRegistro(Usuario item, string senhaRepetida)
        {
            List<ErrorField> errors = new List<ErrorField>();

            var property = item.GetType().GetProperty(nameof(item.Nome));
            ValidarString(item, property, errors, 3, 20);

            property = item.GetType().GetProperty(nameof(item.Sobrenome));
            ValidarString(item, property, errors, 3, 25);

            ValidarIdade(item, errors);

            ValidarGenero(item, errors);

            ValidarEmail(item, errors);

            ValidarSenha(item, errors, senhaRepetida);

            return errors;
        }
    }
}