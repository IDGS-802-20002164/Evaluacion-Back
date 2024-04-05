using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SwiftSack.Context;
using SwiftSack.Models;

namespace SwiftSack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarjetaController : Controller
    {
        private readonly AppDbContext _context;
        public TarjetaController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("obtener-tarjetas/{id}")]
        public ActionResult GetT(int id)
        {
            try
            {
                return Ok(_context.tarjeta.Where(tarjeta => tarjeta.idUser == id).ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{Id}", Name = "tarjeta")]
        public ActionResult Get(int Id)
        {
            try
            {
                var tarjeta = _context.tarjeta.FirstOrDefault(x => x.idTarjeta == Id);
                return Ok(tarjeta);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public ActionResult<tarjeta> Post([FromBody] tarjeta tarjeta)
        {
            try
            {
                tarjeta.numTarEncryp = tarjeta.numeroTarjeta.Substring(12);
                _context.tarjeta.Add(tarjeta);
                _context.SaveChanges();
                return CreatedAtRoute("tarjeta", new { id = tarjeta.idTarjeta }, tarjeta);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{Id}")]
        public ActionResult Put(int Id, [FromBody] tarjeta tarjeta)
        {
            try
            {
                if (tarjeta.idTarjeta == Id)
                {
                    _context.Entry(tarjeta).State = EntityState.Modified;
                    _context.SaveChanges();
                    return CreatedAtRoute("tarjeta", new { Id = tarjeta.idTarjeta }, tarjeta);
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
                var tarjeta = _context.tarjeta.FirstOrDefault(tarjeta => tarjeta.idTarjeta == Id);
                if (tarjeta != null)
                {
                    _context.tarjeta.Remove(tarjeta);
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
