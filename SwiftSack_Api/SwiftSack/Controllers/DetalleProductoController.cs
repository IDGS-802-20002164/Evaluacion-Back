using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SwiftSack.Context;
using SwiftSack.Models;

namespace SwiftSack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetalleProductoController : Controller

    {
        private readonly AppDbContext _context;

        public DetalleProductoController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id}", Name = "DetalleP")]

        public ActionResult Get(int id)
        {
            try
            {
                var detalles = _context.detalle.Where(x => x.Id_producto == id).ToList();
                if (detalles.Count == 0)
                {
                    return NotFound();
                }
                return Ok(detalles);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("obtener-id/{id}")]

        public ActionResult GetD(int id)
        {
            try
            {
                var detalles = _context.detalle.Where(x => x.Id == id).FirstOrDefault();
                if (detalles == null)
                {
                    return NotFound();
                }
                return Ok(detalles);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult Post([FromBody] detalle_producto dp)
        {
            try
            {
                _context.detalle.Add(dp);
                _context.SaveChanges();
                return CreatedAtRoute("DetalleP", new { id = dp.Id }, dp);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] detalle_producto dp)
        {
            try
            {
                if (dp.Id == id)
                {
                    _context.Entry(dp).State = EntityState.Modified;
                    _context.SaveChanges();
                    return CreatedAtRoute("DetalleP", new { id = dp.Id }, dp);

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

        [HttpDelete("{id}")]

        public ActionResult Delete(int id)
        {
            try
            {
                var detalleP = _context.detalle.FirstOrDefault(x => x.Id == id);
                if (detalleP != null)
                {
                    _context.detalle.Remove(detalleP);
                    _context.SaveChanges();
                    return Ok(id);
                }
                else { return BadRequest(); }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
