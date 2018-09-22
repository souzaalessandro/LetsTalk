using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DataAccessObject;
using Entity;
using Entity.Enums;
using Entity.Extensions;
using Entity.ViewModels;
using System.Data.Entity;
using System.IO;

namespace BusinessLogicalLayer
{
    public partial class UsuarioBLL
    {
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

                    ApagarProfilePicAnterior(folder, user.PathFotoPerfil);

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
            pathFotoPerfil= pathFotoPerfil.Replace("/", "\\").Remove(0, 1);
            string pathFotoAtual = Path.Combine(pastaRaiz, pathFotoPerfil);
            System.GC.Collect();
            System.GC.WaitForPendingFinalizers();
            File.Delete(pathFotoAtual);
        }

        public BLLResponse<Usuario> LerPorId(int id)
        {
            BLLResponse<Usuario> response = new BLLResponse<Usuario>();
            Usuario user = new Usuario();
            using (LTContext ctx = new LTContext())
            {
                user = ctx.Usuarios.Include("DiretoriosImagens").FirstOrDefault(u => u.ID == id);
            }
            response.Sucesso = user != null;
            response.Data = user;
            return response;
        }

        public BLLResponse<Usuario> LerTodos()
        {
            BLLResponse<Usuario> response = new BLLResponse<Usuario>();
            using (LTContext ctx = new LTContext())
            {
                response.DataList = ctx.Usuarios.ToList();
            }
            return response;
        }

        public BLLResponse<Usuario> Update(UsuarioViewModel userVM)
        {
            BLLResponse<Usuario> response = new BLLResponse<Usuario>();
            Usuario user = new Usuario();

            using (LTContext ctx = new LTContext())
            {
                user = ctx.Usuarios.FirstOrDefault(u => u.ID == userVM.ID);
                if (user != null)
                {
                    response.Sucesso = true;
                    CopiarInformacoes(userVM, user);
                    ctx.SaveChanges();
                    response.Sucesso = true;
                    response.Mensagem = "Dados atualizados com sucesso!";
                    return response;
                }
                response.Mensagem = "Algo de errado ocorreu";
                return response;
            }
        }

        public BLLResponse<Usuario> UpdatePassword(Usuario usuario, string senhaAntiga)
        {
            BLLResponse<Usuario> response = new BLLResponse<Usuario>();

            using (LTContext ctx = new LTContext())
            {
                Usuario userDoDb = ctx.Usuarios.Find(usuario.ID);
                bool EhSenhaAntiga = Criptografia.Verificar(senhaAntiga, userDoDb.Salt, userDoDb.Hash);

                if (!EhSenhaAntiga)
                {
                    response.Mensagem = "Senha atual incorreta.";
                    return response;
                }
                if (userDoDb == null)
                {
                    response.Mensagem = "Algo de errado ocorreu.";
                    return response;
                }
                else
                {
                    userDoDb.Senha = usuario.Senha;
                    Criptografia.EncriptografarEGuardarSalt(userDoDb);
                    ctx.SaveChanges();

                    response.Sucesso = true;
                    response.Mensagem = "Senha atualizada com sucesso!";
                    response.Data = userDoDb;
                    return response;
                }
            }
        }

        private void CopiarInformacoes(UsuarioViewModel userVM, Usuario user)
        {
            if (!userVM.Frase.IsNullOrWhiteSpace())
            {
                user.FraseApresentacao = userVM.Frase;
            }
            if (!userVM.Descricao.IsNullOrWhiteSpace())
            {
                user.Descricao = userVM.Descricao;
            }
            if (!userVM.Tags.IsNullOrWhiteSpace())
            {
                user.Tags = userVM.Tags;
            }
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