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
    public class UsuarioBLL : IUpdatable<Usuario>, ISearchable<Usuario>, IDeletable<Usuario>
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

        #region validacoes
        private void ValidarString(Usuario user, PropertyInfo prop, List<ErrorField> errors, byte minCaracteres = 3, byte maxCaracteres = 20)
        {
            string valorCampo = user.GetType().GetProperty(prop.Name).GetValue(user) as string;

            if (valorCampo.IsNullOrWhiteSpace())
            {
                errors.Add(new ErrorField(fieldName: prop.Name,
                    message: Utilities.CampoNuloMessage(prop.Name)));
                return;
            }
            else if (valorCampo.Length < minCaracteres)
            {
                errors.Add(new ErrorField(fieldName: prop.Name,
                    message: Utilities.MinCharsMessage(prop.Name, minCaracteres)));
                return;
            }
            else if (valorCampo.Length > maxCaracteres)
            {
                errors.Add(new ErrorField(fieldName: prop.Name,
                    message: Utilities.MaxCharsMessage(prop.Name, maxCaracteres)));
                return;
            }
            string formatado = Utilities.FormatarNome(valorCampo);
            prop.SetValue(user, formatado);
        }

        private void ValidarIdade(Usuario user, List<ErrorField> errors, byte idadeMinima = 18, byte idadeMaxima = 80)
        {
            if (user.DataNascimento == DateTime.MinValue)
            {
                errors.Add(new ErrorField(fieldName: nameof(user.DataNascimento),
                    message: Utilities.CampoNuloMessage("Data de nascimento")));
                return;
            }

            bool ehMenorIdade = (DateTime.Now - user.DataNascimento).TotalDays / 365 <= idadeMinima;
            bool idadeExcedida = (DateTime.Now - user.DataNascimento).TotalDays / 365 > idadeMaxima;

            if (ehMenorIdade)
            {
                errors.Add(new ErrorField(fieldName: nameof(user.DataNascimento),
                    message: Utilities.IdadeMinimaMessage(idadeMinima)));
            }
            else if (idadeExcedida)
            {
                errors.Add(new ErrorField(fieldName: nameof(user.DataNascimento),
                    message: Utilities.IdadeExcedidaMessage(idadeMaxima)));
            }
        }

        private void ValidarGenero(Usuario item, List<ErrorField> errors)
        {
            int valueDaCmb = (int)item.Genero;
            bool valid = false;
            Genero[] generos = (Genero[])Enum.GetValues(typeof(Genero));
            foreach (Genero g in generos)
            {
                valid = valueDaCmb == (int)g;
                if (valid)
                {
                    break;
                }
            }
            if (!valid)
            {
                errors.Add(new ErrorField(nameof(item.Genero), Utilities.EnumInvalidoMessage("Gênero")));
            }
        }

        private void ValidarEmail(Usuario item, List<ErrorField> errors)
        {

            if (item.Email.IsNullOrWhiteSpace())
            {
                errors.Add(new ErrorField(fieldName: nameof(item.Email),
                    message: Utilities.CampoNuloMessage(nameof(item.Email))));
            }
            else if (!Utilities.IsEmailValido(item.Email))
            {
                errors.Add(new ErrorField(fieldName: nameof(item.Email),
                    message: Utilities.EmailInvalidoMessage()));
            }
            else if (EmailJaExiste(item, errors))
            {
                errors.Add(new ErrorField(fieldName: nameof(item.Email),
                    message: Utilities.EmailExistenteMessage()));
            }
        }

        private void ValidarSenha(Usuario item, List<ErrorField> errors, string senhaRepetida, byte minCaracteres = 5, byte maxCaracteres = 12)
        {
            if (item.Senha.IsNullOrWhiteSpace())
            {
                errors.Add(new ErrorField(fieldName: nameof(item.Senha),
                    message: Utilities.CampoNuloMessage(nameof(item.Senha))));
                return;
            }
            else if (item.Senha.Length < minCaracteres)
            {
                errors.Add(new ErrorField(fieldName: nameof(item.Senha),
                   message: Utilities.MinCharsMessage(nameof(item.Senha), minCaracteres)));
            }
            else if (item.Senha.Length > maxCaracteres)
            {
                errors.Add(new ErrorField(fieldName: nameof(item.Senha),
                    message: Utilities.MaxCharsMessage(nameof(item.Senha), maxCaracteres)));
            }

            if (item.Senha != senhaRepetida)
            {
                errors.Add(new ErrorField(fieldName: "SenhasDiferentes",
                   message: "Senhas digitadas não batem"));
                return;
            }
        }
        private bool EmailJaExiste(Usuario item, List<ErrorField> errors)
        {
            using (LTContext context = new LTContext())
            {
                Usuario usuario = context.Usuarios.FirstOrDefault(u => u.Email == item.Email);
                if (usuario == null)
                {
                    return false;
                }
                return true;
            }
        }
        #endregion



    }
}