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
    public partial class UsuarioBLL
    {
        private bool TagsEmComum(string tagsUser, string tagsUserLogado, int minTagsComum)
        {
            if (tagsUser.IsNullOrWhiteSpace() || tagsUserLogado.IsNullOrWhiteSpace())
            {
                return false;
            }
            List<string> userLinq = null;
            List<string> userLogado = null;
            userLinq = tagsUser.Contains(",") ? tagsUser.Split(',').ToList() : new List<string> { tagsUser };
            userLogado = tagsUserLogado.Contains(",") ? tagsUserLogado.Split(',').ToList() : new List<string>() { tagsUserLogado };

            var tagsEmComum = userLinq.Intersect(userLogado);
            return tagsEmComum.Count() >= minTagsComum;
        }
    }
}
