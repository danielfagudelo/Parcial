using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Parcial.API.Data;
using Parcial.Shared.Entities;

namespace Parcial.API.Controllers
{
    [ApiController]
    [Route("/api/Boletas")]
    public class BoletaController : Controller
    {
        private readonly DataContext _context;


        public BoletaController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var boleta = await _context.Boletas.FirstOrDefaultAsync(x => x.Id == id);
            if (boleta == null)
            {
                return NotFound();
            }

            return Ok(boleta);
        }

        [HttpPut]
        public async Task<ActionResult> PutAsync(Boleta boleta)
        {
            try
            {
                _context.Update(boleta);
                await _context.SaveChangesAsync();
                return Ok(boleta);
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException!.Message.Contains("duplicado"))
                {
                    return BadRequest("Boleta usada.");
                }
                return BadRequest(ex.InnerException.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

}
