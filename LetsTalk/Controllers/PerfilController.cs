using BusinessLogicalLayer;
using Entity;
using LetsTalk.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MexendoNoTemplate.Controllers
{
    //[Authorize(Roles = "Adm")]
    [Authorize]
    public class PerfilController : Controller
    {
        public ActionResult Index()
        {
            return View(new Usuario());
        }

        public ActionResult SalvarFoto(HttpPostedFileBase foto)
        {
            MvcUser user = (MvcUser)System.Web.HttpContext.Current.User;
            if (foto != null && IsImagemValida(foto))
            {
                string pathRelativo = GetPathFoto(foto, user);
                var result = new UsuarioBLL().AtualizarFotoPerfil(user.ID, pathRelativo);
                if (result.Sucesso)
                {
                    //se precisar retornar algum aviso que funcionou ou recarregar a página
                    return View("Index");
                }
            }
            return View("Index");
        }

        private string GetPathFoto(HttpPostedFileBase foto, MvcUser user)
        {
            string folder = Path.Combine(Server.MapPath("~/UserImages"), $"userperfil-{user.ID}");
            Directory.CreateDirectory(folder);
            string path = Path.Combine(folder, Path.GetFileName(foto.FileName));
            foto.SaveAs(path);
            string pathRelativo = $"/UserImages/userperfil-{user.ID}/{Path.GetFileName(foto.FileName)}";
            return pathRelativo;
        }

        private bool IsImagemValida(HttpPostedFileBase file)
        {
            byte tamanhoMega = 2;
            string[] formats = new string[] { ".jpg", ".png", ".jpeg" };
            bool validType = file.ContentType.Contains("image");
            bool validSize = file.ContentLength <= tamanhoMega * 1024 * 1024;
            bool validExtension = formats.Any(item => file.FileName.EndsWith(item, StringComparison.OrdinalIgnoreCase));

            return validType && validSize && validExtension;
        }

        [HttpPost]
        public ActionResult SalvarInformacoesPessoais(UsuarioViewModel usuaviewModel)
        {
            Usuario user = new Usuario();
           // List<Tag> tags = new List<Tag>();

            //usuaviewModel.Tags.Split(' ').ToList().ForEach(t => tags.Add(new Tag()
            //{
            //    Nome = t
            //}));
            //UsuarioBLL usuarioBLL = new UsuarioBLL();
            //MvcUser user = (MvcUser)System.Web.HttpContext.Current.User;
            //BLLResponse<Usuario> usuarioAtual = usuarioBLL.LerPorID(usuario.ID);
            //usuario.Descricao = user.ClonarDe()
            //usuarioBLL.Update(usuario);
            return RedirectToAction("Index");
        }
    }
    public class UsuarioViewModel
    {
        public string Frase { get; set; }
        public string Tags{ get; set; }
        public string Descricao{ get; set; }
    }
}