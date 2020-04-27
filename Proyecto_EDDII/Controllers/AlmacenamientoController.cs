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
        [Route("Sucursal/{Key}")]
        public ActionResult Info_Sucursal([FromBody] Sucursal Datos_sucural, string Key)
        {
            if (ModelState.IsValid)
            {
                var Contraseña = Configuracion.Configuracion.Instance.Contaseña;

                var NombreBytes = Encoding.ASCII.GetBytes(Datos_sucural.Nombre);

                var DireccionBytes= Encoding.ASCII.GetBytes(Datos_sucural.direccion);



                Estructuras.Bestrella_Sucursal_.Instance.Insertar(Datos_sucural.ID, Datos_sucural.Nombre, Datos_sucural.direccion);
            }
            return BadRequest(ModelState);
        }
    }
}