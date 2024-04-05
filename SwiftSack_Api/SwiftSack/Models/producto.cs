using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SwiftSack.Models
{
    [Table("producto")]
    public class producto
    {
        [Key]
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public decimal Costo { get; set; }
        public string? Foto { get; set; }
        public string? Tipo_producto { get; set; }
        public string? Receta { get; set; }
        public int? Stock { get; set; }
    }
}
