using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Microsoft.AspNetCore.Http;
using System.Text;

namespace Proyecto_EDDII.Compresion
{
    public class Compresion
    { 

        Dictionary<string, int> DiccionarioTotal;

    

        public void EscogerArchivos(string arbol)
        {
            string PathSucursal = Path.Combine(Environment.CurrentDirectory, "Metadata_Sucursal", "Sucursal.txt");
            string PathProducto = Path.Combine(Environment.CurrentDirectory, "Metadata_Producto", "Producto.txt");
            string PathSucursal_Producto = Path.Combine(Environment.CurrentDirectory, "Metadata_SP", "Producto-Sucursal.txt");

            switch (arbol)
            {
                case "sucursal":
                    LecturaArchivo(PathSucursal);
                    break;
                case "producto":
                    LecturaArchivo(PathProducto);
                    break;
                case "sucursal-producto":
                    LecturaArchivo(PathSucursal_Producto);
                    break;
                case "":
                    LecturaArchivo(PathSucursal);
                    LecturaArchivo(PathProducto);
                    LecturaArchivo(PathSucursal_Producto);
                    break;
                    
            }
        }

        public void LecturaArchivo(string pathArchivo)
        {
            
            using (var Archivo = new FileStream(pathArchivo,FileMode.Open))
            {

                using (var reader = new BinaryReader( Archivo))
                {
                    var LonguitudArchivo = Convert.ToInt32(reader.BaseStream.Length);
                    byte[] buffer = new byte[LonguitudArchivo];
                    buffer = reader.ReadBytes(LonguitudArchivo);
                    CompresionArchivo(buffer, Path.GetFileName(pathArchivo));


                }
            }

        }

        public Dictionary<string, int> DiccionarioInicial(byte[] text)
        {
            Dictionary<string, int> DiccionarioInicial = new Dictionary<string, int>();
            foreach (byte Caracter in text)
            {
                var Caracter2 = Convert.ToString(Convert.ToChar(Caracter));

                if (!DiccionarioInicial.ContainsKey(Caracter2))
                {
                    DiccionarioInicial.Add(Caracter2, DiccionarioInicial.Count + 1);
                }

            }
            return DiccionarioInicial;
        }

        public int[] CompressFile(byte[] file, Dictionary<string, int> diccionarioinicialc)
        {
            var DiccionarioTemp = new Dictionary<string, int>(diccionarioinicialc);
            var Contador = 0;
            var CaracteresLeidos = string.Empty;
            var Bytes = new List<int>();
            while (Contador < file.Length)
            {
                CaracteresLeidos += Convert.ToString(Convert.ToChar(file[Contador]));
                Contador++;

                while (DiccionarioTemp.ContainsKey(CaracteresLeidos) && Contador < file.Length)
                {
                    CaracteresLeidos += Convert.ToString(Convert.ToChar(file[Contador]));
                    Contador++;
                }

                if (DiccionarioTemp.ContainsKey(CaracteresLeidos))
                {
                    Bytes.Add((DiccionarioTemp[CaracteresLeidos]));
                }
                else
                {
                    var Llave = CaracteresLeidos.Substring(0, CaracteresLeidos.Length - 1);
                    Bytes.Add((DiccionarioTemp[Llave]));
                    DiccionarioTemp.Add(CaracteresLeidos, DiccionarioTemp.Count + 1);
                    Contador--;
                    CaracteresLeidos = string.Empty;


                }

            }
            DiccionarioTotal = new Dictionary<string, int>(DiccionarioTemp);
            return Bytes.ToArray();



        }

        public void CompresionArchivo(byte[] archivo, string Nombre)
        {
            Dictionary<string, int> Diccionario_Inicial = DiccionarioInicial(archivo);
            int[] ComprimirArchivo = CompressFile(archivo, Diccionario_Inicial);
            Escritura(ComprimirArchivo, Diccionario_Inicial,Nombre);
        }


        public void Escritura(int[] Compresion, Dictionary<string, int> diccionario, string Name)
        {


            string CarpetaCompress = Environment.CurrentDirectory;

            if (!Directory.Exists(Path.Combine(CarpetaCompress, "CompressData")))
            {
                Directory.CreateDirectory(Path.Combine(CarpetaCompress, "CompressData"));
            }

            using (var streamWriter = new FileStream(Path.Combine(CarpetaCompress, "CompressData", $"{Name}.txt"), FileMode.OpenOrCreate))
            {
                using (var write = new BinaryWriter(streamWriter))
                {

                    write.Write(Encoding.UTF8.GetBytes(Convert.ToString(diccionario.Count).PadLeft(8, '0').ToCharArray()));


                    foreach (var item in diccionario)
                    {
                        write.Write(Convert.ToByte(Convert.ToChar(item.Key)));
                    }

                    double CantidadMaxima = Math.Log2(DiccionarioTotal.Count());

                    if (CantidadMaxima % 1 >= 0.5)
                    {
                        CantidadMaxima = Convert.ToInt32(CantidadMaxima);
                    }
                    else
                    {
                        CantidadMaxima = Convert.ToInt32(CantidadMaxima) + 1;
                    }

                    write.Write(Convert.ToByte(CantidadMaxima));

                    var CompresionEnBinario = new List<string>();

                    foreach (var item in Compresion)
                    {
                        CompresionEnBinario.Add(Convert.ToString(item, 2).PadLeft(Convert.ToInt32(CantidadMaxima), '0'));
                    }

                    var EscrituraBitesCompresion = new List<byte>();

                    string Auxiliar = string.Empty;

                    foreach (var item in CompresionEnBinario)
                    {
                        Auxiliar = Auxiliar + item;
                        if (Auxiliar.Length >= 8)
                        {

                            int CantiadadMaximaBits = Auxiliar.Length / 8;

                            for (int i = 0; i < CantiadadMaximaBits; i++)
                            {
                                var sdf = Convert.ToInt32(Auxiliar.Substring(0, 8), 2);
                                var asdf = Convert.ToInt32((Auxiliar.Substring(0, 8)), 2);

                                EscrituraBitesCompresion.Add(Convert.ToByte(Convert.ToInt32(Auxiliar.Substring(0, 8), 2)));
                                Auxiliar = Auxiliar.Substring(8);
                            }

                        }
                    }
                    if (Auxiliar.Length != 0)
                    {

                        EscrituraBitesCompresion.Add(Convert.ToByte(Convert.ToInt32(Auxiliar.PadRight(8, '0'), 2)));
                    }

                    write.Write(EscrituraBitesCompresion.ToArray());
                }


            }
        }
















    }
}
