using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Entity;

namespace LetsTalk.Controllers
{
    public class RegistrarController : Controller
    {
        // GET: Registrar
       [HttpGet]
        public ActionResult Registrar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registrar(Usuario usuario)
        {
            //validar
            //se der certo, redirecionar para alguma página
            ModelState.AddModelError("FieldName", "Message");
            //se não


            return View();
        }
    }
}