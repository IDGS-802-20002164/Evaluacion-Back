using SwiftSack.Context;
using SwiftSack.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace SwiftSack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProveMaterController : ControllerBase
    {
        private readonly AppDbContext _context;
        public ProveMaterController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public ActionResult Get()
        {
            try
            {
                return Ok(_context.proveedor.ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{Id}", Name = "proveedor")]
        public ActionResult Get(int Id)
        {
            try
            {
                var prov = _context.proveedor.FirstOrDefault(x => x.id == Id);
                return Ok(prov);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult<proveedor> Post([FromBody] proveedor prove)
        {
            try
            {

                // Llamar al procedimiento almacenado después de guardar el producto
                SqlParameter[] parametros = new SqlParameter[]
                {
                    new SqlParameter("@Nombre", prove.nombre),
                    new SqlParameter("@Empresa", prove.empresa),
                    new SqlParameter("@Telefono", prove.telefono),
                    new SqlParameter("@Rfc", prove.rfc),
                    new SqlParameter("@Email", prove.email),
                    new SqlParameter("@Estatus", prove.estatus),
                    // Agregar más parámetros según sea necesario
                };
                _context.EjecutarSP("Usp_ProvedorCreate", parametros);

                return CreatedAtRoute("Productos", new { id = prove.id }, prove);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{Id}")]
        public ActionResult Put(int Id, [FromBody] proveedor prove)
        {
            try
            {
                if (prove.id == Id)
                {
                    _context.Entry(prove).State = EntityState.Modified;
                    _context.SaveChanges();
                    return CreatedAtRoute("proveedor", new { Id = prove.id }, prove);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                // Puedes imprimir el mensaje de error en la consola o registrarlo en un archivo de registro
                Console.WriteLine(errorMessage);
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete]
        public ActionResult Delete(int Id)
        {
            try
            {
                var prove = _context.proveedor.FirstOrDefault(proveedor => proveedor.id == Id);
                if (prove != null)
                {
                    _context.proveedor.Remove(prove);
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
