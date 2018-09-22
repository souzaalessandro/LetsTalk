using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicalLayer
{
   public class MensagensPadrao
    {
        public static string CampoNuloMessage(string nomeCampo)
        {
            return $"{nomeCampo} deve ser informado";
        }

        public static string MaxCharsMessage(string nomeCampo, byte maxChars)
        {
            return $"{nomeCampo} deve conter no máximo {maxChars} caracteres";
        }

        public static string MinCharsMessage(string nomeCampo, byte minChars)
        {
            return $"{nomeCampo} deve conter no mínimo {minChars} caracteres";
        }

        public static string IdadeMinimaMessage(byte MinIdade)
        {
            return $"Idade deve de ser no mínimo {MinIdade} anos";
        }

        public static string IdadeExcedidaMessage(byte MaxIdade)
        {
            return $"Idade pode ser no máximo {MaxIdade} anos";
        }

        public static string EnumInvalidoMessage(string campoEnum)
        {
            return $"{campoEnum} deve ser algum valor do campo selecionado";
        }

        public static string UserOuSenhaInvalidosMessage()
        {
            return "Email ou senha inválidos";
        }

        public static string EmailInvalidoMessage()
        {
            return $"Este email não é válido";
        }

        public static string EmailExistenteMessage()
        {
            return $"Email já cadastrado!";

        }

    }
}
