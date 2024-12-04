using Domain.Dto;

namespace BusinessLogic.Ports
{
    public interface IProductoUseCase
    {
        Task<IEnumerable<ProductoDto>> ListarProductosAsync(string? nombre = null, string? codigo = null);
        Task<ProductoDto> RegistrarProductoAsync(ProductoDto producto);
        Task<ProductoDto> ActualizarProductoAsync(int id, ProductoDto producto);
    }
}
