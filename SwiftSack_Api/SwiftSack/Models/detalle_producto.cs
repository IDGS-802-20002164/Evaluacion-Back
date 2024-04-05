using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SwiftSack.Models
{

    [Table("detalle_producto")]
    public class detalle_producto
    {
        [Key]
        public int Id { get; set; }
        public int Id_materia { get; set; }
        public double Cantidad { get; set; }
        public int Id_producto { get; set; }

    }

    public class AgregarStockResult
    {
        public string Resultado { get; set; }
    }

}
