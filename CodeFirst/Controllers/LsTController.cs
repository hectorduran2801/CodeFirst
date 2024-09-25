using Microsoft.AspNetCore.Http;
using DBCodeFirst;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CodeFirst.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LsTController : ControllerBase
    {

        private readonly CodeFirstContext _context;

        public LsTController(CodeFirstContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ListadoTemporal>>> GetListados()
        {
            return await _context.ListadosTemporal.ToListAsync();
        }

    }
}
