using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Text;

namespace Proyecto_EDDII.Configuracion
{
    public class Configuracion
    {

        /// <summary>
        /// Metodo para aplicar el singleton con su propiedad
        /// </summary>
        private static Configuracion _instance = null;
        public static Configuracion Instance
        {
            get
            {
                if (_instance == null) _instance = new Configuracion();
                return _instance;
            }
        }

        /// <summary>
        /// Propiedad donde se mantendra la contraseña
        /// </summary>
        private int contraseña;

        /// <summary>
        /// Propiedad para que puedan acceder a la contraseña pero sin que la modifiquen
        /// </summary>
        public int Contaseña { get => contraseña; }

        /// <summary>
        /// Metodo para poder obtener la contraseña del usuario si el usuario no inserta ninguna se generara una aleatoria escribiendola en la carpeta contraseña en un txt con nombre contraseña
        /// </summary>

        public void LecturaContraseña()
        {
            string Ruta = Environment.CurrentDirectory;

            var ContraseñaString = string.Empty;
            if (File.Exists(Path.Combine(Ruta, "Contraseña", "contraseña.txt")) && new FileInfo(Path.Combine(Ruta, "Contraseña", "contraseña.txt")).Length != 0)
            {
                using (var Archivo = new FileStream(Path.Combine(Ruta, "Contraseña", "contraseña.txt"), FileMode.Open))
                {
                    using (var Lectura = new BinaryReader(Archivo))
                    {

                        var buffer = new byte[1];
                        while (Lectura.BaseStream.Position != Lectura.BaseStream.Length)
                        {
                            buffer = Lectura.ReadBytes(1);

                            ContraseñaString += Convert.ToString(Convert.ToChar(buffer[0]));

                        }
                    }

                }

            }
            else
            {
                var Random = new Random();
                if (!File.Exists(Path.Combine(Ruta, "Contraseña", "contraseña.txt")))
                {
                    using (var Archivo = new FileStream(Path.Combine(Ruta, "Contraseña", "contraseña.txt"), FileMode.Create))
                    {
                        using (var Escritura = new BinaryWriter(Archivo))
                        {
                            ContraseñaString = Random.Next(0, 1023).ToString();
                            Escritura.Write(Encoding.UTF8.GetBytes(ContraseñaString));
                            
                        }

                    }

                }
                else if (new FileInfo(Path.Combine(Ruta, "Contraseña", "contraseña.txt")).Length == 0)
                {

                    using (var Archivo = new FileStream(Path.Combine(Ruta, "Contraseña", "contraseña.txt"), FileMode.Open))
                    {
                        using (var Escritura = new BinaryWriter(Archivo))
                        {
                            ContraseñaString = Random.Next(0, 1023).ToString();
                            Escritura.Write(Encoding.UTF8.GetBytes(ContraseñaString));
                        }

                    }
                }
            }

            contraseña = Convert.ToInt32(ContraseñaString);
 
        }



    }
}
