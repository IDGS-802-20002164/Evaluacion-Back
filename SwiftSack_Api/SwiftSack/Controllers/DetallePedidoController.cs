using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SwiftSack.Context;
using SwiftSack.Models;

namespace SwiftSack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetallePedidoController : Controller
    {
        private readonly AppDbContext _context;

        public DetallePedidoController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("{Id}")]
        public ActionResult Get(int Id)
        {
            try
            {
                var detalle = _context.detallePedido.Where(x => x.idPedido == Id);
                return Ok(detalle);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("obtener-id/{id}", Name = "items")]
        public ActionResult id(int id)
        {
            try
            {
                var productosPedido = _context.productos
                    .Where(producto => _context.detallePedido
                    .Any(detalle => detalle.idProducto  == producto.Id && detalle.idPedido == id))
                    .ToList();

                return Ok(productosPedido);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public ActionResult<detallePedido> AddDetalle([FromBody] detallePedido detalles)
        {
            try
            {
                var producto = _context.productos.SingleOrDefault(x => x.Id == detalles.idProducto);

                if (producto == null)
                {
                    return NotFound("Producto no encontrado");
                }

                if (producto.Stock < detalles.cantidad)
                {
                    return BadRequest("No hay suficiente stock disponible");
                }

                producto.Stock -= detalles.cantidad;

                _context.detallePedido.Add(detalles);
                _context.SaveChanges();

                return CreatedAtRoute("items", new { id = detalles.idPedido }, detalles);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno del servidor: " + ex.Message);
            }
        }

    }
}
