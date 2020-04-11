using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proyecto_EDDII
{
    public class Nodo
    {
        public int ID {get;set;}
        public int padre { get; set; }
        public Nodo[] hijos { get; set; }
        public Sucursal[] values { get; set; }
        public Nodo(int grado, bool posicion)
        {
          if (posicion)
            {
                int valor = ((4 * (grado - 1)) / 3);
                values = new Sucursal[valor];
                hijos = new Nodo[grado];
            }
            else
            {
                values = new Sucursal[grado - 1];
                hijos = new Nodo[grado];
            }
        }
    }
}
