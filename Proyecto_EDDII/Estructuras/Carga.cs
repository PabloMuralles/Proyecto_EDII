using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

namespace Proyecto_EDDII.Estructuras
{
    public class Carga
    {
        private static Carga _instance = null;
        public static Carga Instance
        {
            get
            {
                if (_instance == null) _instance = new Carga();
                return _instance;
            }
        }
        private Queue<Sucursal> Carga_Datos_Sucursal = new Queue<Sucursal>();
        private Queue<Producto> Carga_Datos_Producto = new Queue<Producto>();
        /// <summary>
        /// Buscar si existe datos anteriores
        /// </summary>
        public void Archivo_Sucursal()
        {           
            var Separacion = "ç0";
            string CarpetaMetadata_S = Environment.CurrentDirectory;
            if (Directory.Exists(Path.Combine(CarpetaMetadata_S, "Metadata_Sucursal")))
            {
                if (File.Exists(Path.Combine(CarpetaMetadata_S, "Metadata_Sucursal", "Sucursal.txt")))
                {
                    FileStream Archivo = new FileStream((Path.Combine(CarpetaMetadata_S, "Metadata_Sucursal", "Sucursal.txt")), FileMode.Open);
                    StreamReader reader = new StreamReader(Archivo);
                    var linea = reader.ReadLine();
                    int contador = 0;
                    while (linea != null)
                    {
                        linea = linea.Trim();
                        if (contador >= 3)
                        {
                            var Antiguo_Datos = new Sucursal();
                            var Datos = linea.Substring(27, linea.Length - 27);
                            var Datos_Separados = Datos.Split(Separacion);
                            for (int i = 0; i < Datos_Separados.Length - 1; i++)
                            {
                                Antiguo_Datos = new Sucursal()
                                {
                                    ID = Convert.ToInt32(Datos_Separados[i]),
                                    Nombre = Datos_Separados[i + 1],
                                    direccion = Datos_Separados[i + 2]
                                };
                                Carga_Datos_Sucursal.Enqueue(Antiguo_Datos);
                                i += 2;
                            }
                        }
                        linea = reader.ReadLine();
                        contador++;
                    }
                    Archivo.Close();
                }                
            }          
                    //Borrar lo que contenga Sucursal                    
                    string PathDebug = Environment.CurrentDirectory;
                    if (File.Exists(Path.Combine(PathDebug, "Metadata_Sucursal", "Sucursal.txt")))
                    {
                        File.Delete(Path.Combine(PathDebug, "Metadata_Sucursal", "Sucursal.txt"));
                    }
                    // Mientras la pila no este vacía recorrerla para insertar en arbol
                    while (Carga_Datos_Sucursal.Count() != 0)
                    {                 
                        Estructuras.Bestrella_Sucursal_.Instance.Insertar(Carga_Datos_Sucursal.Peek().ID, Carga_Datos_Sucursal.Peek().Nombre, Carga_Datos_Sucursal.Peek().direccion);
                        Carga_Datos_Sucursal.Dequeue();
                    }
        }
        public void Archivo_Producto()
        {
            var Separacion = "ç0";
            string CarpetaMetadata_S = Environment.CurrentDirectory;
            if (Directory.Exists(Path.Combine(CarpetaMetadata_S, "Metadata_Sucursal")))
            {
                if (File.Exists(Path.Combine(CarpetaMetadata_S, "Metadata_Sucursal", "Sucursal.txt")))
                {
                    FileStream Archivo = new FileStream((Path.Combine(CarpetaMetadata_S, "Metadata_Sucursal", "Sucursal.txt")), FileMode.Open);
                    StreamReader reader = new StreamReader(Archivo);
                    var linea = reader.ReadLine();
                    int contador = 0;
                    while (linea != null)
                    {
                        linea = linea.Trim();
                        if (contador >= 3)
                        {
                            var Antiguo_Datos = new Producto();
                            var Datos = linea.Substring(27, linea.Length - 27);
                            var Datos_Separados = Datos.Split(Separacion);
                            for (int i = 0; i < Datos_Separados.Length - 1; i++)
                            {
                                Antiguo_Datos = new Producto()
                                {
                                    ID = Convert.ToInt32(Datos_Separados[i]),
                                    Nombre = Datos_Separados[i + 1],
                                    Precio = Datos_Separados[i + 2]
                                };
                                Carga_Datos_Producto.Enqueue(Antiguo_Datos);
                                i += 2;
                            }
                        }
                        linea = reader.ReadLine();
                        contador++;
                    }
                }
            }
        }
    }
}
