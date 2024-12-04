using AutoMapper;
using Domain.Dto;
using Domain.Entity;
using Domain.Interface;
using Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class ProductoRepository : IProductoRepository
    {
        private readonly ModelContext _context;
        private readonly IMapper _mapper;

        public ProductoRepository(ModelContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductoDto>> ListarProductosAsync(string? nombre = null, string? codigo = null)
        {
            var query = _context.Productos.AsQueryable();

            if (!string.IsNullOrWhiteSpace(nombre))
            {
                query = query.Where(p => EF.Functions.Like(p.NombreProducto, $"%{nombre}%"));
            }

            if (!string.IsNullOrWhiteSpace(codigo))
            {
                query = query.Where(p => EF.Functions.Like(p.Codigo, $"%{codigo}%"));
            }

            var productos = await query.ToListAsync();
            return _mapper.Map<IEnumerable<ProductoDto>>(productos);
        }

        public async Task<ProductoDto> RegistrarProductoAsync(ProductoDto productoDto)
        {
            var producto = _mapper.Map<Producto>(productoDto);
            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();
            return _mapper.Map<ProductoDto>(producto);
        }

        public async Task<ProductoDto> ActualizarProductoAsync(int id, ProductoDto productoDto)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto == null) throw new KeyNotFoundException("Producto no encontrado.");

            producto.NombreProducto = productoDto.NombreProducto;
            producto.Codigo = productoDto.Codigo;
            producto.FechaVencimiento = productoDto.FechaVencimiento;
            producto.Descripcion = productoDto.Descripcion;
            producto.Stock = productoDto.Stock;
            producto.FechaModificacion = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return _mapper.Map<ProductoDto>(producto);
        }
    }
}
