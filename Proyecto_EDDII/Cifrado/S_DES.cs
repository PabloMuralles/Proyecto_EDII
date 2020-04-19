using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proyecto_EDDII.Cifrado
{
    public class S_DES
    { 
        /// <summary>
        /// Me todo para poder calcular la operacion XOR 
        /// </summary>
        /// <param name="Cadena1">Es la primera cadena a la que se quiere comparar</param>
        /// <param name="Cadena2">Es la segunda cadena a la que se quiere comparar </param>
        /// <returns>Retorna el resultado del XOR entre las dos cadenas ingresadas</returns>
        private string XOR(string Cadena1, string Cadena2)
        {
            var Resultado = string.Empty;

            for (int i = 0; i < Cadena1.Length; i++)
            {
                if (Cadena1[i].Equals(Cadena2[i]))
                {
                    Resultado += "0";
                }
                else
                {
                    Resultado += "1";
                }
            }

            return Resultado;
        }




    }
}
