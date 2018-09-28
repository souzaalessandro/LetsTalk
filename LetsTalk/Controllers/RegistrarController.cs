using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLogicalLayer;
using Entity;

namespace LetsTalk.Controllers
{
    [AllowAnonymous]
    public class RegistrarController : Controller
    {
        // GET: Registrar
        [Route("Registrar")]
        public ActionResult Index()
        {
            Usuario user = new Usuario();
            if (TempData["ViewData"] != null)
            {
                ViewData = (ViewDataDictionary)TempData["ViewData"];
                user = (Usuario)TempData["CamposInformados"];
            }
            return View(user);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Registrar(Usuario usuario, string senha, string senhaRepetida)
        {
            BLLResponse<Usuario> response = new RegistroBLL().Registrar(usuario, senhaRepetida);

            if (!response.Sucesso)
            {
                foreach (ErrorField item in response.Erros)
                {
                    ModelState.AddModelError(item.FieldName, item.Message);
                }
                TempData["ViewData"] = ViewData;
                TempData["CamposInformados"] = usuario;
                return RedirectToAction("Index");
            }
            TempData["NovoUser"] = response.Data;
            return RedirectToAction("LogarAposRegistro", "Login");
        }
    }
}