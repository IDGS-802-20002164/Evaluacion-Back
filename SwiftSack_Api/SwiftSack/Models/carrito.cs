using System.ComponentModel.DataAnnotations;

namespace SwiftSack.Models
{
    public class carrito
    {
        [Key]
        public int idCarrito { get; set; }
        public int? userId { get; set; }
        public int? productId { get; set; }
        public int? cantidad { get; set; }
    }
}
