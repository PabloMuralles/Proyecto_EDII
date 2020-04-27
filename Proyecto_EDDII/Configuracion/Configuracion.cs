using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

namespace Proyecto_EDDII.Configuracion
{
    public class Configuracion
    {
        private static Configuracion _instance = null;
        public static Configuracion Instance
        {
            get
            {
                if (_instance == null) _instance = new Configuracion();
                return _instance;
            }
        }

        private int contraseña;

        public int Contaseña { get => contraseña; }

        public void LecturaContraseña()
        {
            string Ruta = Environment.CurrentDirectory;

            var ContraseñaString = string.Empty;

            using (var Archivo = new FileStream(Path.Combine(Ruta, "Contraseña", "cotraseña.txt"), FileMode.Open))
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

            contraseña = Convert.ToInt32(ContraseñaString);
 
        }



    }
}
