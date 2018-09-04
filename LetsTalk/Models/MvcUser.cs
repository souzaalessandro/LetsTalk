using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace LetsTalk.Models
{
    public class MvcUser : GenericPrincipal
    {
        public MvcUser(IIdentity identity, string[] roles) : base(identity, roles)
        {
        }
        public int ID { get; set; }
        public string Usuario { get; set; }
        public string FullPathFotoPerfil { get; set; }
    }
}