﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.Enums;

namespace Entity
{
    public class Usuario
    {
        public int ID { get; set; }

        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public DateTime DataNascimento { get; set; }
        public Genero Genero { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public byte[] Hash { get; set; }
        public byte[] Salt { get; set; }


        public string FraseApresentacao { get; set; }
        public string Descricao { get; set; }
        public string Tags { get; set; }
        public string PathFotoPerfil { get; set; }

        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }

        public List<Diretorio> DiretoriosImagens { get; set; }
    }
}
