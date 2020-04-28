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
                //byte[] Name = Encoding.ASCII.GetBytes(Datos_sucural.Nombre);
                // var Name = Convert.ToByte(Datos_sucural.Nombre);
                // byte[] Adress = Encoding.ASCII.GetBytes(Datos_sucural.direccion);
                //var Name = Cifrado.S_DES.Instance.ConvertirTexto(Datos_sucural.Nombre);
                //byte Name_CIPHER = 0;
                //for (int i = 0; i < Name.Length; i++)
                //{
                // Name_CIPHER += Cifrado.S_DES.Instance.Cifrar(Name[i], Convert.ToInt32(Key));                   
                //}
                Estructuras.Bestrella_Sucursal_.Instance.Insertar(Datos_sucural.ID, Datos_sucural.Nombre, Datos_sucural.direccion);
            }
            return BadRequest(ModelState);
        }

        [HttpPost]
        [Route("Producto")]
        public ActionResult Info_Producto([FromBody] Producto Datos_Producto, string path)
        {
            if (ModelState.IsValid)
            {
                if (path != null)
                {
                    Queue<string[]> pila_lectura = Estructuras.Bestrella_Produto_.Instance.Lectura(path);
                    // cifrar el nombre y precio
                    while (pila_lectura.Peek() != null)
                    {
                     Estructuras.Bestrella_Produto_.Instance.Insertar(Convert.ToInt32(pila_lectura.Peek()[0]),pila_lectura.Peek()[1],Convert.ToDecimal(pila_lectura.Peek()[2]));
                        pila_lectura.Dequeue();
                    }
                }
                else
                {
                    Estructuras.Bestrella_Produto_.Instance.Insertar(Datos_Producto.ID,Datos_Producto.Nombre,Datos_Producto.Precio);
                }
            }
            return BadRequest(ModelState);
        }

        [HttpPost]
        [Route("Producto_CSV")]
        public ActionResult Info_Producto_CSV (string path)
        {
            if (ModelState.IsValid)
            {
                if (path != null)
                {
                    Queue<string[]> pila_lectura = Estructuras.Bestrella_Produto_.Instance.Lectura(path);
                    // cifrar el nombre y precio
                    while (pila_lectura.Count() == 0)
                    {
                        Estructuras.Bestrella_Produto_.Instance.Insertar(Convert.ToInt32(pila_lectura.Peek()[0]), pila_lectura.Peek()[1], Convert.ToDecimal(pila_lectura.Peek()[2]));
                        pila_lectura.Dequeue();
                    }                  
                }    
            }
            return BadRequest(ModelState);
        }
    }
}