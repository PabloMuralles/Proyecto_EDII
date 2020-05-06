using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Microsoft.AspNetCore.Http;
 

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
 
                Estructuras.Carga.Instance.Archivo_Sucursal();
 
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
        [Route("compresion/{arbol}")]
        /// En la ruta debe de ingresar sucursal, producto, sucursal-producto este solo es para uno en especifico 
        public ActionResult CompresionDataCases(string arbol)
        {
            if (ModelState.IsValid)
            {
                Compresion.Compresion NuevoCompresion = new Compresion.Compresion();

                NuevoCompresion.EscogerArchivos(arbol);

                return Ok();
            }
            return BadRequest(ModelState);


        }

        [HttpPost]
        [Route("compresion/")]
        /// En la ruta debe de dejar la vacía para poder comprimir todos los archivos de los arboles
        public ActionResult CompresionDataAll()
        {
            if (ModelState.IsValid)
            {
 
                Compresion.Compresion NuevoCompresion = new Compresion.Compresion();

                NuevoCompresion.EscogerArchivos("");

                return Ok();
 
            }
            return BadRequest(ModelState);
        }

        [HttpPost]
        [Route("descompresion")]
        /// En la ruta debe de ingresar sucursal, producto o sucursal-producto
        public async Task<IActionResult> Descompresion(IFormFile file)
        {
            var filePath = Path.GetTempFileName();
            if (file.Length > 0)
                using (var stream = new FileStream(filePath, FileMode.Create))
                    await file.CopyToAsync(stream);
            var FilePath = Path.GetTempPath();
            Compresion.Descompresion descompresion = new Compresion.Descompresion(file);
            return Ok();

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
        /// <summary>
        /// Metodo de modificar árbol
        /// </summary>
        /// <param name="modificar">Json con los datos que se van a modificar</param>
        /// <param name="nombre">Nombre del arbol que se quiere modificar</param>
        /// <returns></returns>
        [HttpPost]
        [Route("Modificar/{nombre}")]
        public ActionResult Modificar([FromBody] Modificar modificar  ,string nombre)
        {
            var Contraseña = Configuracion.Configuracion.Instance.Contaseña;
            switch (nombre)
            {
                case "Sucursal":
                    var NombreCifrado = Cifrado.ManejoInformacion.Instance.CifrarCadena(modificar.Nombre, Contraseña);
                    var DireccioCifrada = Cifrado.ManejoInformacion.Instance.CifrarCadena(modificar.direccion, Contraseña);
                    Estructuras.Bestrella_Sucursal_.Instance.Modificar(modificar.ID,NombreCifrado,DireccioCifrada);
                    break;
                case "Producto":                   
                    var NombreCifradoProducto = Cifrado.ManejoInformacion.Instance.CifrarCadena(modificar.Nombre, Contraseña);
                    var PrecioCifrado = Cifrado.ManejoInformacion.Instance.CifrarCadena(modificar.Precio,Contraseña);
                    Estructuras.Bestrella_Produto_.Instance.Modificar(modificar.ID,NombreCifradoProducto,PrecioCifrado);
                    break;
                case "Sucursal-Producto":
                    Estructuras.Bestrella_Producto_Sucursal_.Instance.Modificar(modificar.identificador,modificar.cantidad);
                    break;
            }
            return Ok();
        }
    }
}