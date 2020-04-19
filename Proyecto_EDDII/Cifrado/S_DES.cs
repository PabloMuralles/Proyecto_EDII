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

        /// <summary>
        /// Metodo para poder calcular los resultados de las S-Boxes dividiendolo en 4 y 4  
        /// </summary>
        /// <param name="Cadena">Es la cadena que entra para calcular los S-boxes</param>
        /// <returns>Es el resultado calculado de las dos S-Boxes</returns>
        private string S_Boxes(string Cadena)
        {
            var S0 = new string[4, 4]
            {
                { "01" , "00" , "11" , "10"},
                { "11" , "10" , "01" , "00"},
                { "00" , "10" , "01" , "11"},
                { "11" , "01" , "11" , "10"}      
            };
            var S1 = new string[4, 4]
            {
                { "01" , "01" , "10" , "11"},
                { "10" , "00" , "01" , "11"},
                { "11" , "00" , "01" , "00"},
                { "10" , "01" , "00" , "11"}     
            };

            var Primeros4 = Cadena.Substring(0, 4);
            var Segundos4 = Cadena.Substring(4, 4);

            var Resultado = string.Empty;

            var FilaS0 = Convert.ToInt32((Primeros4[0].ToString() + Primeros4[3].ToString()), 2);
            var ColumnasS0 = Convert.ToInt32((Primeros4[0].ToString() + Primeros4[2].ToString()), 2);
            Resultado += S0[FilaS0, ColumnasS0];

            var FilaS1 = Convert.ToInt32((Primeros4[0].ToString() + Primeros4[3].ToString()), 2);
            var ColumnasS1 = Convert.ToInt32((Primeros4[0].ToString() + Primeros4[2].ToString()), 2);
            Resultado += S1[FilaS1, ColumnasS1];

            return Resultado;



        }




    }
}
