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
        private static Producto productoBuscada = new Producto();
        public Producto Busqueda(int ID, int grado)
        {
            for (int i = 0; i < grado - 1; i++)
            {
                if (values[i] != null)
                {
                    //String.Compare(nombre, values[i].Nombre) == -1
                    if (ID < values[i].ID)
                    {
                        if (hijos[0] != null)
                        {
                            hijos[i].Busqueda(ID, grado);
                        }
                    }
                    //String.Compare(nombre, values[i].Nombre) == 1
                    if (ID > values[i].ID)
                    {
                        if (hijos[0] != null)
                        {
                            if (values[i + 1] == null)
                            {
                                hijos[i + 1].Busqueda(ID, grado);
                            }
                            else
                            {
                                //String.Compare(nombre, values[i + 1].Nombre) == -1
                                if (ID < values[i + 1].ID)
                                {
                                    hijos[i].Busqueda(ID, grado);
                                }
                            }
                        }
                    }
                    //String.Compare(nombre, values[i].Nombre) == 0
                    if (ID == values[i].ID)
                    {
                        productoBuscada.ID = values[i].ID;
                        productoBuscada.Nombre = values[i].Nombre;
                        productoBuscada.Precio = values[i].Precio;
                        break;
                    }
                }
            }
            return productoBuscada;
        }
    }
}
