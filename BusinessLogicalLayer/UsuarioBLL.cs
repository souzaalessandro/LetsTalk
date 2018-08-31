﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessObject;
using Entity;
using Entity.Extensions;
using Entity.Interfaces;

namespace BusinessLogicalLayer
{
    public class UsuarioBLL : IInsertable<Usuario>, IUpdatable<Usuario>, ISearchable<Usuario>, IDeletable<Usuario>
    {
        public BLLResponse<Usuario> Insert(Usuario item)
        {
            //Fazer validações
            item.Nome = Utilities.FormatarNome(item.Nome);
            item.Sobrenome = Utilities.FormatarNome(item.Sobrenome);


            //
            BLLResponse<Usuario> response = new BLLResponse<Usuario>();
            using (LTContext ctx = new LTContext())
            {
                ctx.Usuarios.Add(item);
                ctx.SaveChanges();
            }
            response.Sucesso = true;
            response.Mensagem = "Cadastrado com sucesso.";
            return response;
        }

        public BLLResponse<Usuario> Update(Usuario item)
        {
            //Fazer validações

            BLLResponse<Usuario> response = new BLLResponse<Usuario>();
            Usuario user = new Usuario();
            using (LTContext ctx = new LTContext())
            {
                user = ctx.Usuarios.FirstOrDefault(u => u.ID == item.ID);
                response.Sucesso = user != null;
                user.ClonarDe(item);
                ctx.SaveChanges();
            }
            return response;
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

        public BLLResponse<Usuario> LerPorId(Usuario item)
        {
            BLLResponse<Usuario> response = new BLLResponse<Usuario>();
            Usuario user = new Usuario();
            using (LTContext ctx = new LTContext())
            {
                user = ctx.Usuarios.FirstOrDefault(u => u.ID == item.ID);
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


        public List<ErrorField> ValidarUsuario(Usuario item)
        {
            ///////////////////////////////////////////////////////////////////////////
            List<ErrorField> errors = new List<ErrorField>();

            byte MaxCharsInNome = 20;
            byte MinCharsInNome = 3;

            //Nulo ou espaço em branco
            if (item.Nome.IsNullOrWhiteSpace())
            {
                errors.Add(new ErrorField(nameof(item.Nome),
                    Utilities.MensagemParaCampoNulo(nameof(item.Nome))));
            }
            //Máximo de caracteres
            else if (item.Nome.Length > MaxCharsInNome)
            {
                errors.Add(new ErrorField(nameof(item.Nome),
                    Utilities.MensagemParaMaxChars(nameof(item.Nome), MaxCharsInNome)));
            }
            //Mínimo de caracteres
            else if (item.Nome.Length < MinCharsInNome)
            {
                errors.Add(new ErrorField(nameof(item.Nome),
                    Utilities.MensagemParaMinChars(nameof(item.Nome), MinCharsInNome)));
            }
            ///////////////////////////////////////////////////////////////////////////

            byte MaxCharsInSobrenome = 25;
            byte MinCharsInSobrenome = 3;

            //Nulo ou espaço em branco
            if (item.Sobrenome.IsNullOrWhiteSpace())
            {
                errors.Add(new ErrorField(nameof(item.Sobrenome),
                    Utilities.MensagemParaCampoNulo(nameof(item.Sobrenome))));
            }
            //Máximo de caracteres
            else if (item.Sobrenome.Length > MaxCharsInSobrenome)
            {
                errors.Add(new ErrorField(nameof(item.Sobrenome),
                    Utilities.MensagemParaMaxChars(nameof(item.Sobrenome), MaxCharsInSobrenome)));
            }
            //Mínimo de caracteres
            else if (item.Sobrenome.Length < MinCharsInSobrenome)
            {
                errors.Add(new ErrorField(nameof(item.Sobrenome),
                    Utilities.MensagemParaMinChars(nameof(item.Sobrenome), MinCharsInSobrenome)));
            }
            ///////////////////////////////////////////////////////////////////////////

            // Requisitos de idade mínima e máxima
            byte MinIdade = 18;
            byte MaxIdade = 60;

            bool EhMenorIdade = (DateTime.Now - item.DataNascimento).TotalDays / 365 <= 18;
            
            //Testar a verificação IsNullOrWhiteSpace

            if (item.DataNascimento.ToString().IsNullOrWhiteSpace())
            {
                errors.Add(new ErrorField(nameof(item.Sobrenome),
                    Utilities.MensagemParaCampoNulo(nameof(item.Sobrenome))));
            }
            if (EhMenorIdade)
            {
                errors.Add(new ErrorField(nameof(item.DataNascimento),
                     Utilities.MensagemParaMenor18(nameof(item.DataNascimento), MinIdade)));
            }
            //Lembrete para começar a validação de Idade Maxima (ninguém quer velho no site néh)



            return errors;
        }


    }
}
