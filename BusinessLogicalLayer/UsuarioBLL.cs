﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessObject;
using Entity;
using Entity.Interfaces;

namespace BusinessLogicalLayer
{
    public class UsuarioBLL : IInsertable<Usuario>, IUpdatable<Usuario>, ISearchable<Usuario>, IDeletable<Usuario>
    {
        public BLLResponse<Usuario> Insert(Usuario item)
        {
            //Fazer validações
            item.Nome = FormatarNome(item.Nome);
            item.Sobrenome = FormatarNome(item.Sobrenome);


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

        public string FormatarNome(string nome)
        {
            string lower = nome.ToLower();
            string[] nomes = lower.Split(' ');
            for (int i = 0; i < nomes.Length; i++)
            {
                nomes[i] = nomes[i].Substring(0, 1).ToUpper() + nomes[i].Substring(1);
            }
            return nome;
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
    }
}