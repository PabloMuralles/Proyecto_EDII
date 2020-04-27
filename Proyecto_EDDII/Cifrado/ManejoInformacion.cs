using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;

namespace Proyecto_EDDII.Cifrado
{
    public class ManejoInformacion
    {
        private static ManejoInformacion _instance = null;
        public static ManejoInformacion Instance
        {
            get
            {
                if (_instance == null) _instance = new ManejoInformacion();
                return _instance;
            }
        }

        /// <summary>
        /// Metetodo para cifrar una cadena con sdes
        /// </summary>
        /// <param name="Cadena">La cadena que se desea cifrar </param>
        /// <param name="Contraseña"> La contraseña para cifrar</param>
        /// <returns>La cadena cifrada</returns>

        public string CifrarCadena(string Cadena, int Contraseña)
        {
            

            var CadenaCifrada = string.Empty;

            foreach (var CaracterCifrar in Cadena)
            {
                var ByteCifrar = Convert.ToByte(Convert.ToChar(CaracterCifrar));
                var ByteCifrado = S_DES.Instance.Cifrar(ByteCifrar, Contraseña);
                CadenaCifrada += Convert.ToString(Convert.ToChar(ByteCifrado));
                 
            }

            return CadenaCifrada;
             
        }

        /// <summary>
        /// Metetodo para descifrar una cadena con sdes
        /// </summary>
        /// <param name="Cadena">La cadena que se desea descifrar </param>
        /// <param name="Contraseña"> La contraseña para descifrar</param>
        /// <returns>La cadena descifrada</returns>
        public string DescifrarCadena(string Cadena, int Contraseña)
        {
             

            var CadenaDescifrada = string.Empty;

            foreach (var Caracter in Cadena)
            {
                var ByteDescifrar = Convert.ToByte(Convert.ToChar(Caracter));
                var ByteCifrado = S_DES.Instance.Descifrar(ByteDescifrar, Contraseña);
                CadenaDescifrada += Convert.ToString(Convert.ToChar(ByteCifrado));

            }

            return CadenaDescifrada;

        }


    }
}
