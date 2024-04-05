using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SwiftSack.Context;
using SwiftSack.Models;

namespace SwiftSack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetalleCompraController : Controller
    {
        private readonly AppDbContext _context;

        public DetalleCompraController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("obtener-id/{id}", Name = "detalle")]
        public ActionResult id(int id)
        {
            try
            {
                return Ok(_context.detalleCompra.Where(c => c.idCompra == id).ToList());

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public ActionResult<detalleCompra> AddDetalle([FromBody] detalleCompra detalles)
        {
            try
            {
                _context.detalleCompra.Add(detalles);
                
                var materia = _context.materiaPrima.FirstOrDefault(m => m.id == detalles.idProducto);
                materia.cantidad += detalles.cantidad;
                _context.Entry(materia).State = EntityState.Modified;

                _context.SaveChanges();
                return CreatedAtRoute("detalle", new { id = detalles.idCompra }, detalles);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException);
            }
        }


    }
}
