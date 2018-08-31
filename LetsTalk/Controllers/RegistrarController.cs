using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLogicalLayer;
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

        [HttpPost, ActionName("RegistrarNovo")]
        public ActionResult Registrar(Usuario usuario)
        {
            BLLResponse<Usuario> response = new UsuarioBLL().Insert(usuario);

            foreach (var item in response.Erros)
            {
                ModelState.AddModelError(item.FieldName, item.Message);
            }

            if (ModelState.Count == 0)
            {
                return RedirectToAction("ConhecerPessoas", "ConhecerController");
            }

            return View("Registrar");
        }
    }
}