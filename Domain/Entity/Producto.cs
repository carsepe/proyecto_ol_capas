using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entity
{
    public class Producto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Codigo { get; set; }

        [Required]
        [MaxLength(150)]
        public string NombreProducto { get; set; }

        [MaxLength(300)]
        public string Descripcion { get; set; }

        [Required]
        public int Stock { get; set; }

        [Required]
        public DateTime FechaVencimiento { get; set; }

        [Required]
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

        public DateTime? FechaModificacion { get; set; }
    }
}
