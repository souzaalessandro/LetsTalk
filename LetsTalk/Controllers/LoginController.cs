using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using BusinessLogicalLayer;
using Entity;

namespace MexendoNoTemplate.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        [AllowAnonymous]
        public ActionResult Index()
        {
            Usuario user = new Usuario();
            if (TempData["ViewData"] != null)
            {
                ViewData = (ViewDataDictionary)TempData["ViewData"];
                user = (Usuario)TempData["login"];
            }
            return View(user);
        }

        [HttpPost, ValidateAntiForgeryToken, AllowAnonymous]
        public ActionResult Logar(Usuario usuario, bool lembrar = false)
        {
            BLLResponse<Usuario> response = new UsuarioBLL().IsLoginValido(usuario);

            if (response.Sucesso)
            {
                CriarCookie(lembrar, response);
                return RedirectToAction("Index", "Conhecer");
            }
            else
            {
                ModelState.AddModelError("Invalido", "Usuário ou senha inválidos");
                return View("Index", usuario);
            }
        }

        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }


        private void CriarCookie(bool lembrar, BLLResponse<Usuario> response)
        {
            FormsAuthenticationTicket ticket =
                new FormsAuthenticationTicket(1, FormsAuthentication.FormsCookieName, DateTime.Now, DateTime.Now.AddDays(1), lembrar, response.Data.ID.ToString());
            string cookieEncriptado = FormsAuthentication.Encrypt(ticket);
            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, cookieEncriptado);
            //cookie.Expires.AddDays(1);
            cookie.Expires.AddSeconds(20.0);
            Response.Cookies.Add(cookie);
        }
    }
}