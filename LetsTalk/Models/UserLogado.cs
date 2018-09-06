using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LetsTalk.Models
{
    [Serializable]
    public class UserLogado
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public string PathFotoPerfil { get; set; }
    }
}