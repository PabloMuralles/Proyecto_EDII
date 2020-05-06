using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Text;

namespace Proyecto_EDDII.Compresion
{
    public class Descompresion
    {
        /// <summary>
        /// variable gobal para poder almacenar del nuevo archivo a comprimir
        /// </summary>
        private string Nombre = string.Empty;

 
        /// <summary>
        /// Constructuro de la clase
        /// </summary>
        /// <param name="pathArchivo"> Se recibe el achivo subido por medio de postman</param>
        public Descompresion(IFormFile pathArchivo)
        {
            Nombre = Path.GetFileNameWithoutExtension(pathArchivo.FileName);
             
            Lectura(pathArchivo);
             
        }

        /// <summary>
        /// Metodo para poder leer el archivo compreso y al mismo tiempo ir escribiendo el archivo descompreso
        /// </summary>
        /// <param name="Archivo"> Recibe el archivo subido por medio de postman</param>
        private void Lectura(IFormFile Archivo)
        { 
            string Carpetadesscompress = Environment.CurrentDirectory;

            if (!Directory.Exists(Path.Combine(Carpetadesscompress, "DecompressData")))
            {
                Directory.CreateDirectory(Path.Combine(Carpetadesscompress, "DecompressData"));
            }
            using (var reader = new BinaryReader(Archivo.OpenReadStream()))
            {
                using (var streamwitre = new FileStream(Path.Combine(Carpetadesscompress, "DecompressData", $"{Nombre}.txt"), FileMode.OpenOrCreate))
                {
                    using (var write = new BinaryWriter(streamwitre))
                    {
                        var CantidadDiccioanriobytes = reader.ReadBytes(8);

                        var CantidadDiccioanrioNumeros = Convert.ToInt32(Encoding.UTF8.GetString(CantidadDiccioanriobytes));

                        var DiccionarioInicial = new Dictionary<string, int>();

                        for (int i = 0; i < CantidadDiccioanrioNumeros; i++)
                        {
                            var Caracter = reader.ReadBytes(1);
                            DiccionarioInicial.Add(Convert.ToChar(Caracter[0]).ToString(), DiccionarioInicial.Count + 1);
                        }

                        var CantiadaMax = reader.ReadBytes(1);

                        var cantidadMaxBits = Convert.ToInt32(CantiadaMax[0]);

                        var LonguitudArchivo = Convert.ToInt32(reader.BaseStream.Length);

                        var LonguitudLeer = Convert.ToInt32(LonguitudArchivo - reader.BaseStream.Position);

                        var Buffer = new byte[LonguitudArchivo + 10000];

                        var TextoComprimidoBits = new List<int>();

                        Buffer = reader.ReadBytes(LonguitudLeer);

                        string Auxiliar = string.Empty;

                        foreach (var item in Buffer)
                        {
                            Auxiliar = Auxiliar + Convert.ToString(item, 2).PadLeft(8, '0');

                            if (Auxiliar.Length >= cantidadMaxBits)
                            {
                                int CantidadParaescribirCaracter = Auxiliar.Length / cantidadMaxBits;

                                for (int i = 0; i < CantidadParaescribirCaracter; i++)
                                {
                                    TextoComprimidoBits.Add(Convert.ToInt32(Auxiliar.Substring(0, cantidadMaxBits), 2));
                                    Auxiliar = Auxiliar.Substring(cantidadMaxBits);
                                }

                            }
                        }
                        var TextoOriginal = Descomprimir(TextoComprimidoBits.ToArray(), DiccionarioInicial);

                        foreach (var item in TextoOriginal)
                        {
                            write.Write(Convert.ToByte(Convert.ToChar(item)));
                        }

                    }
                }

            }
        }

        /// <summary>
        /// Metodo para poder descomprimir el archivo cifrado
        /// </summary>
        /// <param name="ArchivoComprimido">Arreglo de int donde viene los caracteres cifrados del texto</param>
        /// <param name="dicci">Diccionario inicial que se leyo del documento</param>
        /// <returns>El texto descifrado/returns>
        private string Descomprimir(int[] ArchivoComprimido, Dictionary<string, int> dicci)
        {
            var DiccionarioTemp = new Dictionary<string, int>(dicci);
            var Actual = string.Empty;
            var Anterio = string.Empty;
            var Texto = string.Empty;
            var Contador = 0;
            int Nuevo = 0;

            Nuevo = ArchivoComprimido[Contador];
            Anterio = DiccionarioTemp.FirstOrDefault(x => x.Value == Nuevo).Key;
            Texto += Anterio;
            Contador++;



            while (Contador < ArchivoComprimido.Length)
            {
                Nuevo = ArchivoComprimido[Contador];
                if (Nuevo > DiccionarioTemp.Count)
                {
                    Actual = Anterio + Anterio.Substring(0, 1);
                    DiccionarioTemp.Add(Actual, DiccionarioTemp.Count + 1);
                    Texto += Actual;
                    Contador++;
                    Anterio = Actual;
                }
                else
                {
                    Actual = DiccionarioTemp.FirstOrDefault(x => x.Value == Nuevo).Key;
                    var Caracter = Anterio + Actual.Substring(0, 1);
                    DiccionarioTemp.Add(Caracter, DiccionarioTemp.Count + 1);
                    Texto += Actual;
                    Contador++;
                    Anterio = Actual;
                }
            }
            return Texto;
        }
         
    }
}
