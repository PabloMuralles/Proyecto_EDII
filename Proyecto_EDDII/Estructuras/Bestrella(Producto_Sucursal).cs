using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

namespace Proyecto_EDDII.Estructuras
{
    public class Bestrella_Producto_Sucursal_
    {
        private static Bestrella_Producto_Sucursal_ _instance = null;
        public static Bestrella_Producto_Sucursal_ Instance
        {
            get
            {
                if (_instance == null) _instance = new Bestrella_Producto_Sucursal_();
                return _instance;
            }
        }
        Nodo_S_P raiz = null;
        public bool entrar = true;
        static int grado = 7;
        int identificador = 1;
        public int Inserciones = 0;
        static int valor = ((4 * (grado - 1)) / 3);
        List<Nodo_S_P> Arbollista = new List<Nodo_S_P>();
        public List<int> Sucursale = new List<int>();
        public List<int> Productos = new List<int>();
        public void Verificar(int ID_S, int ID_P, int Cantidad)
        {
            if (Sucursale.Contains(ID_S)&& Productos.Contains(ID_P))
            {
                Insertar(ID_S,ID_P,Cantidad);
            }
            else
            {
               // Mostrar un Error porque no existe los ID
            }
        }
        public void Insertar(int ID_S,int ID_P,int Cantidad)
        {
            Inserciones++;
            int num = 0;
            int validar_Hijo = 0;
            if (entrar)
            {
                raiz = new Nodo_S_P(grado, entrar);
                entrar = false;
                raiz.values[0] = new Precio_Sucursal()
                {
                    identificador = ID_S + ID_P,
                    ID_S = ID_S,
                    ID_P = ID_P,
                    cantidad = Cantidad
                };
                raiz.ID = identificador;
                identificador++;
                Arbollista.Add(raiz);
            }
            else
            {
                if (raiz.hijos[validar_Hijo] != null)
                {
                    // Ir a la Izquierda
                    if (identificador < raiz.values[identificador - 4].identificador)
                    {
                        Insertar_Izquierda(ID_S, ID_P, Cantidad);
                    }
                    // Ir a la derecha
                    else
                    {
                        Insertar_derecha(ID_S, ID_P, Cantidad);
                    }
                    // aumentar para poder entrar 2
                    validar_Hijo = validar_Hijo + 2;
                }
                else
                {
                    foreach (var espacio in raiz.values)
                    {
                        if (espacio == null && num < valor)
                        {

                            raiz.values[num] = new Precio_Sucursal()
                            {
                                identificador = ID_S + ID_P,
                                ID_S = ID_S,
                                ID_P = ID_P,
                                cantidad = Cantidad
                            };
                            Arbollista.Add(raiz);
                            break;
                        }
                        num++;
                        if (num == valor) /// full
                        {
                            // crear un auxiliar
                            Precio_Sucursal[] Auxiliar_ = Auxiliar(ID_S, ID_P, Cantidad, raiz.values);
                            // dividir el auxiliar
                            int intermedio = Auxiliar_.Length / 2;
                            //Izquierda hasta la mitad                            
                            raiz.hijos[0] = Izquierda(Auxiliar_, intermedio, raiz.ID);
                            //derecha hasta la mtad
                            raiz.hijos[1] = Derecha(Auxiliar_, intermedio, raiz.ID);
                            // vaciar raiz
                            Array.Clear(raiz.values, 0, raiz.values.Length);
                            //Asignar nuevo dato a raiz
                            raiz.values[0] = Auxiliar_[intermedio];
                            Arbollista.Add(raiz);
                            //componer esta parte
                            Arbollista.Add(raiz.hijos[0]);
                            Arbollista.Add(raiz.hijos[1]);
                        }
                    }
                }
                raiz.values = Ordenar(raiz.values);
            }
            Escribir();
            // limpiar la lista para que no se repita
            Arbollista.Clear();
        }
        public Precio_Sucursal[] Auxiliar( int ID_S, int ID_P, int Cantida, Precio_Sucursal[] datos)
        {
            Precio_Sucursal[] Aux = new Precio_Sucursal[(valor) + 1];
            int entrada = 0;
            foreach (var item in datos)
            {
                Aux[entrada] = item;
                entrada++;
            }
            Aux[entrada] = new Precio_Sucursal()
            {
                identificador = ID_S + ID_P,
                ID_S = ID_S,
                ID_P = ID_P,
                cantidad = Cantida
            };
            Aux = Ordenar(Aux);
            return Aux;
        }
        public void Insertar_Izquierda(int ID_S, int ID_P, int Cantida)
        {
            int num = 0;
            foreach (var espacio in raiz.hijos[identificador - 4].values)
            {
                if (espacio == null && num < grado - 1)
                {
                    raiz.hijos[0].values[num] = new Precio_Sucursal()
                    {
                        identificador = ID_S + ID_P,
                        ID_S = ID_S,
                        ID_P = ID_P,
                        cantidad = Cantida
                    };
                    raiz.hijos[identificador - 4].values = Ordenar(raiz.hijos[identificador - 4].values);
                    /// Ingresar nueva lista
                    Arbollista.Add(raiz);
                    Arbollista.Add(raiz.hijos[identificador - 4]);
                    Arbollista.Add(raiz.hijos[identificador - 3]);
                    break;
                }
                num++;
                // esta lleno y revisar hermano derecho
                if (num == grado - 1)
                {
                    foreach (var disponibilidad in raiz.hijos[identificador - 3].values)
                    {
                        if (disponibilidad == null)
                        {
                            //encontrar ultima posicion de la raiz
                            int contador = 0;
                            while (raiz.values[contador] != null)
                            {
                                contador++;
                            }
                            // bajar dato a hijo derecho
                            Insertar_derecha(raiz.values[contador - 1].ID_S, raiz.values[contador - 1].ID_P,raiz.values[contador - 1].cantidad);
                            //borrar la ultima posicion de la raiz
                            Array.Clear(raiz.values, contador - 1, contador - 1);
                            //colocar nueva raiz
                            raiz.values[contador - 1] = new Precio_Sucursal() {identificador = ID_S + ID_P, ID_S = ID_S, ID_P = ID_P, cantidad = Cantida };
                            break;
                        }
                    }
                }
            }
        }
        public void Insertar_derecha(int ID_S, int ID_P, int Cantida)
        {
            int num = 0;
            foreach (var espacio in raiz.hijos[identificador - 3].values)
            {
                if (espacio == null && num < grado - 1)
                {
                    raiz.hijos[identificador - 3].values[num] = new Precio_Sucursal()
                    {
                        identificador = ID_S + ID_P,
                        ID_S = ID_S,
                        ID_P = ID_P,
                        cantidad = Cantida
                    };
                    raiz.hijos[identificador - 3].values = Ordenar(raiz.hijos[identificador - 3].values);
                    /// Ingresar nueva lista
                    Arbollista.Add(raiz);
                    Arbollista.Add(raiz.hijos[identificador - 4]);
                    Arbollista.Add(raiz.hijos[identificador - 3]);
                    break;
                }
                num++;
                // esta lleno
                if (num == grado - 1)
                {
                    if (raiz.hijos[identificador - 2] != null)
                    {
                        foreach (var disponibilidad in raiz.hijos[identificador - 2].values)
                        {
                            if (disponibilidad == null)
                            {
                                //encontrar ultima posicion de la raiz
                                int contador = 0;
                                while (raiz.values[contador] != null)
                                {
                                    contador++;
                                }
                                // bajar dato a hijo derecho
                                Insertar_derecha(raiz.values[contador - 1].ID_S, raiz.values[contador - 1].ID_P, raiz.values[contador - 1].cantidad);
                                //borrar la ultima posicion de la raiz
                                Array.Clear(raiz.values, contador - 1, contador - 1);
                                //colocar nueva raiz
                                raiz.values[contador - 1] = new Precio_Sucursal() { identificador = ID_S + ID_P, ID_S = ID_S, ID_P = ID_P, cantidad = Cantida };
                                break;
                            }
                        }
                    }
                    else
                    {
                        Nodo_S_P der = new Nodo_S_P(grado, entrar);
                        der.ID = identificador;
                        der.padre = raiz.ID;
                        raiz.hijos[identificador - 2] = der;
                        identificador++;
                        //Crear un auxiliar
                        Precio_Sucursal[] Aux_ = Auxiliar(ID_S, ID_P, Cantida, raiz.hijos[identificador - 4].values);
                        //borar derecha
                        Array.Clear(raiz.hijos[identificador - 3].values, 0, grado - 1);
                        // subir penultima posicion 
                        int nuevo_num = 0;
                        foreach (var espacio_ in raiz.values)
                        {
                            if (espacio_ == null)
                            {
                                raiz.values[nuevo_num] = Aux_[5];
                                break;
                            }
                            nuevo_num++;

                        }
                        for (int i = 0; i < 5; i++)
                        {
                            raiz.hijos[identificador - 4].values[i] = Aux_[i];
                        }
                        // colocar ultimo dato
                        der.values[0] = Aux_[6];
                    }

                }
            }
        }
        public Nodo_S_P Izquierda(Precio_Sucursal[] Aux, int intermedio, int ID_padre)
        {
            //Izquierda
            Nodo_S_P izq = new Nodo_S_P(grado, entrar);
            izq.ID = identificador;
            izq.padre = ID_padre;
            identificador++;
            for (int i = 0; i < intermedio; i++)
            {
                izq.values[i] = Aux[i];
            }
            return izq;
        }
        public Nodo_S_P Derecha(Precio_Sucursal[] Aux, int intermedio, int ID_padre)
        {
            //derecha
            Nodo_S_P der = new Nodo_S_P(grado, entrar);
            der.ID = identificador;
            der.padre = ID_padre;
            identificador++;
            int num = 0;
            for (int i = intermedio + 1; i < Aux.Length; i++)
            {
                der.values[num] = Aux[i];
                num++;
            }
            return der;
        }
        public Precio_Sucursal[] Ordenar(Precio_Sucursal[] valores)
        {
            var lista = new List<Precio_Sucursal>();
            foreach (var iteraciones in valores)
            {
                if (iteraciones != null)
                {
                    lista.Add(iteraciones);
                }
            }
            lista = lista.OrderBy(x => x.identificador).ToList();
            var contador = 0;
            foreach (var item in lista)
            {
                valores[contador] = item;
                contador++;
            }
            return valores;
        }
        public Precio_Sucursal Busqueda(int identificador)
        {
            Precio_Sucursal PS = raiz.Busqueda(identificador, grado);
            return PS;
        }
        public void Modificar(int identificador, int cantidad)
        {
            raiz.Modificar(identificador,cantidad, grado);
            Escribir();
        }
        public void Escribir()
        {
            string Identificar_ID = string.Empty;
            string CarpetaMetadata_PS = Environment.CurrentDirectory;
            if (!Directory.Exists(Path.Combine(CarpetaMetadata_PS, "Metadata_SP")))
            {
                Directory.CreateDirectory(Path.Combine(CarpetaMetadata_PS, "Metadata_SP"));
            }
            using (var writeStream = new FileStream(Path.Combine(CarpetaMetadata_PS, "Metadata_SP", "Producto-Sucursal.txt"), FileMode.OpenOrCreate))
            {
                using (var write = new StreamWriter(writeStream))
                {
                    write.WriteLine("Grado " + grado);
                    write.WriteLine("Raiz " + raiz.ID);
                    write.WriteLine("Proxima posición Disponible: " + identificador);

                    foreach (var NodoLista in Arbollista)
                    {
                        if (NodoLista.padre == 0)
                        {
                            write.Write(NodoLista.ID + "|0|");
                        }
                        else
                        {
                            write.Write(NodoLista.ID + "|" + NodoLista.padre + "|");

                        }
                        if (NodoLista.hijos[0] == null)
                        {
                            string hijos = string.Empty;
                            for (int i = 0; i < grado; i++)
                            {
                                hijos += "0|";

                            }
                            write.Write(hijos);

                        }
                        else
                        {
                            foreach (var nodosHijos in NodoLista.hijos)
                            {
                                if (nodosHijos != null)
                                {
                                    write.Write(nodosHijos.ID + "|");
                                }
                                else
                                {
                                    write.Write("0|");
                                }


                            }
                        }

                        foreach (var valores in NodoLista.values)
                        {
                            if (valores == null)
                            {
                                break;
                            }
                            write.Write(valores.identificador + "|");
                            write.Write(valores.ID_P + "|");
                            write.Write(valores.ID_S + "|");
                            write.Write(valores.cantidad + "|");
                        }
                        write.Write("\n");
                    }

                    write.Close();
                }
            }
        }
    }
}
