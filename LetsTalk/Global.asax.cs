using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using LetsTalk.Models;

namespace LetsTalk
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];

            if (cookie != null)
            {
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(cookie.Value);
                FormsIdentity identity = new FormsIdentity(ticket);

                byte[] buffer = Convert.FromBase64String(ticket.UserData); 
                UserLogado userLog = new UserLogado();
                using (Stream myStream = new MemoryStream(buffer))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    userLog = (UserLogado)formatter.Deserialize(myStream);
                }

                //MVCUser user = new MVCUser(identity, new string[] {"Adm","FInanceiro" });
                MvcUser user = new MvcUser(identity, null)
                {
                    ID = userLog.ID,
                    Nome = userLog.Nome,
                    PathFotoPerfil = userLog.PathFotoPerfil
                };

                HttpContext.Current.User = user;
            }
        }
    }
}
