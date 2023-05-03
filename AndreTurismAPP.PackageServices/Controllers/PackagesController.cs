using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AndreTurismAPP.PackageServices.Data;
using Models;
using AndreTurismAPP.PackageServices.Services;

namespace AndreTurismAPP.PackageServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PackagesController : ControllerBase
    {
        private readonly AndreTurismAPPPackageServicesContext _context;

        public PackagesController(AndreTurismAPPPackageServicesContext context)
        {
            _context = context;
        }

        // GET: api/Packages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Package>>> GetPackage()
        {
          if (_context.Package == null)
          {
              return NotFound();
          }
            return await _context.Package.ToListAsync();
        }

        // GET: api/Packages/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Package>> GetPackage(int id)
        {
          if (_context.Package == null)
          {
              return NotFound();
          }
            var package = await _context.Package.FindAsync(id);

            if (package == null)
            {
                return NotFound();
            }

            return package;
        }

        // PUT: api/Packages/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPackage(int id, Package package)
        {
            if (id != package.Id)
            {
                return BadRequest();
            }

            _context.Entry(package).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PackageExists(id))
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

        // POST: api/Packages
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Package>> PostPackage(Package package)
        {
          if (_context.Package == null)
          {
              return Problem("Entity set 'AndreTurismAPPPackageServicesContext.Package'  is null.");
          }
            var hotel = (await HotelService.GetHotel()).Where(h=>h.Id==package.Hotel.Id).First();
            if (hotel == null)
                return NotFound();

            var aux= (await TicketService.GetTicket());
            bool flag = false;
            Ticket ticket=new();
            foreach(var item in aux)
            {
                var i = item.Id;
                if (item.Id == package.Ticket.Id)
                {
                    ticket = item;
                    flag = true;
                }
            }
            if (flag == false)
                return NotFound();

            var customer = (await CustomerService.GetCustomer()).Where(c => c.Id == package.Customer.Id).First();
            if (customer == null)
                return NotFound();
            customer.Id = 0;
            hotel.Id = 0;
            ticket.Id = 0;

            package.Hotel= hotel;
            package.Ticket = ticket;
            package.Customer = customer;

            _context.Entry(hotel).State = EntityState.Added;
            _context.Entry(ticket).State = EntityState.Added;
            _context.Entry(customer).State = EntityState.Added;
            _context.Entry(package).State = EntityState.Added;
            _context.Package.Add(package);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPackage", new { id = package.Id }, package);
        }

        // DELETE: api/Packages/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePackage(int id)
        {
            if (_context.Package == null)
            {
                return NotFound();
            }
            var package = await _context.Package.FindAsync(id);
            if (package == null)
            {
                return NotFound();
            }

            _context.Package.Remove(package);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PackageExists(int id)
        {
            return (_context.Package?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
