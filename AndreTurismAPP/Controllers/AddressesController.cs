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
    public class AddressesController : ControllerBase
    {
        private readonly AndreTurismAPPContext _context;

        public AddressesController(AndreTurismAPPContext context)
        {
            _context = context;
        }

        // GET: api/Addresses
        [HttpGet]
        public async Task<ActionResult<List<Address>>> GetAddress()
        {
          if (_context.Address == null)
          {
              return NotFound();
          }
            return AddressService.GetAddress().Result;
        }

        // GET: api/Addresses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Address>> GetAddress(int id)
        {
          if (_context.Address == null)
          {
              return NotFound();
          }
            var address = await _context.Address.FindAsync(id);

            if (address == null)
            {
                return NotFound();
            }

            return address;
        }

        // PUT: api/Addresses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAddress(int id, Address address)
        {
            if (id != address.Id)
            {
                return BadRequest();
            }

            _context.Entry(address).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AddressExists(id))
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

        // POST: api/Addresses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("{cep}")]
        public async Task<ActionResult<Address>> PostAddress(string cep)
        {
          if (_context.Address == null)
          {
              return Problem("Entity set 'AndreTurismAPPContext.Address'  is null.");
          }
            Address address = new Address()
            {
                CEP = cep,
                City=new City()
            };

            return AddressService.PostAddress(address).Result;
        }

        // DELETE: api/Addresses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAddress(int id)
        {
            if (_context.Address == null)
            {
                return NotFound();
            }
            var address = await _context.Address.FindAsync(id);
            if (address == null)
            {
                return NotFound();
            }

            _context.Address.Remove(address);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AddressExists(int id)
        {
            return (_context.Address?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
