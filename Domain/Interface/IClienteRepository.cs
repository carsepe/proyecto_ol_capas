using Domain.Dto;

namespace Domain.Interface
{
    public interface IClienteRepository
    {
        Task<IEnumerable<ClienteDto>> ListarClientesAsync(string? nombre = null, string? identificacion = null);
        Task<ClienteDto> RegistrarClienteAsync(ClienteDto cliente);
        Task<ClienteDto> ActualizarClienteAsync(int id, ClienteDto cliente);
    }
}
