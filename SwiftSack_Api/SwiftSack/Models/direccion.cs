using System.ComponentModel.DataAnnotations;

namespace SwiftSack.Models
{
    public class direccion
    {
        [Key]
        public int idDireccion { get; set; }
        public int? idUser { get; set; }
        public string? nombreCompleto { get; set; }
        public string? calleNumero { get; set; }
        public string? codigoPostal { get; set; }
        public string? telefono { get; set; }
    }
}
