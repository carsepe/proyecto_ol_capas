using BusinessLogic.Ports;
using Domain.Dto;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PipelineDatosController : ControllerBase
    {
        private readonly IPipelineTransformacionUseCase _pipelineTransformacionUseCase;
        private readonly string _baseDirectory;

        public PipelineDatosController(IPipelineTransformacionUseCase pipelineTransformacionUseCase)
        {
            _pipelineTransformacionUseCase = pipelineTransformacionUseCase;
            _baseDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Archivos");

        }

        [HttpGet("descargar-csv-clientes")]
        public IActionResult DescargarClientes()
        {
            var filePath = Path.Combine(_baseDirectory, "clientes.csv");

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound("El archivo clientes.csv no existe.");
            }

            var fileBytes = System.IO.File.ReadAllBytes(filePath);
            return File(fileBytes, "text/csv", "clientes.csv");
        }

        [HttpPost("procesar-clientes")]
        public async Task<IActionResult> ProcesarClientes([FromBody] RutaArchivoDto rutaArchivoDto)
        {
            if (string.IsNullOrWhiteSpace(rutaArchivoDto.RutaArchivo))
            {
                return BadRequest(new
                {
                    Mensaje = "La ruta del archivo no puede estar vacía.",
                    EjemploRuta = "C:\\Archivos\\clientes.csv"
                });
            }

            if (!Path.IsPathRooted(rutaArchivoDto.RutaArchivo) || !rutaArchivoDto.RutaArchivo.EndsWith(".csv"))
            {
                return BadRequest(new
                {
                    Mensaje = "La ruta del archivo debe ser válida y terminar en '.csv'.",
                    EjemploRuta = "C:\\Archivos\\clientes.csv"
                });
            }

            var clientes = await _pipelineTransformacionUseCase.ProcesarClientesAsync(rutaArchivoDto.RutaArchivo);
            return Ok(clientes);
        }

        [HttpGet("descargar-csv-productos")]
        public IActionResult DescargarProductos()
        {
            var filePath = Path.Combine(_baseDirectory, "productos.csv");

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound("El archivo productos.csv no existe.");
            }

            var fileBytes = System.IO.File.ReadAllBytes(filePath);
            return File(fileBytes, "text/csv", "productos.csv");
        }

        [HttpPost("procesar-productos")]
        public async Task<IActionResult> ProcesarProductos([FromBody] RutaArchivoDto rutaArchivoDto)
        {
            if (string.IsNullOrWhiteSpace(rutaArchivoDto.RutaArchivo))
            {
                return BadRequest(new
                {
                    Mensaje = "La ruta del archivo no puede estar vacía.",
                    EjemploRuta = "C:\\Archivos\\productos.csv"
                });
            }

            if (!Path.IsPathRooted(rutaArchivoDto.RutaArchivo) || !rutaArchivoDto.RutaArchivo.EndsWith(".csv"))
            {
                return BadRequest(new
                {
                    Mensaje = "La ruta del archivo debe ser válida y terminar en '.csv'.",
                    EjemploRuta = "C:\\Archivos\\productos.csv"
                });
            }

            var productos = await _pipelineTransformacionUseCase.ProcesarProductosAsync(rutaArchivoDto.RutaArchivo);
            return Ok(productos);
        }
    }
}
