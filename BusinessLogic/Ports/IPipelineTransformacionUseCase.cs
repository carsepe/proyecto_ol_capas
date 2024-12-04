using Domain.Dto;

namespace BusinessLogic.Ports
{
    public interface IPipelineTransformacionUseCase
    {
        Task<IEnumerable<ClienteDto>> ProcesarClientesAsync(string rutaArchivo);
        Task<IEnumerable<ProductoDto>> ProcesarProductosAsync(string rutaArchivo);
    }
}
