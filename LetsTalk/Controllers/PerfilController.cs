using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MexendoNoTemplate.Controllers
{
    //[Authorize(Roles = "Adm")]
    //[Authorize]
    public class PerfilController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }

}