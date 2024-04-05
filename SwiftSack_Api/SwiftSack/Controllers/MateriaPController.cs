using SwiftSack.Context;
using SwiftSack.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace SwiftSack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MateriaPController : ControllerBase
    {
        private readonly AppDbContext _context;
        public MateriaPController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public ActionResult Get()
        {
            try
            {
                return Ok(_context.materiaPrima.ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{Id}", Name = "materiaPrima")]
        public ActionResult Get(int Id)
        {
            try
            {
                var materia = _context.materiaPrima.FirstOrDefault(x => x.id == Id);
                return Ok(materia);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public ActionResult<materiaP> Post([FromBody] materiaP materia)
        {
            try
            {
                _context.materiaPrima.Add(materia);
                _context.SaveChanges();
                return CreatedAtRoute("materiaPrima", new { id = materia.id }, materia);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{Id}")]
        public ActionResult Put(int Id, [FromBody] materiaP materia)
        {
            try
            {
                if (materia.id == Id)
                {
                    _context.Entry(materia).State = EntityState.Modified;
                    _context.SaveChanges();
                    return CreatedAtRoute("materiaPrima", new { Id = materia.id }, materia);
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
                var materia = _context.materiaPrima.FirstOrDefault(materiaP => materiaP.id == Id);
                if (materia != null)
                {
                    _context.materiaPrima.Remove(materia);
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
