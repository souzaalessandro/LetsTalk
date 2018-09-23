using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DataAccessObject;
using Entity;
using Entity.Enums;
using Entity.Extensions;

namespace BusinessLogicalLayer
{
    public class RegistroBLL
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

            Criptografia.EncriptografarEGuardarSalt(item);

            using (LTContext ctx = new LTContext())
            {
                ctx.Usuarios.Add(item);
                ctx.SaveChanges();
            }
            response.Sucesso = true;
            response.Data = item;
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

        private void ValidarString(Usuario user, PropertyInfo prop, List<ErrorField> errors, byte minCaracteres = 3, byte maxCaracteres = 20)
        {
            string valorCampo = user.GetType().GetProperty(prop.Name).GetValue(user) as string;

            if (valorCampo.IsNullOrWhiteSpace())
            {
                errors.Add(new ErrorField(fieldName: prop.Name,
                    message: MensagensPadrao.CampoNuloMessage(prop.Name)));
                return;
            }
            else if (valorCampo.Length < minCaracteres)
            {
                errors.Add(new ErrorField(fieldName: prop.Name,
                    message: MensagensPadrao.MinCharsMessage(prop.Name, minCaracteres)));
                return;
            }
            else if (valorCampo.Length > maxCaracteres)
            {
                errors.Add(new ErrorField(fieldName: prop.Name,
                    message: MensagensPadrao.MaxCharsMessage(prop.Name, maxCaracteres)));
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
                    message: MensagensPadrao.CampoNuloMessage("Data de nascimento")));
                return;
            }

            bool ehMenorIdade = (DateTime.Now - user.DataNascimento).TotalDays / 365 <= idadeMinima;
            bool idadeExcedida = (DateTime.Now - user.DataNascimento).TotalDays / 365 > idadeMaxima;

            if (ehMenorIdade)
            {
                errors.Add(new ErrorField(fieldName: nameof(user.DataNascimento),
                    message: MensagensPadrao.IdadeMinimaMessage(idadeMinima)));
            }
            else if (idadeExcedida)
            {
                errors.Add(new ErrorField(fieldName: nameof(user.DataNascimento),
                    message: MensagensPadrao.IdadeExcedidaMessage(idadeMaxima)));
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
                errors.Add(new ErrorField(nameof(item.Genero), MensagensPadrao.EnumInvalidoMessage("Gênero")));
            }
        }

        private void ValidarEmail(Usuario item, List<ErrorField> errors)
        {
            if (item.Email.IsNullOrWhiteSpace())
            {
                errors.Add(new ErrorField(fieldName: nameof(item.Email),
                    message: MensagensPadrao.CampoNuloMessage(nameof(item.Email))));
            }
            else if (!Utilities.IsEmailValido(item.Email))
            {
                errors.Add(new ErrorField(fieldName: nameof(item.Email),
                    message: MensagensPadrao.EmailInvalidoMessage()));
            }
            else if (EmailJaExiste(item, errors))
            {
                errors.Add(new ErrorField(fieldName: nameof(item.Email),
                    message: MensagensPadrao.EmailExistenteMessage()));
            }
        }

        private void ValidarSenha(Usuario item, List<ErrorField> errors, string senhaRepetida, byte minCaracteres = 5, byte maxCaracteres = 12)
        {
            if (item.Senha.IsNullOrWhiteSpace())
            {
                errors.Add(new ErrorField(fieldName: nameof(item.Senha),
                    message: MensagensPadrao.CampoNuloMessage(nameof(item.Senha))));
                return;
            }
            else if (item.Senha.Length < minCaracteres)
            {
                errors.Add(new ErrorField(fieldName: nameof(item.Senha),
                   message: MensagensPadrao.MinCharsMessage(nameof(item.Senha), minCaracteres)));
            }
            else if (item.Senha.Length > maxCaracteres)
            {
                errors.Add(new ErrorField(fieldName: nameof(item.Senha),
                    message: MensagensPadrao.MaxCharsMessage(nameof(item.Senha), maxCaracteres)));
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
    }
}
