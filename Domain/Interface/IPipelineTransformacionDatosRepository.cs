using Domain.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interface
{
    public interface IPipelineTransformacionDatosRepository
    {
        Task<IEnumerable<ClienteDto>> ProcesarClientesAsync(string rutaArchivo);
        Task<IEnumerable<ProductoDto>> ProcesarProductosAsync(string rutaArchivo);
    }
}
