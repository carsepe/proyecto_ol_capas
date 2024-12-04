using BusinessLogic.Ports;
using Domain.Dto;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteUseCase _clienteUseCase;
        private readonly ILogger<ClienteController> _logger;

        public ClienteController(IClienteUseCase clienteUseCase, ILogger<ClienteController> logger)
        {
            _clienteUseCase = clienteUseCase;
            _logger = logger;
        }

        [HttpGet("Listar")]
        public async Task<IActionResult> ListarClientes([FromQuery] string? nombre = null, [FromQuery] string? identificacion = null)
        {
            try
            {
                var clientes = await _clienteUseCase.ListarClientesAsync(nombre, identificacion);
                return Ok(clientes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al listar clientes.");
                return StatusCode(500, new { Message = "Ocurrió un error al listar los clientes." });
            }
        }

        [HttpPost("Registrar")]
        public async Task<IActionResult> RegistrarCliente([FromBody] ClienteDto cliente)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var resultado = await _clienteUseCase.RegistrarClienteAsync(cliente);
                return CreatedAtAction(nameof(RegistrarCliente), new { id = resultado.Identificacion }, resultado);
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Validación fallida al registrar cliente.");
                return BadRequest(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al registrar cliente.");
                return StatusCode(500, new { Message = "Ocurrió un error al registrar el cliente." });
            }
        }

        [HttpPut("Actualizar/{id}")]
        public async Task<IActionResult> ActualizarCliente(int id, [FromBody] ClienteDto cliente)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var resultado = await _clienteUseCase.ActualizarClienteAsync(id, cliente);
                return Ok(resultado);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Cliente no encontrado al intentar actualizar.");
                return NotFound(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar cliente.");
                return StatusCode(500, new { Message = "Ocurrió un error al actualizar el cliente." });
            }
        }
    }
}
