using BusinessLogic.Ports;
using Domain.Dto;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductoController : ControllerBase
    {
        private readonly IProductoUseCase _productoUseCase;
        private readonly ILogger<ProductoController> _logger;

        public ProductoController(IProductoUseCase productoUseCase, ILogger<ProductoController> logger)
        {
            _productoUseCase = productoUseCase;
            _logger = logger;
        }

        [HttpGet("Listar")]
        public async Task<IActionResult> ListarProductos([FromQuery] string? nombre = null, [FromQuery] string? codigo = null)
        {
            try
            {
                var productos = await _productoUseCase.ListarProductosAsync(nombre, codigo);
                return Ok(productos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al listar productos.");
                return StatusCode(500, new { Message = "Ocurrió un error al listar los productos." });
            }
        }

        [HttpPost("Registrar")]
        public async Task<IActionResult> RegistrarProducto([FromBody] ProductoDto producto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var resultado = await _productoUseCase.RegistrarProductoAsync(producto);
                return CreatedAtAction(nameof(RegistrarProducto), new { id = resultado.Codigo }, resultado);
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Validación fallida al registrar producto.");
                return BadRequest(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al registrar producto.");
                return StatusCode(500, new { Message = "Ocurrió un error al registrar el producto." });
            }
        }

        [HttpPut("Actualizar/{id}")]
        public async Task<IActionResult> ActualizarProducto(int id, [FromBody] ProductoDto producto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var resultado = await _productoUseCase.ActualizarProductoAsync(id, producto);
                return Ok(resultado);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Producto no encontrado al intentar actualizar.");
                return NotFound(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar producto.");
                return StatusCode(500, new { Message = "Ocurrió un error al actualizar el producto." });
            }
        }
    }
}
