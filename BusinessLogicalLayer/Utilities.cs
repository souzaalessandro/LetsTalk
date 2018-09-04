using Entity.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.Extensions;
using System.Text.RegularExpressions;
using System.Globalization;

namespace BusinessLogicalLayer
{
    internal class Utilities
    {
        public static string FormatarNome(string nome)
        {
            string lower = nome.ToLower();
            string[] nomes = lower.Split(' ');
            for (int i = 0; i < nomes.Length; i++)
            {
                nomes[i] = nomes[i].Substring(0, 1).ToUpper() + nomes[i].Substring(1);
            }
            nome = String.Join(" ", nomes);
            return nome;
        }

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

        internal static string UserOuSenhaInvalidosMessage()
        {
            return "Email ou senha inválidos";
        }

        public static string EmailInvalidoMessage()
        {
            return $"Este email não é válido";
        }

        static bool invalid = false;
        private static string DomainMapper(Match match)
        {
            IdnMapping idn = new IdnMapping();

            string domainName = match.Groups[2].Value;
            try
            {
                domainName = idn.GetAscii(domainName);
            }
            catch (ArgumentException)
            {
                invalid = true;
            }
            return match.Groups[1].Value + domainName;
        }
        public static bool IsEmailValido(string email)
        {
            invalid = false;
            if (email.IsNullOrWhiteSpace())
            {
                return false;
            }

            try
            {
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }

            if (invalid)
            {
                return false;
            }
            try
            {
                return Regex.IsMatch(email,
                      @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                      @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                      RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        internal static string EmailExistenteMessage()
        {
            return $"Email já cadastrado!";

        }
    }

}
