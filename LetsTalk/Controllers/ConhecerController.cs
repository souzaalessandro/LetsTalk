using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccessObject;
using Entity;
using LetsTalk.Models;
using System.Data.Entity;
using BusinessLogicalLayer;


namespace LetsTalk.Controllers
{
    [Authorize]
    public class ConhecerController : Controller
    {
        // GET: Conhecer
        public ActionResult Index()
        {
            MvcUser user = (MvcUser)System.Web.HttpContext.Current.User;
            List<Usuario> usersFiltrados = null;

            if (TempData["Usuarios"] != null)
            {
                usersFiltrados = (List<Usuario>)TempData["Usuarios"];
            }
            if (usersFiltrados != null)
            {
                return View(usersFiltrados);
            }

            using (LTContext ctx = new LTContext())
            {
                var users = ctx.Usuarios.Where(u => u.ID != user.ID).ToList();
                return View(users);
            }
        }

        [HttpPost]
        public ActionResult Filtrar(int idadeMin, int idadeMax, int tagsComum)
        {
            MvcUser user = (MvcUser)System.Web.HttpContext.Current.User;

            using (LTContext ctx = new LTContext())
            {
                Usuario userDb = ctx.Usuarios.Find(user.ID);

                DateTime menor = new DateTime(DateTime.Now.Year - idadeMin, DateTime.Now.Month, DateTime.Now.Day);
                DateTime maior = new DateTime(DateTime.Now.Year - idadeMax, DateTime.Now.Month, DateTime.Now.Day);

                var filtroEunao = ctx.Usuarios.Where(u => u.ID != user.ID);
                var filtroIdade = filtroEunao.Where(u => u.DataNascimento < menor && u.DataNascimento > maior);
                var filtroTags = filtroIdade.Where(u => (u.Tags.Split(',').Intersect(userDb.Tags.Split(',')).Count() >= tagsComum)).ToList();

                TempData["Usuarios"] = filtroTags;

                return RedirectToAction("Index");
            }
        }

        public ActionResult VisualizarPerfil()
        {
            MvcUser user = (MvcUser)System.Web.HttpContext.Current.User;
            BLLResponse<Usuario> response = new UsuarioBLL().LerPorId(user.ID);
            return View(response.Data);
        }

        public ActionResult GetUsuarios()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SalvarCoordenadas(Coordenada coordenada)
        {
            MvcUser user = (MvcUser)System.Web.HttpContext.Current.User;
            using (LTContext ctx = new LTContext())
            {
                Usuario userDoDb = ctx.Usuarios.FirstOrDefault(u => u.ID == user.ID);
                if (userDoDb == null)
                {
                    //return
                }
                userDoDb.Latitude = coordenada.Latitude;
                userDoDb.Longitude = coordenada.Longitude;
                ctx.SaveChanges();
            }
            return Content("Coordenadas salvas no usuário");
        }
    }
}