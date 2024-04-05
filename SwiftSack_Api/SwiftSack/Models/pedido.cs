using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SwiftSack.Models
{
    [Table("pedido")]
    public class pedido
    {
        [Key]
        public int id { get; set; }
        public DateTime? fecha { get; set; }
        [Column("iduser")]
        public int? iduser { get; set; }
        public int? direccion { get; set; }
        public string? folio { get; set; }
        public int? estatus { get; set; }
    }
}
