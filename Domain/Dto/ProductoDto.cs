using System.ComponentModel.DataAnnotations;

namespace Domain.Dto
{
    public class ProductoDto
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "El código es obligatorio.")]
        public string Codigo { get; set; }

        [Required(ErrorMessage = "El nombre del producto es obligatorio.")]
        [MaxLength(150, ErrorMessage = "El nombre del producto no puede tener más de 150 caracteres.")]
        public string NombreProducto { get; set; }

        [MaxLength(300, ErrorMessage = "La descripción no puede tener más de 300 caracteres.")]
        public string Descripcion { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "El stock debe ser mayor a 0.")]
        public int Stock { get; set; }

        [Required(ErrorMessage = "La fecha de vencimiento es obligatoria.")]
        public DateTime FechaVencimiento { get; set; }
    }
}
