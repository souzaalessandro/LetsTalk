using BusinessLogicalLayer;
using Entity;
using Entity.ViewModels;
using LetsTalk.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LetsTalk.Controllers
{
    //[Authorize(Roles = "Adm")]
    [Authorize]
    public class PerfilController : Controller
    {
        public ActionResult Index()
        {
            MvcUser user = (MvcUser)System.Web.HttpContext.Current.User;
            BLLResponse<Usuario> response = new UsuarioBLL().LerPorId(user.ID);
            return View(response.Data);
        }

        public ActionResult MostrarDadosExistente()
        {
            MvcUser user = (MvcUser)System.Web.HttpContext.Current.User;
            BLLResponse<Usuario> response = new UsuarioBLL().LerPorId(user.ID);


            return View(response.Data);
        }

        [HttpPost]
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
                    return RedirectToAction("Index");
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
        public ActionResult SalvarInformacoesPessoais(UsuarioViewModel userVM)
        {
            MvcUser user = (MvcUser)System.Web.HttpContext.Current.User;
            userVM.ID = user.ID;

            BLLResponse<Usuario> response = new UsuarioBLL().Update(userVM);
            return Json(new { sucesso = response.Sucesso, mensagem = response.Mensagem });
        }

        [HttpPost]
        public ActionResult AtualizarSenha(string senhaNova)
        {
            MvcUser user = (MvcUser)System.Web.HttpContext.Current.User;
            Usuario usuario = new Usuario
            {
                ID = user.ID,
                Senha = senhaNova
            };

            BLLResponse<Usuario> response = new UsuarioBLL().UpdatePassword(usuario);
            if (response.Sucesso)
            {
                return Json(new { sucesso = true, mensagem = response.Mensagem });
            }
            else
            {
                return Json(new { sucesso = false, mensagem = response.Mensagem });
            }
        }
    }
}