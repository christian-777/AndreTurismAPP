using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AndreTurismAPP.Data;
using Models;
using AndreTurismAPP.Services;

namespace AndreTurismAPP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private readonly AndreTurismAPPContext _context;

        public HotelsController(AndreTurismAPPContext context)
        {
            _context = context;
        }

        // GET: api/Hotels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Hotel>>> GetHotel()
        {
          if (_context.Hotel == null)
          {
              return NotFound();
          }
            return HotelService.GetHotel().Result; ;
        }

        // GET: api/Hotels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Hotel>> GetHotel(int id)
        {
          if (_context.Hotel == null)
          {
              return NotFound();
          }
            var hotel = await _context.Hotel.FindAsync(id);

            if (hotel == null)
            {
                return NotFound();
            }

            return hotel;
        }

        // PUT: api/Hotels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHotel(int id, Hotel hotel)
        {
            if (id != hotel.Id)
            {
                return BadRequest();
            }

            _context.Entry(hotel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HotelExists(id))
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

        // POST: api/Hotels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Hotel>> PostHotel(Hotel hotel)
        {
          if (_context.Hotel == null)
          {
              return Problem("Entity set 'AndreTurismAPPContext.Hotel'  is null.");
          }

            return HotelService.PostHotel(hotel).Result;
        }

        // DELETE: api/Hotels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotel(int id)
        {
            if (_context.Hotel == null)
            {
                return NotFound();
            }
            var hotel = await _context.Hotel.FindAsync(id);
            if (hotel == null)
            {
                return NotFound();
            }

            _context.Hotel.Remove(hotel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HotelExists(int id)
        {
            return (_context.Hotel?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
