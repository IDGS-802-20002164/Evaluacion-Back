using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SwiftSack.Models
{
    [Table("compra")]
    public class compra
    {
        [Key]
        public int idCompra { get; set; }
        public DateTime? fecha { get; set; }
        [Column("iduser")]
        public int? iduser { get; set; }
        [Column("idProveedor")]
        public int? idProveedor { get; set; }
        public string? folio { get; set; }
        public int? estatus { get; set; }
    }
}
