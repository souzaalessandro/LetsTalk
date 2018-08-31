using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLogicalLayer;
using Entity;

namespace LetsTalk.Controllers
{
    public class RegistrarController : Controller
    {
        // GET: Registrar
        [Route("Registrar")]
        public ActionResult Index()
        {
            if (TempData["ViewData"] != null)
            {
                ViewData = (ViewDataDictionary)TempData["ViewData"];
            }
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Registrar(Usuario usuario, string senha, string senhaRepetida)
        {
            if (senha == senhaRepetida)
            {
                //BLLResponse<Usuario> response = new UsuarioBLL().Insert(usuario);

                BLLResponse<Usuario> response = new BLLResponse<Usuario>();
                response.Erros = new List<ErrorField>()
                {
                    new ErrorField("Nome", "Nome inválido"),
                    new ErrorField("Sobrenome", "Sobrenome inválido"),
                    new ErrorField("DataNascimento", "Data de nascimento inválido")
                };

                //Validação
                //Teste
                /*
                int valueOptionQueVeio = (int)usuario.Genero;
                bool valid = false;
                foreach (Entity.Enums.Genero item in (Entity.Enums.Genero[])Enum.GetValues(typeof(Entity.Enums.Genero)))
                {
                    valid = valueOptionQueVeio == (int)item;
                    if (valid)
                    {
                        break;
                    }
                }
                if (!valid)
                {
                    response.Erros.Add(new ErrorField("Genero", "O genero que você mexeu seu merda não existe"));
                }
                */

                foreach (ErrorField item in response.Erros)
                {
                    ModelState.AddModelError(item.FieldName, item.Message);
                }
                if (ModelState.Count == 0)//sucesso
                {
                    return RedirectToAction("ConhecerPessoas", "ConhecerController");//nao existe ainda
                }
                TempData["ViewData"] = ViewData;
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("SenhasDiferentes", "Senhas não batem");
            TempData["ViewData"] = ViewData;
            return RedirectToAction("Index");
        }
    }
}