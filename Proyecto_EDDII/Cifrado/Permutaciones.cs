using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proyecto_EDDII.Cifrado
{
    public class Permutaciones
    {
        /// <summary>
        /// Varibalbes globales de permutaciones tipo solo lectura para que ningun metodo las pueda cambiar
        /// </summary>
        private readonly string P10;
        private readonly string P8;
        private readonly string P4;
        private readonly string EP;
        private readonly string IP;
        private readonly string InversaIP;
        
        /// <summary>
        /// Propiedades para que se pueda acesar a las permutaciones desde otras clases pero solo con el get para que no pudan cambiarlas
        /// </summary>
        public string P10_ { get;}
        public string P8_ { get;}
        public string P4_ { get;}
        public string EP_ { get;}
        public string IP_ { get;}
        public string InversaIP_ { get;}

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

        /// <summary>
        /// Verifica si la permutacion 8 esta bien escrita 
        /// </summary>
        /// <returns>Retorna un bool, si es corracta true y si esta mala false</returns>
        private bool VerificarPermutacion8()
        {
            /// Cuenta cuantos caracteres son diferentes en la permutacion
            var CantidadNumeroDiferentes = P8.Distinct().Count();
            if (P8.Length != 8 || CantidadNumeroDiferentes != 8)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Verfica que la permutacion 10 este corractemente escrita
        /// </summary>
        /// <returns>Retorna un bool, si es corracta true y si esta mala false</returns>
        private bool VerificarPermutacion10()
        {
            /// Cuenta cuantos caracteres son diferentes en la permutacion
            var CantidadNumeroDiferentes = P10.Distinct().Count();
            if (P10.Length != 10 || CantidadNumeroDiferentes != 10)
            {
                return false;
            }
            return true;

        }

        /// <summary>
        /// Verfica que la permutacion 4  este corractemente escrita
        /// </summary>
        /// <returns>Retorna un bool, si es corracta true y si esta mala false</returns>
        private bool VerificacionPermutacion4()
        {
            /// Cuenta cuantos caracteres son diferentes en la permutacion
            var CantidadNumeroDiferentes = P4.Distinct().Count();
            if (P4.Length != 4 || CantidadNumeroDiferentes != 4)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Verfica que la permutacion este corractemente escrita
        /// Si la permutacion esta bien escrita la inversa lo va estar por eso no es necesario implementar ese metodo de validacion
        /// </summary>
        /// <returns>Retorna un bool, si es corracta true y si esta mala false</returns>
        private bool VerificarPermutacion()
        {
            /// Cuenta cuantos caracteres son diferentes en la permutacion
            var CantidadNumeroDiferentes = IP.Distinct().Count();
            if (IP.Length != 8 || CantidadNumeroDiferentes != 8)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Verfica que el expandir y permutar este correcto en este caso como se divide en 4 dentro se ese grupo no se pueden repetir 
        /// pero en el grupo en totarl si pueden haber repetido 
        /// </summary>
        /// <returns>Retorna un bool, si es corracta true y si esta mala false</returns>
        private bool VerificarExpandirPermutar()
        {
            var Primeros4 = EP.Substring(0, 4);
            var Segundos4 = EP.Substring(4, 4);
            if (EP.Length == 8)
            {
                /// Cuenta cuantos caracteres son diferentes en la permutacion
                var CantidadNumeroDiferentes = Primeros4.Distinct().Count();
                if (Primeros4.Length != 4 || CantidadNumeroDiferentes != 4)
                {
                    return false;
                }
                else
                {
                    /// Cuenta cuantos caracteres son diferentes en la permutacion
                    var CantidadNumeroDiferentes2 = Segundos4.Distinct().Count();
                    if (Segundos4.Length != 4 || CantidadNumeroDiferentes2 != 4)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }

            }
            else
            {
                return false;
            }
        }

        public bool VerificarTodasPermutaciones()
        {
            var Resultado = VerificacionPermutacion4() && VerificarPermutacion10() && VerificarPermutacion8() && VerificarPermutacion() &&
                VerificarExpandirPermutar();

            return Resultado;
        }
 
    }
}
