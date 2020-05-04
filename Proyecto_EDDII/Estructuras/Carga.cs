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
        private Queue<Sucursal> Carga_Datos = new Queue<Sucursal>();
        /// <summary>
        /// Buscar si existe datos anteriores
        /// </summary>
        public void Archivo()
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
                                Carga_Datos.Enqueue(Antiguo_Datos);
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
