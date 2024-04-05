using System.ComponentModel.DataAnnotations.Schema;

namespace SwiftSack.Models
{
    [Table("role")] // Asegúrate de que refleje el nombre correcto de la tabla en tu base de datos
    public class Rol
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
