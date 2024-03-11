using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebNew.Data;
using WebNew.Models;

namespace WebNew.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RollesController : ControllerBase
    {
        private readonly WebNewsContext _context;

        public RollesController(WebNewsContext context)
        {
            _context = context;
        }

        // GET: api/Rolles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Rolles>>> GetRolles()
        {
            return await _context.Rolles.ToListAsync();
        }

        // GET: api/Rolles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Rolles>> GetRolles(int id)
        {
            var rolles = await _context.Rolles.FindAsync(id);

            if (rolles == null)
            {
                return NotFound();
            }

            return rolles;
        }

        // PUT: api/Rolles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRolles(int id, Rolles rolles)
        {
            if (id != rolles.Id)
            {
                return BadRequest();
            }

            _context.Entry(rolles).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RollesExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Rolles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Rolles>> PostRolles(Rolles rolles)
        {
            _context.Rolles.Add(rolles);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRolles", new { id = rolles.Id }, rolles);
        }

        // DELETE: api/Rolles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRolles(int id)
        {
            var rolles = await _context.Rolles.FindAsync(id);
            if (rolles == null)
            {
                return NotFound();
            }

            _context.Rolles.Remove(rolles);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RollesExists(int id)
        {
            return _context.Rolles.Any(e => e.Id == id);
        }
    }
}
