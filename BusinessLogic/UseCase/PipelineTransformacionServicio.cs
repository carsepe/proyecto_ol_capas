using BusinessLogic.Ports;
using Domain.Dto;
using Domain.Interface;

namespace BusinessLogic.UseCase
{
    public class PipelineTransformacionServicio : IPipelineTransformacionUseCase
    {
        private readonly IPipelineTransformacionDatosRepository _pipelineTransformacionDatosRepository;

        public PipelineTransformacionServicio(IPipelineTransformacionDatosRepository pipelineTransformacionDatosRepository)
        {
            _pipelineTransformacionDatosRepository = pipelineTransformacionDatosRepository;
        }

        public async Task<IEnumerable<ClienteDto>> ProcesarClientesAsync(string rutaArchivo)
        {
            return await _pipelineTransformacionDatosRepository.ProcesarClientesAsync(rutaArchivo);
        }

        public async Task<IEnumerable<ProductoDto>> ProcesarProductosAsync(string rutaArchivo)
        {
            return await _pipelineTransformacionDatosRepository.ProcesarProductosAsync(rutaArchivo);
        }
    }
}
