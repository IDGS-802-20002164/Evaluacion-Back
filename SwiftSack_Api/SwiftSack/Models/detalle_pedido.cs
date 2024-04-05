
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SwiftSack.Models
{
    [Table("detallePedido")]
    public class detallePedido
    {
        [Key]
        public int id { get; set; }
        [Column("idPedido")]
        public int? idPedido { get; set; }
        [Column("idProducto")]
        public int? idProducto { get; set; }
        public int? cantidad { get; set; }
        public double? costoTotal { get; set; }
    }
}
