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

        /// <summary>
        /// Metodo para poder hacer los Leftshift es decir cambir el primer bit y pasarlo a la ultmia posicion
        /// </summary>
        /// <param name="Cadena">Cadena que se necesita realizar el leftshift</param>
        /// <returns> retornar la nueva cadena que ya se le hizo el leftshift </returns>
        private string LS_1(string Cadena)
        {
            var Primeros5 = Cadena.Substring(0, 5);
            var Segundos5 = Cadena.Substring(5, 5);

            var Resultado = string.Empty;

            Resultado = (Primeros5.Substring(1, 4) + Primeros5.Substring(0, 1)) + (Segundos5.Substring(1, 4) + Segundos5.Substring(0, 1));

            return Resultado;
             
        }

        /// <summary>
        /// Metodo para poder generar la llaves que se utilizan para el metodo de cifrado y descifrado
        /// </summary>
        /// <param name="LlaveUsuario">La llave que ingresara el usuario para pode generar las otras dos llaves </param>
        /// <param name="P8">La permutacion 8 que se usara para poder hacer los calculos </param>
        /// <param name="P10">La permutacion 10 que se utilizara para poder hacer los calculo</param>
        /// <returns>Retorna la llave 1 y  2 para poder hacer el cifrado y descifrado</returns>
        private (string Llave1 , string Llave2) GeneradorLlaves(string LlaveUsuario, string P8, string P10)
        {

            var Permutacion10 = RealizarPermutaciones(LlaveUsuario, P10);
             
            var LS1 = LS_1(Permutacion10);

            var Permutacion8 = RealizarPermutaciones(LS1, P8);
  
            var Llave1 = Permutacion8;

            var LS2 = LS_1(LS_1(Permutacion10));

            var Llave2 = RealizarPermutaciones(LS2, P8);
 
            return (Llave1, Llave2);
             
        }

        /// <summary>
        /// Metodo para realizar cualquier tipo de permutacion
        /// </summary>
        /// <param name="RealizarPermutacion">La cadena de bits a la que se le quiere realizar una permutacion</param>
        /// <param name="Permutacion">La permutacion que se quiere realizar</param>
        /// <returns>La cadena de bits permutada</returns>
        private string RealizarPermutaciones(string RealizarPermutacion , string Permutacion)
        {
            /// variable para poder poner el resultado de la permutacion
            var ResultadoPermutacion = string.Empty;

            /// permite poder hacer la permutacion, se va sacando cada numero de la permutacion y despues  ese numero sacado se convierte a 
            /// entero para poder sacar el valor de la llave en dicho pasicion que lo indica el numero de la permutacion

            foreach (var TomarCaracteres in Permutacion)
            {
                ResultadoPermutacion += RealizarPermutacion[Convert.ToInt32(TomarCaracteres)];
            }

            return ResultadoPermutacion;
        }

    }
}
