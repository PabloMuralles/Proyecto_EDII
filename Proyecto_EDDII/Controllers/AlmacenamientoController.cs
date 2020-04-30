﻿using System;
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

        [HttpPost]
        [Route("Producto")]
        public ActionResult Info_Producto([FromBody] Producto Datos_Producto, string path)
        {
            if (ModelState.IsValid)
            {
             //cifrar Nombre y precio
             Estructuras.Bestrella_Produto_.Instance.Insertar(Datos_Producto.ID,Datos_Producto.Nombre,Datos_Producto.Precio);
                
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
        [HttpPost]
        [Route("Sucursal-Producto")]
        public ActionResult Info_SP([FromBody] Producto Datos_Producto, string path)
        {
            if (ModelState.IsValid)
            {
                //cifrar Nombre y precio
               // Estructuras.Bestrella_Producto_Sucursal_.Instance.Insertar(Datos_Producto.ID, Datos_Producto.Nombre, Datos_Producto.Precio);

            }
            return BadRequest(ModelState);
        }
    }
}