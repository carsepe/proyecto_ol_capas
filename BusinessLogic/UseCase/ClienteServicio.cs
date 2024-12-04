using BusinessLogic.Ports;
using Domain.Dto;
using Domain.Interface;

namespace BusinessLogic.UseCase
{
    public class ClienteServicio : IClienteUseCase
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteServicio(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task<IEnumerable<ClienteDto>> ListarClientesAsync(string? nombre = null, string? identificacion = null)
        {
            return await _clienteRepository.ListarClientesAsync(nombre, identificacion);
        }

        public async Task<ClienteDto> RegistrarClienteAsync(ClienteDto cliente)
        {
            return await _clienteRepository.RegistrarClienteAsync(cliente);
        }

        public async Task<ClienteDto> ActualizarClienteAsync(int id, ClienteDto cliente)
        {
            return await _clienteRepository.ActualizarClienteAsync(id, cliente);
        }
    }
}
