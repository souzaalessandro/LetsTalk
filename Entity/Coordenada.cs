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
        public double Latitude { get; set; }
        public double Longitude { get; set; }


        public Coordenada(double latitude, double longitude)
        {
            this.Latitude = latitude;
            this.Longitude = longitude;
        }
    }
}
