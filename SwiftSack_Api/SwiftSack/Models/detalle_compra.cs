using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace SwiftSack.Models
{
    [Table("detalleCompra")]
    public class detalleCompra
    {
        [Key]
        
        public int id { get; set; }
        [Column("idCompra")]
        public int? idCompra { get; set; }
        [Column("idProducto")]
        public int? idProducto { get; set; }
        public int? cantidad { get; set; }
        public double? precio { get; set; }
    }
}
