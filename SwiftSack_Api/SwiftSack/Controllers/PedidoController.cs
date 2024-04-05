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
    public class PedidoController : Controller
    {
        private readonly AppDbContext _context;
        public PedidoController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult Get()
        {
            try
            {
                var vistaPedidos = _context.Set<pedido>().ToList();
                return Ok(vistaPedidos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("obtener-mis-pedidos/{Id}")]
        public ActionResult GetM(int Id)
        {
            try
            {
                var pedido = _context.pedido.Where(pedido => pedido.iduser == Id);
                return Ok(pedido);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{Id}", Name = "pedido")]
        public ActionResult Get(int Id)
        {
            try
            {
                var pedido = _context.pedido.FirstOrDefault(x => x.id == Id);
                return Ok(pedido);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult<pedido> Post([FromBody] pedido pedido)
        {
            try
            {

                SqlParameter[] parametros = new SqlParameter[]
                {
                    new SqlParameter("@Fecha", pedido.fecha),
                    new SqlParameter("@IdUser", pedido.iduser),
                    new SqlParameter("@Folio", pedido.folio),
                    new SqlParameter("@Direccion", 2),
                    new SqlParameter("@Estatus", pedido.estatus),
                };
                _context.EjecutarSP("Usp_PedidoCreate", parametros);

                return CreatedAtRoute("Pedidos", new { id = pedido.id }, pedido);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{Id}")]
        public ActionResult Put(int Id, [FromBody] pedido pedido)
        {
            try
            {
                if (pedido.id == Id)
                {
                    _context.Entry(pedido).State = EntityState.Modified;
                    _context.SaveChanges();
                    
                    return CreatedAtRoute("materia_prima", new { Id = pedido.id }, pedido);
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
