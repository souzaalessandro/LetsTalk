using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LetsTalk.Controllers
{
    public class ErrosController : Controller
    {
        [Route("404")]
        public ActionResult ErroNotFound()
        {
            return View();
        }
    }
}