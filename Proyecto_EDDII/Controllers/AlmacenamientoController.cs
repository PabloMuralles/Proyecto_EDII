using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Proyecto_EDDII.Controllers
{

    public class AlmacenamientoController : ControllerBase
    {
        [HttpPost]
        [Route("Sucursal")]
        public ActionResult Info_Sucursal([FromBody] Sucursal Datos_sucural, string Key)
        {
            if (ModelState.IsValid)
            {
                var Contraseña = Configuracion.Configuracion.Instance.Contaseña;

                var NombreCifrado = Cifrado.ManejoInformacion.Instance.CifrarCadena(Datos_sucural.Nombre, Contraseña);

                var DireccioCifrada = Cifrado.ManejoInformacion.Instance.CifrarCadena(Datos_sucural.direccion, Contraseña);
  

                Estructuras.Bestrella_Sucursal_.Instance.Insertar(Datos_sucural.ID, NombreCifrado, DireccioCifrada);
            }
            return BadRequest(ModelState);
        }
    }
}