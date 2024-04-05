using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SwiftSack.Context;
using SwiftSack.Models;

namespace SwiftSack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DireccionController : Controller
    {
        private readonly AppDbContext _context;
        public DireccionController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("obtener-direcciones/{id}")]
        public ActionResult GetD(int id)
        {
            try
            {
                return Ok(_context.direccion.Where(direccion => direccion.idUser == id).ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{Id}", Name = "direccion")]
        public ActionResult Get(int Id)
        {
            try
            {
                var direccion = _context.direccion.FirstOrDefault(x => x.idDireccion == Id);
                return Ok(direccion);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public ActionResult<direccion> Post([FromBody] direccion direccion)
        {
            try
            {
                _context.direccion.Add(direccion);
                _context.SaveChanges();
                return CreatedAtRoute("direccion", new { id = direccion.idDireccion }, direccion);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{Id}")]
        public ActionResult Put(int Id, [FromBody] direccion direccion)
        {
            try
            {
                if (direccion.idDireccion == Id)
                {
                    _context.Entry(direccion).State = EntityState.Modified;
                    _context.SaveChanges();
                    return CreatedAtRoute("direccion", new { Id = direccion.idDireccion }, direccion);
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

        [HttpDelete]
        public ActionResult Delete(int Id)
        {
            try
            {
                var direccion = _context.direccion.FirstOrDefault(direccion => direccion.idDireccion == Id);
                if (direccion != null)
                {
                    _context.direccion.Remove(direccion);
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
    }
}
