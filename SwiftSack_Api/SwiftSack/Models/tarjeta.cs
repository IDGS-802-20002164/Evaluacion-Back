using System.ComponentModel.DataAnnotations;

namespace SwiftSack.Models
{
    public class tarjeta
    {
        [Key]
        public int idTarjeta { get; set; }
        public int? idUser { get; set; }
        public string? numeroTarjeta { get; set; }
        public string? numTarEncryp { get; set; }
        public string? nombreTarjeta { get; set; }
        public string? mesVencimiento { get; set; }
        public string? annioVencimiento { get; set; }
        public string? ccv { get; set; }
    }
}
