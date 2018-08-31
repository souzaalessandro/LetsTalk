using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Utilities
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

        public static string MensagemParaCampoNulo(string nomeCampo)
        {
            return $"{nomeCampo} deve ser informado.";
        }

        public static string  MensagemParaMaxChars(string nomeCampo, byte maxChars)
        {
            return $"{nomeCampo} deve conter no máximo {maxChars} caracteres.";
        }

        public static string MensagemParaMinChars(string nomeCampo, byte minChars)
        {
            return $"{nomeCampo} deve conter no mínimo {minChars} caracteres.";
        }
        public static string MensagemParaMenor18 (string nomeCampo, byte MinIdade)
        {
            return  $"{nomeCampo} deve de ser no mínimo {MinIdade} anos. ";
        }
    }

}
