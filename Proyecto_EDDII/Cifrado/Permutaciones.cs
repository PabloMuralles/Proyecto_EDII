using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proyecto_EDDII.Cifrado
{
    public class Permutaciones
    {

        private readonly string P10;
        private readonly string P8;
        private readonly string P4;
        private readonly string EP;
        private readonly string IP;
        private readonly string InversaIP;
        
        /// <summary>
        /// Constructor de la clase donde se inicializan las permutaciones y se calcula la permutacion inversa
        /// </summary>
        public Permutaciones()
        {
            P10 = "2416390875";
            P8 = "52637498";
            P4 = "1320";
            EP = "30121230";
            IP = "15203746";

            /// Con este for se calcula la permutacion inversa por medio del i va calculando el indice de los numeros en orden 
            /// para poder escribir la permutacion inversa

            for (int i = 0; i < IP.Length; i++)
            {
                var Numero = IP.IndexOf(i.ToString());
                InversaIP += Convert.ToString(Numero);
            }
        }










    }
}
