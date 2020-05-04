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
        private static Precio_Sucursal PSBuscada = new Precio_Sucursal();
        public Precio_Sucursal Busqueda(int identificador, int grado)
        {
            for (int i = 0; i < grado - 1; i++)
            {
                if (values[i] != null)
                {
                    if (identificador < values[i].identificador)
                    {
                        if (hijos[0] != null)
                        {
                            hijos[i].Busqueda(identificador, grado);
                        }
                    }
                    if (identificador > values[i].identificador)
                    {
                        if (hijos[0] != null)
                        {
                            if (values[i + 1] == null)
                            {
                                hijos[i + 1].Busqueda(identificador, grado);
                            }
                            else
                            {
                                if (identificador < values[i + 1].identificador)
                                {
                                    hijos[i].Busqueda(identificador, grado);
                                }
                            }
                        }
                    }                   
                    if (identificador == values[i].identificador)
                    {
                        PSBuscada.identificador = values[i].identificador;
                        PSBuscada.ID_S = values[i].ID_S;
                        PSBuscada.ID_P = values[i].ID_P;
                        PSBuscada.cantidad = values[i].cantidad;
                        break;
                    }
                }
            }
            return PSBuscada;
        }
    }
}
