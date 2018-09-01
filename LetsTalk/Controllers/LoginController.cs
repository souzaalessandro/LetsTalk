using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
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
        public ActionResult Logar(Usuario usuario, string senha, bool lembrar = false)
        {
            //Vai no banco e se funcionar roda esta treta
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, FormsAuthentication.FormsCookieName, DateTime.Now, DateTime.Now.AddDays(1), lembrar, usuario.ID.ToString());
            string cookieEncriptado = FormsAuthentication.Encrypt(ticket);
            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, cookieEncriptado);
            Response.Cookies.Add(cookie);

            return RedirectToAction("Algumlugar");
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