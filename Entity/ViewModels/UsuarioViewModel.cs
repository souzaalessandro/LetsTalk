using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entity;

namespace Entity.ViewModels
{
    public class UsuarioViewModel
    {
        public int ID { get; set; }
        public string Frase { get; set; }
        public string Tags { get; set; }
        public string Descricao { get; set; }
        public List<Diretorio> Diretorios { get; set; }
    }
}