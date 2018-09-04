using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
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
            return RedirectToAction("Index", "Home");
        }


        private void CriarCookie(bool lembrar, BLLResponse<Usuario> response)
        {
            string userData = null;
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream stream = new MemoryStream())
            {
                bf.Serialize(stream, response.Data);
                stream.Position = 0;
                byte[] buffer = new byte[stream.Length];
                stream.Read(buffer, 0, (int)stream.Length);
                userData = Convert.ToBase64String(buffer);
            }

            FormsAuthenticationTicket ticket =
                new FormsAuthenticationTicket(1, FormsAuthentication.FormsCookieName, DateTime.Now, DateTime.Now.AddDays(1), lembrar, userData);
            string cookieEncriptado = FormsAuthentication.Encrypt(ticket);
            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, cookieEncriptado);
            cookie.HttpOnly = true;
            cookie.Expires = DateTime.Now.AddDays(1);
            Response.Cookies.Add(cookie);
        }
    }
}