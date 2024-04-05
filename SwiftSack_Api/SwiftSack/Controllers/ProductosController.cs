using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SwiftSack.Context;
using SwiftSack.Models;
using System;


namespace SwiftSack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult Get()
        {
            try
            {
                return Ok(_context.productos.ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("{id}", Name = "Productos")]

        public ActionResult Get(int id)
        {
            try
            {
                var producto = _context.productos.FirstOrDefault(x => x.Id == id);
                if (producto == null)
                {
                    return NotFound();
                }
                return Ok(producto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult Post([FromBody] producto p)
        {
            try
            {
                _context.productos.Add(p);
                _context.SaveChanges();
                return CreatedAtRoute("Productos", new { id = p.Id }, p);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("agregar-stock/{id}")]
        public ActionResult Put(int id, [FromBody]  int stock)
        {
            try
            {
                var resultado = _context.Set<AgregarStockResult>()
            .FromSqlRaw($"EXEC agregar_stock {stock}, {id}")
            .AsEnumerable()
            .FirstOrDefault();
                if (resultado != null)
                {
                    return Ok(resultado);
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

        [HttpPut("{id}")]
//p
        public ActionResult Put(int id, [FromBody] producto p)
        {
            try
            {
                if (p.Id == id)
                {
                    _context.Entry(p).State = EntityState.Modified;
                    _context.SaveChanges();
                    return CreatedAtRoute("Productos", new { id = p.Id }, p);

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
                var producto = _context.productos.FirstOrDefault(x => x.Id == id);
                if (producto != null)
                {
                    _context.productos.Remove(producto);
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
