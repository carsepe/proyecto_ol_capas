using Domain.Dto;
using Domain.Interface;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using System.Text.Json;
using System.Formats.Asn1;

namespace Repository
{
    public class PipelineTransformacionDatosRepository : IPipelineTransformacionDatosRepository
    {
        public async Task<IEnumerable<ClienteDto>> ProcesarClientesAsync(string rutaArchivo)
        {
            // Leer datos desde el archivo CSV
            var clientes = LeerCsv<ClienteDto>(rutaArchivo);

            // Exportar a JSON
            ExportarJson(clientes, "clientesTransformados.json");

            return clientes;
        }

        public async Task<IEnumerable<ProductoDto>> ProcesarProductosAsync(string rutaArchivo)
        {
            // Leer datos desde el archivo CSV
            var productos = LeerCsv<ProductoDto>(rutaArchivo);

            // Exportar a JSON
            ExportarJson(productos, "productosTransformados.json");

            return productos;
        }

        // Método privado para leer datos desde un archivo CSV
        private IEnumerable<T> LeerCsv<T>(string rutaArchivo) where T : class
        {
            using var lector = new StreamReader(rutaArchivo);
            using var csv = new CsvReader(lector, new CsvConfiguration(CultureInfo.InvariantCulture));
            return csv.GetRecords<T>().ToList();
        }

        // Método privado para exportar datos a un archivo JSON
        private void ExportarJson<T>(IEnumerable<T> datos, string rutaSalida)
        {
            var opciones = new JsonSerializerOptions { WriteIndented = true };
            var json = JsonSerializer.Serialize(datos, opciones);
            File.WriteAllText(rutaSalida, json);
        }
    }
}
