using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccessObject;
using Entity;
using LetsTalk.Models;
using System.Data.Entity;

namespace LetsTalk.Controllers
{
    [Authorize]
    public class ConhecerController : Controller
    {
        // GET: Conhecer
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult VisualizarPerfil()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SalvarCoordenadas(Coordenada coordenada)
        {
            MvcUser user = (MvcUser)System.Web.HttpContext.Current.User;
            using (LTContext ctx = new LTContext())
            {
                Usuario userDoDb = ctx.Usuarios.Include(u => u.UltimaCoordenada).FirstOrDefault(u => u.ID == user.ID);
                if (userDoDb == null)
                {
                    //return
                }
                userDoDb.UltimaCoordenada = new Coordenada(coordenada.Latitude, coordenada.Longitude);
                ctx.SaveChanges();
            }
            return Content("Coordenadas salvas no usuário");
        }
    }
}