using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proyecto_EDDII
{
    public class Nodo_S_P
    {
        public int ID { get; set; }

        public int padre { get; set; }

        public Nodo_S_P[] hijos { get; set; }
        public Precio_Sucursal[] values { get; set; }
        public Nodo_S_P(int grado, bool posicion)
        {
            if (posicion)
            {
                int valor = ((4 * (grado - 1)) / 3);
                values = new Precio_Sucursal[valor];
                hijos = new Nodo_S_P[grado];
            }
            else
            {
                values = new Precio_Sucursal[grado - 1];
                hijos = new Nodo_S_P[grado];
            }
        }
    }
}
