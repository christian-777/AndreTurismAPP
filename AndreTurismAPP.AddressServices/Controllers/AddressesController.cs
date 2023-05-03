using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AndreTurismAPP.AddressServices.Data;
using Models;
using AndreTurismAPP.AddressServices.Services;
using System.Runtime.Intrinsics.X86;

namespace AndreTurismAPP.AddressServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressesController : ControllerBase
    {
        private readonly AndreTurismAPPAddressServicesContext _context;

        public AddressesController(AndreTurismAPPAddressServicesContext context)
        {
            _context = context;
        }

        // GET: api/Addresses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Address>>> GetAddress()
        {
          if (_context.Address == null)
          {
              return NotFound();
          }
            return await _context.Address.Include(a=>a.City).ToListAsync();
        }

        // GET: api/Addresses/5
        [HttpGet("{id}", Name ="GetById")]
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

        [HttpGet("{cep:length(8)}", Name = "GetAddressByCep")]
        public async Task<ActionResult<Address>> GetAddressByCep(string cep)
        {
            var aux = ViaCEP.GetAddress(cep).Result;
            if(aux != null)
            {
                NotFound();
            }
            Address address = new Address()
            {
                CEP = aux.CEP,
                Description = aux.Description,
                Number = aux.Number,
                Neighborhood = aux.Neighborhood,
                RegisterDate = DateTime.Now.ToString(),
                Street = aux.Street,
                City = new City()
                {
                    Description = aux.City,
                    RegisterDate = DateTime.Now.ToString()
                }

            };

            return address;
        }

        // PUT: api/Addresses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<Address>> PutAddress(int id, Address address)
        {
            if (id != address.Id)
            {
                return BadRequest();
            }

            _context.Entry(address).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return address;
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
        [HttpPost]
        public async Task<ActionResult<Address>> PostAddress(Address ad)
        {
          if (_context.Address == null)
          {
              return Problem("Entity set 'AndreTurismAPPAddressServicesContext.Address'  is null.");
          }
            AddressDTO aux = ViaCEP.GetAddress(ad.CEP).Result;
            Address address = new Address()
            {
                CEP = aux.CEP,
                Description=aux.Description,
                Number = aux.Number,
                Neighborhood = aux.Neighborhood,
                RegisterDate= DateTime.Now.ToString(),
                Street = aux.Street,
                City= new City()
                {
                    Description=aux.City,
                    RegisterDate= DateTime.Now.ToString()
                }

            };
            _context.Address.Add(address);
            await _context.SaveChangesAsync();

            return address;
        }

        // DELETE: api/Addresses/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Address>> DeleteAddress(int id)
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
