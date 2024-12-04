using BusinessLogic.Ports;
using Domain.Dto;
using Domain.Interface;

namespace BusinessLogic.UseCase
{
    public class ProductoServicio : IProductoUseCase
    {
        private readonly IProductoRepository _productoRepository;

        public ProductoServicio(IProductoRepository productoRepository)
        {
            _productoRepository = productoRepository;
        }

        public async Task<IEnumerable<ProductoDto>> ListarProductosAsync(string? nombre = null, string? codigo = null)
        {
            return await _productoRepository.ListarProductosAsync(nombre, codigo);
        }

        public async Task<ProductoDto> RegistrarProductoAsync(ProductoDto producto)
        {
            return await _productoRepository.RegistrarProductoAsync(producto);
        }

        public async Task<ProductoDto> ActualizarProductoAsync(int id, ProductoDto producto)
        {
            return await _productoRepository.ActualizarProductoAsync(id, producto);
        }
    }
}
