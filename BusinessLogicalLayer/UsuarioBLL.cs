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

namespace BusinessLogicalLayer
{
    public partial class UsuarioBLL
    {
        public BLLResponse<Usuario> Registrar(Usuario item, string senhaRepetida)
        {
            List<ErrorField> erros = ValidarUsuarioParaRegistro(item, senhaRepetida);
            BLLResponse<Usuario> response = new BLLResponse<Usuario>();
            response.Erros = erros;
            if (response.HasErros)
            {
                response.Sucesso = false;
                return response;
            }

            EncriptografarEGuardarSalt(item);

            using (LTContext ctx = new LTContext())
            {
                ctx.Usuarios.Add(item);
                ctx.SaveChanges();
            }
            response.Sucesso = true;
            return response;
        }

        public BLLResponse<Usuario> IsLoginValido(Usuario item)
        {
            BLLResponse<Usuario> response = new BLLResponse<Usuario>();
            response.Erros = new List<ErrorField>();

            using (LTContext ctx = new LTContext())
            {
                Usuario userDoBanco = ctx.Usuarios.FirstOrDefault(u => u.Email == item.Email);
                if (userDoBanco == null)
                {
                    response.Erros.Add(new ErrorField(fieldName: nameof(userDoBanco.Email),
                        message: Utilities.UserOuSenhaInvalidosMessage()));
                    return response;
                }
                response.Sucesso = Criptografia.Verificar(item.Senha, userDoBanco.Salt, userDoBanco.Hash);
                if (response.Sucesso)
                {
                    response.Data = userDoBanco;
                }
            }
            return response;
        }

        private void EncriptografarEGuardarSalt(Usuario item)
        {
            byte[] salt;
            item.Hash = Criptografia.Encriptar(item.Senha, out salt);
            item.Salt = salt;
        }

        public BLLResponse<Usuario> Delete(Usuario item)
        {
            BLLResponse<Usuario> response = new BLLResponse<Usuario>();
            using (LTContext ctx = new LTContext())
            {
                Usuario user = ctx.Usuarios.FirstOrDefault(u => u.ID == item.ID);
                ctx.Usuarios.Remove(user);
                ctx.SaveChanges();
            }
            response.Sucesso = true;
            response.Mensagem = "Deletado com sucesso.";
            return response;
        }

        public BLLResponse<Usuario> AtualizarFotoPerfil(int id, string pathRelativo)
        {
            BLLResponse<Usuario> response = new BLLResponse<Usuario>();
            Usuario user = new Usuario();
            using (LTContext ctx = new LTContext())
            {
                user = ctx.Usuarios.FirstOrDefault(u => u.ID == id);

                if (user != null)
                {
                    response.Sucesso = true;
                    user.PathFotoPerfil = pathRelativo;
                    ctx.SaveChanges();
                }
                return response;
            }
        }

        public BLLResponse<Usuario> AtualizarFotosAlbum(int id, string pathRelativo)
        {
            BLLResponse<Usuario> response = new BLLResponse<Usuario>();
            Usuario user = new Usuario();
            using (LTContext ctx = new LTContext())
            {
                user = ctx.Usuarios.FirstOrDefault(u => u.ID == id);

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

        private List<ErrorField> ValidarUsuarioParaRegistro(Usuario item, string senhaRepetida)
        {
            List<ErrorField> errors = new List<ErrorField>();

            var property = item.GetType().GetProperty(nameof(item.Nome));
            ValidarString(item, property, errors, 3, 20);

            property = item.GetType().GetProperty(nameof(item.Sobrenome));
            ValidarString(item, property, errors, 3, 25);

            ValidarIdade(item, errors);

            ValidarGenero(item, errors);

            ValidarEmail(item, errors);

            ValidarSenha(item, errors, senhaRepetida);

            return errors;
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

        public BLLResponse<Usuario> UpdatePassword(Usuario usuario)
        {
            BLLResponse<Usuario> response = new BLLResponse<Usuario>();

            using (LTContext ctx = new LTContext())
            {
                Usuario userDoDb = ctx.Usuarios.FirstOrDefault(u => u.ID == usuario.ID);
                if (userDoDb != null)
                {
                    userDoDb.Senha = usuario.Senha;
                    EncriptografarEGuardarSalt(userDoDb);
                    ctx.SaveChanges();
                    response.Sucesso = true;
                    response.Mensagem = "Senha atualizada com sucesso!";
                    response.Data = userDoDb;
                    return response;
                }
                response.Mensagem = "Algo de errado ocorreu. Tente novamente";
                return response;
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