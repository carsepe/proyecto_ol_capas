using AutoMapper;
using Domain.Dto;
using Domain.Entity;
using Domain.Interface;
using Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly ModelContext _context;
        private readonly IMapper _mapper;

        public ClienteRepository(ModelContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ClienteDto>> ListarClientesAsync(string? nombre = null, string? identificacion = null)
        {
            var query = _context.Clientes.AsQueryable();

            if (!string.IsNullOrWhiteSpace(nombre))
            {
                query = query.Where(c => EF.Functions.Like(c.Nombre, $"%{nombre}%"));
            }

            if (!string.IsNullOrWhiteSpace(identificacion))
            {
                query = query.Where(c => EF.Functions.Like(c.Identificacion, $"%{identificacion}%"));
            }

            var clientes = await query.ToListAsync();
            return _mapper.Map<IEnumerable<ClienteDto>>(clientes);
        }

        public async Task<ClienteDto> RegistrarClienteAsync(ClienteDto clienteDto)
        {
            var cliente = _mapper.Map<Cliente>(clienteDto);
            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();
            return _mapper.Map<ClienteDto>(cliente);
        }

        public async Task<ClienteDto> ActualizarClienteAsync(int id, ClienteDto clienteDto)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null) throw new KeyNotFoundException("Cliente no encontrado.");

            cliente.Nombre = clienteDto.Nombre;
            cliente.Identificacion = clienteDto.Identificacion;
            cliente.TipoIdentificacion = clienteDto.TipoIdentificacion;
            cliente.Direccion = clienteDto.Direccion;
            cliente.Telefono = clienteDto.Telefono;
            cliente.FechaNacimiento = clienteDto.FechaNacimiento;
            cliente.FechaModificacion = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return _mapper.Map<ClienteDto>(cliente);
        }
    }
}
