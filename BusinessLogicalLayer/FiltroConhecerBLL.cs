using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessObject;
using Entity;
using Entity.Extensions;

namespace BusinessLogicalLayer
{
    public class FiltroConhecerBLL
    {
        public List<Usuario> GetUsersComFiltro(int idadeMin, int idadeMax, int tagsComum, int userID)
        {
            using (LTContext ctx = new LTContext())
            {
                Usuario userDb = ctx.Usuarios.Find(userID);

                DateTime menor = new DateTime(DateTime.Now.Year - idadeMin, DateTime.Now.Month, DateTime.Now.Day);
                DateTime maior = new DateTime(DateTime.Now.Year - idadeMax, DateTime.Now.Month, DateTime.Now.Day);

                var filtroEunao = ctx.Usuarios.Where(u => u.ID != userID);
                var filtroIdade = filtroEunao.Where(u => u.DataNascimento < menor && u.DataNascimento > maior);
                var filtroTags = filtroIdade.AsEnumerable().Where(u => TagsEmComum(u.Tags, userDb.Tags, tagsComum)).ToList();

                return filtroTags;
            }
        }

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
