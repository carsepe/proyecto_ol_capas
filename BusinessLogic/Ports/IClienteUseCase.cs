using Domain.Dto;

namespace BusinessLogic.Ports
{
    public interface IClienteUseCase
    {
        Task<IEnumerable<ClienteDto>> ListarClientesAsync(string? nombre = null, string? identificacion = null);
        Task<ClienteDto> RegistrarClienteAsync(ClienteDto cliente);
        Task<ClienteDto> ActualizarClienteAsync(int id, ClienteDto cliente);
    }
}
