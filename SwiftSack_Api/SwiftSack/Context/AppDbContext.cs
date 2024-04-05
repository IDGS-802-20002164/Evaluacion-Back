using SwiftSack.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using System.Data;

namespace SwiftSack.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<proveedor> proveedor { get; set; }
        public DbSet<materiaP> materiaPrima { get; set; }
        public DbSet<carrito> carrito { get; set; }
        public DbSet<tarjeta> tarjeta { get; set; }
        public DbSet<direccion> direccion { get; set; }

        public DbSet<producto> productos { get; set; }
        public DbSet<detalle_producto> detalle { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<compra> compra { get; set; }
        public DbSet<detalleCompra> detalleCompra { get; set; }
        public DbSet<pedido> pedido { get; set; }
        public DbSet<detallePedido> detallePedido { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AgregarStockResult>().HasNoKey();
        }

        public void EjecutarSP(string nombreSP, SqlParameter[] parametros)
        {
            // Crear un objeto SqlConnection utilizando la cadena de conexión del contexto
            using (SqlConnection connection = (SqlConnection)Database.GetDbConnection())
            {
                // Abrir la conexión
                connection.Open();

                // Crear un objeto SqlCommand para el SP
                using (SqlCommand cmd = new SqlCommand(nombreSP, connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Agregar parámetros al SP si es necesario
                    if (parametros != null)
                    {
                        cmd.Parameters.AddRange(parametros);
                    }

                    // Ejecutar el SP
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
