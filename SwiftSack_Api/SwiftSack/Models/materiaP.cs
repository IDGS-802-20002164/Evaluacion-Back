using System.ComponentModel.DataAnnotations;


namespace SwiftSack.Models
{
    public class materiaP
    {
        [Key]
        public int id { get; set; }
        public string? nombre { get; set; }
        public int? cantidad { get; set; }
        public string? unidadMedida { get; set; }
        public int? costo { get; set; }
        public int? idProveedor { get; set; }
        public bool? estatus { get; set; }
    }
}
