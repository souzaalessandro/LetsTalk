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
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Logar(Usuario usuario, bool lembrar = false)
        {
            //Vai no banco e se funcionar roda esta treta
            BLLResponse<Usuario> response = new UsuarioBLL().IsLoginValido(usuario);
            if (response.Sucesso)
            {
                //FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, FormsAuthentication.FormsCookieName, DateTime.Now, DateTime.Now.AddDays(1), lembrar, usuario.ID.ToString());
                //string cookieEncriptado = FormsAuthentication.Encrypt(ticket);
                //HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, cookieEncriptado);
                //Response.Cookies.Add(cookie);
            }
            string hash = String.Join("", response.Data.Hash);
            return Content($"<h1>O login para usuario de email {usuario.Email} resultou em {response.Sucesso}. \nSeu hash é {hash}");
          //  return RedirectToAction("Algumlugar");
        }
    }

    class User : GenericPrincipal
    {
        public User(IIdentity identity, string[] roles) : base(identity, roles)
        {

        }

        public int ID { get; set; }
        public string UserName { get; set; }
    }
}