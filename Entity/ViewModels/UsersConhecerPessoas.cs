using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.ViewModels
{
    public class UsersConhecerPessoas
    {
        public UsersConhecerPessoas()
        {
            Usuarios = new List<Usuario>();
        }
        public List<Usuario> Usuarios { get; set; }
        public int PaginaAtual { get; set; }
        public int NumeroTagsComum { get; set; }
        public int QtdPessoasPagina { get; set; }
        public int IdadeMinima { get; set; }
        public int IdadeMaxima { get; set; }
        public int NumeroColunas { get; set; }
    }
}
