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
        private Queue<Precio_Sucursal> Carga_Datos_PS = new Queue<Precio_Sucursal>();
        /// <summary>
        /// Buscar si existe datos anteriores para volver a cargarlos
        /// y enviarlos a volvrer a insertar para generar el arbol
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
                    //Borrar lo que contenga para volver a escribir todo                    
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
            if (Directory.Exists(Path.Combine(CarpetaMetadata_S, "Metadata_Producto")))
            {
                if (File.Exists(Path.Combine(CarpetaMetadata_S, "Metadata_Producto", "Producto.txt")))
                {
                    FileStream Archivo = new FileStream((Path.Combine(CarpetaMetadata_S, "Metadata_Producto", "Producto.txt")), FileMode.Open);
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
                    Archivo.Close();
                }                
            }
            string PathDebug = Environment.CurrentDirectory;
            if (File.Exists(Path.Combine(PathDebug, "Metadata_Producto", "Producto.txt")))
            {
                File.Delete(Path.Combine(PathDebug, "Metadata_Producto", "Producto.txt"));
            }
            while (Carga_Datos_Producto.Count() != 0)
            {
                Estructuras.Bestrella_Produto_.Instance.Insertar(Carga_Datos_Producto.Peek().ID, Carga_Datos_Producto.Peek().Nombre, Carga_Datos_Producto.Peek().Precio);
                Carga_Datos_Producto.Dequeue();
            }
        }
        
        public void Archivo_SP()
        {
            var Separacion = "ç0";
            string CarpetaMetadata_S = Environment.CurrentDirectory;
            if (Directory.Exists(Path.Combine(CarpetaMetadata_S, "Metadata_SP")))
            {
                if (File.Exists(Path.Combine(CarpetaMetadata_S, "Metadata_SP", "Producto-Sucursal.txt")))
                {
                    FileStream Archivo = new FileStream((Path.Combine(CarpetaMetadata_S, "Metadata_SP", "Producto-Sucursal.txt")), FileMode.Open);
                    StreamReader reader = new StreamReader(Archivo);
                    var linea = reader.ReadLine();
                    int contador = 0;
                    while (linea != null)
                    {
                        linea = linea.Trim();
                        if (contador >= 3)
                        {
                            var Antiguo_Datos = new Precio_Sucursal();
                            var Datos = linea.Substring(27, linea.Length - 27);
                            var Datos_Separados = Datos.Split(Separacion);
                            for (int i = 0; i < Datos_Separados.Length - 1; i++)
                            {
                                Antiguo_Datos = new Precio_Sucursal()
                                {
                                    identificador = Convert.ToInt32(Datos_Separados[i]),
                                    ID_S = Convert.ToInt32(Datos_Separados[i + 1]),
                                     ID_P = Convert.ToInt32(Datos_Separados[i+2]),
                                     cantidad = Convert.ToInt32(Datos_Separados[i + 3])
                                };
                                Carga_Datos_PS.Enqueue(Antiguo_Datos);
                                i += 3;
                            }
                        }
                        linea = reader.ReadLine();
                        contador++;
                    }
                    Archivo.Close();
                }
            }
            string PathDebug = Environment.CurrentDirectory;
            if (File.Exists(Path.Combine(PathDebug, "Metadata_SP", "Producto-Sucursal.txt")))
            {
                File.Delete(Path.Combine(PathDebug, "Metadata_SP", "Producto-Sucursal.txt"));
            }
            while (Carga_Datos_PS.Count() != 0)
            {
                Estructuras.Bestrella_Producto_Sucursal_.Instance.Insertar(Carga_Datos_PS.Peek().ID_S,Carga_Datos_PS.Peek().ID_P,Carga_Datos_PS.Peek().cantidad);
                Carga_Datos_PS.Dequeue();
            }
        }
    }
}
