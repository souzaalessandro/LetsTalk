using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
    }
}