using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proyecto_EDDII
{
    public class Nodo
    {
        public int ID { get; set; }

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
        private static Sucursal sucursalBuscada = new Sucursal();
        public Sucursal Busqueda(int ID, int grado)
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
                                if (ID < values[i+1].ID)
                                {
                                    hijos[i].Busqueda(ID, grado);
                                }
                            }
                        }
                    }
                    //String.Compare(nombre, values[i].Nombre) == 0
                    if (ID == values[i].ID)
                    {
                        sucursalBuscada.ID = values[i].ID;
                        sucursalBuscada.Nombre = values[i].Nombre;
                        sucursalBuscada.direccion = values[i].direccion;
                        break;
                    }
                }
            }
            return sucursalBuscada;
        }
        //Metodo para buscar 
        public void Modificar(int ID, string Name, string Adress,int grado)
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
                        values[i] = new Sucursal()
                        {
                            ID = ID,
                            Nombre = Name,
                            direccion = Adress
                        };
                    }
                }
            }            
        }
    }
}
