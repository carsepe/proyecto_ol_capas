using System.ComponentModel.DataAnnotations;

namespace Domain.Dto
{
    public class ClienteDto
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "El tipo de identificación es obligatorio.")]
        public string TipoIdentificacion { get; set; }

        [Required(ErrorMessage = "La identificación es obligatoria.")]
        public string Identificacion { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [MaxLength(150, ErrorMessage = "El nombre no puede tener más de 150 caracteres.")]
        public string Nombre { get; set; }

        [MaxLength(300, ErrorMessage = "La dirección no puede tener más de 300 caracteres.")]
        public string Direccion { get; set; }

        [Phone(ErrorMessage = "El teléfono no tiene un formato válido.")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "La fecha de nacimiento es obligatoria.")]
        public DateTime FechaNacimiento { get; set; }
    }
}
