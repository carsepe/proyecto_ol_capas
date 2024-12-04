using Domain.Dto;

namespace Domain.Interface
{
    public interface IProductoRepository
    {
        Task<IEnumerable<ProductoDto>> ListarProductosAsync(string? nombre = null, string? codigo = null);
        Task<ProductoDto> RegistrarProductoAsync(ProductoDto producto);
        Task<ProductoDto> ActualizarProductoAsync(int id, ProductoDto producto);
    }
}
