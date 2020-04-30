using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proyecto_EDDII
{
    public class Nodo_P
    {
        public int ID { get; set; }
        public int padre { get; set; }

        public Nodo_P[] hijos { get; set; }
        public Producto[] values { get; set; }
        public Nodo_P(int grado, bool posicion)
        {
            if (posicion)
            {
                int valor = ((4 * (grado - 1)) / 3);
                values = new Producto[valor];
                hijos = new Nodo_P[grado];
            }
            else
            {
                values = new Producto[grado - 1];
                hijos = new Nodo_P[grado];
            }
        }
    }
}
