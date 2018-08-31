using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public static class ClonerExtension
    {
        /// <summary>
        /// Clona as propriedades de <paramref name="input"/> para o outro objeto de mesmo tipo <paramref name="output"/>.
        /// </summary>
        /// <typeparam name="T">A classe genérica</typeparam>
        /// <param name="output">O objeto a ser clonado para</param>
        /// <param name="input">O objeto a ser clonado de</param>
        public static void ClonarDe<T>(this T output, T input) where T : class
        {
            foreach (var item in typeof(T).GetProperties())
            {
                if (item.Name == "ID")
                {
                    continue;
                }
                item.SetValue(output, item.GetValue(input));
            }
        }
    }
}
