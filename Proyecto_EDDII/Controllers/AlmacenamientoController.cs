using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Proyecto_EDDII.Controllers
{
    
    public class AlmacenamientoController : ControllerBase
    {
        [HttpPost]
        [Route("Sucursal/{nombre}")]
        public ActionResult Info_Sucursal([FromBody] Sucursal Datos_sucural)
        {
            if (ModelState.IsValid)
            {
                Estructuras.Bestrella_Sucursal_.Instance.Insertar(Datos_sucural.ID,Datos_sucural.Nombre,Datos_sucural.direccion);
            }
            return BadRequest(ModelState);
        }
    }
}