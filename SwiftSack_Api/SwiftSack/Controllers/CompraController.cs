using Microsoft.AspNetCore.Mvc;
using SwiftSack.Context;
using SwiftSack.Models;

namespace SwiftSack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompraController : Controller
    {
        private readonly AppDbContext _context;

        public CompraController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet ("{idProveedor}")]
        public ActionResult Get(int idProveedor)
        {
            try
            {
                return Ok(_context.materiaPrima.Where(mp => mp.idProveedor == idProveedor).ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("obtener-id/{id}", Name = "compra")]
        public ActionResult id(int id)
        {
            try
            {
                var compra = _context.compra.FirstOrDefault(c => c.idCompra == id);
                if (compra != null)
                {
                    return Ok(compra);
                }
                else
                {
                    return NotFound();
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public ActionResult<compra> Post([FromBody] compra nuevaCompra)
        {
            try
            {
                _context.compra.Add(nuevaCompra);
                _context.SaveChanges();
                return CreatedAtRoute("compra", new { id = nuevaCompra.idCompra }, nuevaCompra);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException);
            }
        }
    }
}
