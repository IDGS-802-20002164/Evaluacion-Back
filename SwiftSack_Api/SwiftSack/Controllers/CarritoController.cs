using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SwiftSack.Context;
using SwiftSack.Models;

namespace SwiftSack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarritoController : Controller
    {
        private readonly AppDbContext _context;
        public CarritoController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("obtener-carrito/{id}")]
        public ActionResult GetT(int id)
        {
            try
            {
                return Ok(_context.carrito.Where(carrito => carrito.userId == id).ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{Id}", Name = "carrito")]
        public ActionResult Get(int Id)
        {
            try
            {
                var carrito = _context.carrito.FirstOrDefault(x => x.idCarrito == Id);
                return Ok(carrito);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public ActionResult<carrito> Post([FromBody] carrito carrito)
        {
            try
            {
                // Buscar el registro del carrito con el mismo ProductId y UserId
                var carritoExistente = _context.carrito
                    .SingleOrDefault(c => c.productId == carrito.productId && c.userId == carrito.userId);

                if (carritoExistente != null)
                {
                    // Actualizar la cantidad en el registro existente
                    carritoExistente.cantidad += carrito.cantidad;

                    _context.SaveChanges();

                    return Ok(carritoExistente); // Devuelve el registro modificado
                }
                else
                {
                    try
                    {

                        SqlParameter[] parametros = new SqlParameter[]
                        {
                    new SqlParameter("@IdCarrito", carrito.idCarrito),
                    new SqlParameter("@ProductId", carrito.productId),
                    new SqlParameter("@Cantidad", carrito.cantidad),
                    new SqlParameter("@UserId", carrito.userId),

                        };
                        _context.EjecutarSP("Usp_CarritoCreate", parametros);

                        return CreatedAtRoute("carrito", new { id = carrito.idCarrito }, carrito);
                    }
                    catch (Exception ex)
                    {
                        return BadRequest(ex.Message);
                    }

                    
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("obtener-items/{id}")]
        public ActionResult GetP(int id)
        {
            try
            {
                var productosEnCarrito = _context.productos
                    .Where(producto => _context.carrito
                    .Any(carrito => carrito.productId == producto.Id && carrito.userId == id))
                    .ToList();

                return Ok(productosEnCarrito);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public ActionResult Delete(int Id)
        {
            try
            {
                var carrito = _context.carrito.FirstOrDefault(carrito => carrito.idCarrito == Id);
                if (carrito != null)
                {
                    _context.carrito.Remove(carrito);
                    _context.SaveChanges();
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("limpiar-carrito/{id}")]
        public ActionResult DeleteCartItems(int id)
        {
            try
            {
                var cartItemsToDelete = _context.carrito.Where(c => c.userId == id).ToList();

                if (cartItemsToDelete.Count > 0)
                {
                    _context.carrito.RemoveRange(cartItemsToDelete);
                    _context.SaveChanges();
                    return Ok();
                }
                else
                {
                    return BadRequest("No se encontraron registros para eliminar.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
