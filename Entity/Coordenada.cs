using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Coordenada
    {
        public int ID { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }

        public Coordenada()
        {

        }

        public Coordenada(decimal latitude, decimal longitude)
        {
            this.Latitude = latitude;
            this.Longitude = longitude;
        }
    }
}
