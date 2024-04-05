using System.Text.Json.Serialization;

namespace SwiftSack.Models
{
    public class CompraConDetalle
    {
        public compra Compra { get; set; }
        [JsonIgnore]
        public List<detalleCompra> Detalles { get; set; }
    }
}
