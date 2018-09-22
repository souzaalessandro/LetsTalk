using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccessObject;
using Entity;
using LetsTalk.Models;
using System.Data.Entity;
using BusinessLogicalLayer;
using Entity.Extensions;

namespace LetsTalk.Controllers
{
    public class UsersConhecerPessoas
    {
        public UsersConhecerPessoas()
        {
            Usuarios = new List<Usuario>();
        }
        public List<Usuario> Usuarios { get; set; }
        public int PaginaAtual { get; set; }
        public int NumeroTagsComum { get; set; }
        public int QtdPessoasPagina { get; set; }
        public int IdadeMinima { get; set; }
        public int IdadeMaxima { get; set; }
        public int NumeroColunas { get; set; }
    }
    [Authorize]
    public class ConhecerController : Controller
    {
        // GET: Conhecer
        public ActionResult Index(int pagina = 1, int idadeMin = 18, int idadeMax = 80, int tagsComum = 1, int colunas = 4, int qntPorPagina = 15)
        {
            MvcUser user = (MvcUser)System.Web.HttpContext.Current.User;
            List<Usuario> users = new UsuarioBLL().GetUsersComFiltro(idadeMin, idadeMax, tagsComum, user.ID);

            int skip = (pagina - 1) * qntPorPagina;
            users = users.Skip(skip).Take(qntPorPagina).ToList();

            UsersConhecerPessoas modelo = new UsersConhecerPessoas()
            {
                Usuarios = users,
                PaginaAtual = pagina,
                NumeroTagsComum = tagsComum,
                QtdPessoasPagina = qntPorPagina,
                IdadeMinima = idadeMin,
                IdadeMaxima = idadeMax,
                NumeroColunas = colunas
            };

            return View(modelo);

            //if (TempData["Usuarios"] != null)
            //{
            //    usersFiltrados = (List<Usuario>)TempData["Usuarios"];
            //    qntPorPagina = (int)TempData["QntPorPagina"];
            //    colunas = (int)TempData["Colunas"];
            //}
            //if (usersFiltrados != null)
            //{
            //    return View(usersFiltrados);
            //}
            //ViewData["Colunas"] = colunas;
            //using (LTContext ctx = new LTContext())
            //{
            //    var users = ctx.Usuarios.Where(u => u.ID != user.ID).ToList();
            //    return View(users);
            //}
        }

        [HttpPost]
        public ActionResult Filtrar(int idadeMin, int idadeMax, int tagsComum, int qntPorPagina, int colunas)
        {
            MvcUser user = (MvcUser)System.Web.HttpContext.Current.User;
            List<Usuario> users = new UsuarioBLL().GetUsersComFiltro(idadeMin, idadeMax, tagsComum, user.ID);

            TempData["Usuarios"] = users;
            TempData["QntPorPagina"] = qntPorPagina;
            TempData["Colunas"] = colunas;

            return RedirectToAction("Index");
        }



        public ActionResult VisualizarPerfil()
        {
            MvcUser user = (MvcUser)System.Web.HttpContext.Current.User;
            BLLResponse<Usuario> response = new UsuarioBLL().LerPorId(user.ID);
            return View(response.Data);
        }

        public ActionResult GetUsuarios()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SalvarCoordenadas(Coordenada coordenada)
        {
            MvcUser user = (MvcUser)System.Web.HttpContext.Current.User;
            using (LTContext ctx = new LTContext())
            {
                Usuario userDoDb = ctx.Usuarios.FirstOrDefault(u => u.ID == user.ID);
                if (userDoDb == null)
                {
                    //return
                }
                userDoDb.Latitude = coordenada.Latitude;
                userDoDb.Longitude = coordenada.Longitude;
                ctx.SaveChanges();
            }
            return Content("Coordenadas salvas no usuário");
        }

        public ActionResult GetUser(int id)
        {
            BLLResponse<Usuario> response = new UsuarioBLL().LerPorId(id);
            object dados = new { suceso = false };
            if (response.Sucesso)
            {
                int idade = DateTime.Now.Year - response.Data.DataNascimento.Year;
                string[] tags = String.IsNullOrWhiteSpace(response.Data.Tags) ? new string[1] : response.Data.Tags.Split(',');

                dados = new
                {
                    sucesso = true,
                    nome = response.Data.Nome,
                    idade = idade,
                    foto = response.Data.PathFotoPerfil,
                    frase = response.Data.FraseApresentacao,
                    descricao = response.Data.Descricao,
                    tags = tags
                };
            }
            return Json(dados);
        }
    }
}