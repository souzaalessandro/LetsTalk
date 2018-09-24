using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessObject;
using Entity;
using Entity.Extensions;
using Entity.ViewModels;

namespace BusinessLogicalLayer
{
    public class FotosUsuarioBLL
    {
        private const string PATH_IMAGEM_PADRAO = "/UserImages/DefaultCropped.png";
        //fazer trocar a foto direito
        public BLLResponse<Usuario> AtualizarFotosAlbum(int id, string pathRelativo)
        {
            BLLResponse<Usuario> response = new BLLResponse<Usuario>();
            Usuario user = new Usuario();
            using (LTContext ctx = new LTContext())
            {
                user = ctx.Usuarios.Find(id);

                if (user != null)
                {
                    response.Sucesso = true;
                    Diretorio diretorio = new Diretorio();
                    diretorio.PathRelativo = pathRelativo;
                    diretorio.Usuario = user;
                    List<Diretorio> diretorios = new List<Diretorio>();
                    diretorios.Add(diretorio);
                    user.DiretoriosImagens = diretorios;
                    ctx.SaveChanges();
                }
                return response;
            }
        }

        public BLLResponse<Usuario> UpdateProfilePic(int id, string folder, byte[] imagem, string pathRelativo)
        {
            BLLResponse<Usuario> response = new BLLResponse<Usuario>();

            using (LTContext ctx = new LTContext())
            {
                Usuario user = ctx.Usuarios.Find(id);

                if (user != null)
                {
                    response.Sucesso = true;

                    string nomeFoto = Guid.NewGuid() + ".png";
                    string path = Path.Combine(folder, nomeFoto);
                    FileStream stream = new FileStream(path, FileMode.Create);
                    stream.Write(imagem, 0, imagem.Length);
                    stream.Flush();

                    if (user.PathFotoPerfil != PATH_IMAGEM_PADRAO)
                    {
                        ApagarProfilePicAnterior(folder, user.PathFotoPerfil);
                    }

                    user.PathFotoPerfil = Path.Combine(pathRelativo, nomeFoto);
                    ctx.SaveChanges();

                    response.Data = user;
                }
            }
            return response;
        }

        private void ApagarProfilePicAnterior(string folder, string pathFotoPerfil)
        {
            if (pathFotoPerfil.IsNullOrWhiteSpace())
            {
                return;
            }
            int index = folder.IndexOf("\\UserImages");
            string pastaRaiz = folder.Remove(index);
            pathFotoPerfil = pathFotoPerfil.Replace("/", "\\").Remove(0, 1);
            string pathFotoAtual = Path.Combine(pastaRaiz, pathFotoPerfil);
            GC.Collect();
            GC.WaitForPendingFinalizers();
            File.Delete(pathFotoAtual);
        }

        public void BuscarDiretorios(UsuarioViewModel usuarioVM)
        {
            BLLResponse<Usuario> response = new BLLResponse<Usuario>();

            using (LTContext ctx = new LTContext())
            {
                List<Diretorio> diretorios = ctx.Diretorios.ToList();
            }
        }
    }
}
