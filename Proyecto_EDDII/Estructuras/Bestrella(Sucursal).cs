using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proyecto_EDDII.Estructuras
{
    public class Bestrella_Sucursal_
    {
        private static Bestrella_Sucursal_ _instance = null;
        public static Bestrella_Sucursal_ Instance
        {
            get
            {
                if (_instance == null) _instance = new Bestrella_Sucursal_();
                return _instance;
            }
        }
        Nodo raiz = null;
        public bool entrar = true;
        static int grado = 7;
        int identificador = 1;
        static int valor = ((4 * (grado - 1)) / 3);
        public void Insertar(int ID, string Name, string Adress)
        {
            if (entrar)
            {
                raiz = new Nodo(grado,entrar);
                entrar = false;
                raiz.values[0] = new Sucursal()
                {
                    ID = ID,
                    Nombre = Name,
                    direccion = Adress
                };
                raiz.ID = identificador;
            }
            else
            {
                int num = 0;
                foreach (var espacio in raiz.values)
                {
                    if (espacio == null && num < valor)
                    {
                        raiz.values[num] = new Sucursal()
                        {
                            ID = ID,
                            Nombre = Name,
                            direccion = Adress
                        };
                        break;
                    }
                    num++;
                    if (num == valor) /// full
                    {

                    }
                }
                raiz.values = Ordenar(raiz.values);
            }
        }

        public Sucursal[] Ordenar(Sucursal[] valores)
        {
            var lista = new List<Sucursal>();
            foreach (var iteraciones in valores)
            {
                if (iteraciones != null)
                {
                    lista.Add(iteraciones);
                }
            }
            lista = lista.OrderBy(x => x.Nombre).ToList();
            var contador = 0;
            foreach (var item in lista)
            {
                valores[contador] = item;
                contador++;
            }
            return valores;
        }
    }
}
