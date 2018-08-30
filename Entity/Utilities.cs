using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Utilities
    {
        public string FormatarNome(string nome)
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
    }

}
