using Microsoft.AspNetCore.Http;
using DBCodeFirst;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CodeFirst.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListadosController : ControllerBase
    {

        private readonly CodeFirstContext _context;

        public ListadosController(CodeFirstContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Listado>>> GetListados()
        {
            return await _context.Listados.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Listado>> GetListadoXId(int id)
        {
            if (!IdExiste(id))
            {
                return NotFound();
            }

            var listado = await _context.Listados.FindAsync(id);

            if (listado == null)
            {
                return NotFound();
            }

            return listado;
        }

        [HttpPost]
        public async Task<ActionResult<Listado>> AddListado(Listado listado)
        {
            _context.Listados.Add(listado);
            await _context.SaveChangesAsync();

            // aca mandamos los datos a la tabla "temporal"
            await AddTablaTemporal(listado, "add");

            return CreatedAtAction(nameof(GetListados), new { id = listado.Id }, listado);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditListado(int id, Listado listado)
        {
            if (id != listado.Id)
            {
                return BadRequest();
            }

            if (!IdExiste(id))
            {
                return NotFound();
            }

            _context.Entry(listado).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

                // aca mandamos los datos a la tabla "temporal"
                await AddTablaTemporal(listado, "editar");
            }
             catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            return Ok(listado);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteListado(int id)
        {
            if (!IdExiste(id))
            {
                return NotFound();
            }

            var listado = await _context.Listados.FindAsync(id);

            if (listado == null)
            {
                return NotFound();
            }

            _context.Listados.Remove(listado);
            await _context.SaveChangesAsync();

            return Ok(listado);
        }

        [HttpPut("change-state/{id}")]
        public async Task<IActionResult> ChangeState(int id)
        {
            if (!IdExiste(id))
            {
                return NotFound();
            }

            var listado = await _context.Listados.FindAsync(id);
            
            if (listado == null)
            {
                return NotFound();
            }

            listado.estado = !listado.estado;

            await _context.SaveChangesAsync();

            return Ok(listado);
        }

        [HttpPost("add-listadoss")]
        public async Task<IActionResult> AddListadoss(List<Listado> listados)
        {
            if (listados.Count == 0)
            {
                return BadRequest();
            }

            _context.Listados.AddRange(listados);

            try
            {
                await _context.SaveChangesAsync();

                // aca mandamos los datos a la tabla "temporal"
                foreach (var listado in listados)
                {
                    await AddTablaTemporal(listado, "add");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            return Ok(listados);
        }



        private bool IdExiste(int id)
        {
            return _context.Listados.Any(e => e.Id == id);
        }

        private async Task AddTablaTemporal(Listado listado, string Tipo)
        {
            var temporal = new ListadoTemporal
            {
                ListadoId = listado.Id,
                Titulo = listado.titulo,
                Descripcion = listado.descripcion,
                FechaCreacion = listado.fechaCreacion,
                FechaFinalizacion = listado.fechaFinalizacion,
                Estado = listado.estado,
                Tipo = Tipo
            };

            _context.ListadosTemporal.Add(temporal);
            await _context.SaveChangesAsync();

        }
    }
}
