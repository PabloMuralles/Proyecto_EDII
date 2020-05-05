using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;

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
                Estructuras.Carga.Instance.Archivo();
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
                var Contraseña = Configuracion.Configuracion.Instance.Contaseña;
                var NombreCifrado = Cifrado.ManejoInformacion.Instance.CifrarCadena(Datos_Producto.Nombre, Contraseña);
                var PrecioCifrado = Cifrado.ManejoInformacion.Instance.CifrarCadena(Convert.ToString(Datos_Producto.Precio), Contraseña);
                Estructuras.Bestrella_Produto_.Instance.Insertar(Datos_Producto.ID, NombreCifrado, PrecioCifrado);
            }
            return BadRequest(ModelState);
        }

        [HttpPost]
        [Route("Producto_CSV")]
        public ActionResult Info_Producto_CSV(string path)
        {
            if (ModelState.IsValid)
            {
                if (path != null)
                {
                    Queue<string[]> pila_lectura = Estructuras.Bestrella_Produto_.Instance.Lectura(path);
                    // cifrar el nombre y precio
                    var Contraseña = Configuracion.Configuracion.Instance.Contaseña;

                    while (pila_lectura.Count() != 0)
                    {
                        var NombreCifrado = Cifrado.ManejoInformacion.Instance.CifrarCadena(pila_lectura.Peek()[1], Contraseña);
                        var PrecioCifrado = Cifrado.ManejoInformacion.Instance.CifrarCadena(pila_lectura.Peek()[2], Contraseña);
                        Estructuras.Bestrella_Produto_.Instance.Insertar(Convert.ToInt32(pila_lectura.Peek()[0]), NombreCifrado, PrecioCifrado);
                        pila_lectura.Dequeue();
                    }
                }
            }
            return BadRequest(ModelState);
        }
        [HttpPost]
        [Route("Sucursal-Producto")]
        public ActionResult Info_SP([FromBody] Precio_Sucursal Datos_SP)
        {
            if (ModelState.IsValid)
            {

                Estructuras.Bestrella_Producto_Sucursal_.Instance.Verificar(Datos_SP.ID_S, Datos_SP.ID_P, Datos_SP.cantidad);
            }
            return BadRequest(ModelState);
        }

        [HttpPost]
        [Route("Busqueda/{nombre}")]
        public ActionResult Busqueda(string ID, string nombre)
        {
            Sucursal sucursal = null;
            Producto producto = null;
            Precio_Sucursal PS = null;
            if (ModelState.IsValid)
            {
                var Contraseña = Configuracion.Configuracion.Instance.Contaseña;
                switch ($"{nombre}")
                {
                    case "Sucursal":
                        sucursal = Estructuras.Bestrella_Sucursal_.Instance.Busqueda(Convert.ToInt32(ID));
                        sucursal.Nombre = Cifrado.ManejoInformacion.Instance.DescifrarCadena(sucursal.Nombre, Contraseña);
                        sucursal.direccion = Cifrado.ManejoInformacion.Instance.DescifrarCadena(sucursal.direccion, Contraseña);
                        if (sucursal != null)
                            return Ok(sucursal);
                        return NotFound();
                    case "Producto":
                        producto = Estructuras.Bestrella_Produto_.Instance.Busqueda(Convert.ToInt32(ID));
                        producto.Nombre = Cifrado.ManejoInformacion.Instance.DescifrarCadena(producto.Nombre, Contraseña);
                        producto.Precio = Cifrado.ManejoInformacion.Instance.DescifrarCadena(producto.Precio, Contraseña);
                        if (producto != null)
                            return Ok(producto);
                        return NotFound();
                    case "Sucursal-Producto":
                        PS = Estructuras.Bestrella_Producto_Sucursal_.Instance.Busqueda(Convert.ToInt32(ID));
                        if (PS != null)
                            return Ok(PS);
                        return NotFound();
                }
            }
            return BadRequest(ModelState);
        }
        [HttpPost]
        [Route("Modificar/{nombre}")]
        public ActionResult Modificar([FromBody] Modificar modificar  ,string nombre)
        {
            switch (nombre)
            {
                case "Sucursal":
                    var Contraseña = Configuracion.Configuracion.Instance.Contaseña;
                    var NombreCifrado = Cifrado.ManejoInformacion.Instance.CifrarCadena(modificar.Nombre, Contraseña);
                    var DireccioCifrada = Cifrado.ManejoInformacion.Instance.CifrarCadena(modificar.direccion, Contraseña);
                    Estructuras.Bestrella_Sucursal_.Instance.Modificar(modificar.ID,modificar.Nombre,modificar.direccion);
                    break;
                case "Producto":
                    break;
                case "Sucursal-Producto":
                    break;
            }
            return Ok();
        }
    }
}