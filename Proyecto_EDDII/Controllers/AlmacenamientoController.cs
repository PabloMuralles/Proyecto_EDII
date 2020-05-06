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




    }
}