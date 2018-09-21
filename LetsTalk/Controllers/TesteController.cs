using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LetsTalk.Controllers
{
    //UISliders
    public class TesteController : Controller
    {
        // GET: Teste
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult TesteNovo()
        {
            return View();
        }

        public  ActionResult Teste(string imgbase64)
        {
            byte[] bytes = Convert.FromBase64String(imgbase64.Split(',')[1]);
            FileStream stream = new FileStream(Server.MapPath("~/UserImages/" + Guid.NewGuid() + ".png"), FileMode.Create);
            stream.Write(bytes, 0, bytes.Length);
            stream.Flush();
            TempData["Success"] = "Image uploaded successfully";

            return View();
        }
    }
}