using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

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
        public int Inserciones = 0;
        static int valor = ((4 * (grado - 1)) / 3);
        List<Nodo> Arbollista = new List<Nodo>();
        public void Insertar(int ID, string Name, string Adress)
        {
            Inserciones++;
            int num = 0;
            int validar_Hijo = 0;
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
                identificador++;
                Arbollista.Add(raiz);
            }
            else
            {
                if (raiz.hijos[validar_Hijo] != null)
                {
                    // Ir a la Izquierda
                    if (ID < raiz.values[identificador - 4].ID)
                    {
                        Insertar_Izquierda(ID,Name,Adress);                       
                    }
                    // Ir a la derecha
                    else
                    {
                        Insertar_derecha(ID, Name, Adress);
                    }
                    /// aumentar para poder entrar 2
                    validar_Hijo = validar_Hijo + 2;
                }
                else
                {
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
                            Arbollista.Add(raiz);
                            break;
                    }
                    num++;
                    if (num == valor) /// full
                    {
                        // crear un auxiliar
                        Sucursal[] Auxiliar_ = Auxiliar(ID, Name, Adress, raiz.values);
                        // dividir el auxiliar
                        int intermedio = Auxiliar_.Length / 2;
                            //Izquierda hasta la mitad
                            ;
                         raiz.hijos[0] = Izquierda(Auxiliar_, intermedio,raiz.ID);
                        //derecha hasta la mtad
                       
                        raiz.hijos[1] = Derecha(Auxiliar_,intermedio,raiz.ID);
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
            Arbollista.Clear();
        }
        public Sucursal[] Auxiliar(int ID, string Name, string Adress, Sucursal[] datos)
        {
            Sucursal[] Aux = new Sucursal[(valor) + 1];
            int entrada = 0;
            foreach (var item in datos)
            {
                Aux[entrada] = item;
                entrada++;
            }
            Aux[entrada] = new Sucursal()
            {
                ID = ID,
                Nombre = Name,
                direccion = Adress
            };
            Aux = Ordenar(Aux);
            return Aux;
        }
        public void Insertar_Izquierda(int ID, string Name, string Adress)
        {
            int num = 0;           
                foreach (var espacio in raiz.hijos[identificador - 4].values)
                {
                    if (espacio == null && num < grado - 1)
                    {
                        raiz.hijos[0].values[num] = new Sucursal()
                        {
                            ID = ID,
                            Nombre = Name,
                            direccion = Adress
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
                            Insertar_derecha(raiz.values[contador - 1].ID, raiz.values[contador - 1].Nombre, raiz.values[contador - 1].direccion);
                            //borrar la ultima posicion de la raiz
                            Array.Clear(raiz.values, contador - 1, contador - 1);
                            //colocar nueva raiz
                            raiz.values[contador - 1] = new Sucursal() { ID = ID, Nombre = Name, direccion = Adress };
                            break;
                        }                                               
                        }
                    }
                }            
        }
        public void Insertar_derecha(int ID, string Name, string Adress)
        {
            int num = 0;
            foreach (var espacio in raiz.hijos[identificador - 3].values)
            {
                if (espacio == null && num < grado - 1)
                {
                    raiz.hijos[identificador - 3].values[num] = new Sucursal()
                    {
                        ID = ID,
                        Nombre = Name,
                        direccion = Adress
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
                                Insertar_derecha(raiz.values[contador - 1].ID, raiz.values[contador - 1].Nombre, raiz.values[contador - 1].direccion);
                                //borrar la ultima posicion de la raiz
                                Array.Clear(raiz.values, contador - 1, contador - 1);
                                //colocar nueva raiz
                                raiz.values[contador - 1] = new Sucursal() { ID = ID, Nombre = Name, direccion = Adress };
                                break;
                            }
                        }
                    }
                    else
                    {
                        Nodo der = new Nodo(grado, entrar);
                        der.ID = identificador;
                        der.padre = raiz.ID;
                        raiz.hijos[identificador - 2] = der;
                        identificador++;
                        //Crear un auxiliar
                        Sucursal[] Aux_ = Auxiliar(ID, Name, Adress, raiz.hijos[identificador - 4].values);
                        //borar derecha
                        Array.Clear(raiz.hijos[identificador - 3].values, 0, grado-1);
                        // subir penultima posicion 
                        int nuevo_num = 0;
                        foreach (var espacio_ in raiz.values)
                        {
                            if (espacio_ == null )
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
        public Nodo Izquierda(Sucursal[] Aux, int intermedio, int ID_padre)
        {          
            //Izquierda
            Nodo izq = new Nodo(grado, entrar);
            izq.ID = identificador;
            izq.padre = ID_padre;
            identificador++;
            for (int i = 0; i < intermedio; i++)
            {
                izq.values[i] = Aux[i];
            }          
            return izq;
        }
        public Nodo Derecha(Sucursal[] Aux, int intermedio, int ID_padre)
        {
            //derecha
            Nodo der = new Nodo(grado, entrar);
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
            lista = lista.OrderBy(x => x.ID).ToList();
            var contador = 0;
            foreach (var item in lista)
            {
                valores[contador] = item;
                contador++;
            }
            return valores;
        }
        public void Recorrido(Nodo raiz)
        {
            if (identificador > 1)
            {
               
            }
        }
       public void Escribir()
        {
            string Identificar_ID = string.Empty;
            string CarpetaMetadata_S = Environment.CurrentDirectory;
            if (!Directory.Exists(Path.Combine(CarpetaMetadata_S, "Metadata_Sucursal")))
            {
                Directory.CreateDirectory(Path.Combine(CarpetaMetadata_S, "Metadata_Sucursal"));
            }
            using (var writeStream = new FileStream(Path.Combine(CarpetaMetadata_S, "Metadata_Sucursal","Sucursal.txt"), FileMode.OpenOrCreate))
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
                            write.Write(valores.ID + "|");                            
                        }
                        write.Write("\n");
                    }

                    write.Close();
                }
            }
        }
    }
}
