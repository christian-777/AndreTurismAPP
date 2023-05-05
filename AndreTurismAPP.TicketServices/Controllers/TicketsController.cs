using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AndreTurismAPP.TicketServices.Data;
using Models;
using AndreTurismAPP.TicketServices.Services;
using Newtonsoft.Json;
using System.Text;
using Microsoft.AspNetCore.Connections;
using RabbitMQ.Client;

namespace AndreTurismAPP.TicketServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly AndreTurismAPPTicketServicesContext _context;
        private readonly ConnectionFactory _factory;
        private const string QUEUE_NAME = "message";

        public TicketsController(AndreTurismAPPTicketServicesContext context, ConnectionFactory factory)
        {
            _context = context;
            _factory = factory;
        }

        // GET: api/Tickets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ticket>>> GetTicket()
        {
          if (_context.Ticket == null)
          {
              return NotFound();
          }
            var aux = await _context.Ticket.Include(t => t.Source).Include(t=> t.Source.City).Include(t => t.Destiny).Include(t => t.Destiny.City).Include(t => t.Customer).Include(t => t.Customer.Address).Include(t => t.Customer.Address.City).ToListAsync();
            return aux;
        }

        // GET: api/Tickets/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Ticket>> GetTicket(int id)
        {
          if (_context.Ticket == null)
          {
              return NotFound();
          }
            var ticket = await _context.Ticket.FindAsync(id);

            if (ticket == null)
            {
                return NotFound();
            }

            return ticket;
        }

        // PUT: api/Tickets/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTicket(int id, Ticket ticket)
        {
            if (id != ticket.Id)
            {
                return BadRequest();
            }

            _context.Entry(ticket).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TicketExists(id))
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

        // POST: api/Tickets
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /*[HttpPost]
        public async Task<ActionResult<Ticket>> PostTicket(Ticket ticket)
        {
          if (_context.Ticket == null)
          {
              return Problem("Entity set 'AndreTurismAPPTicketServicesContext.Ticket'  is null.");
          }
            var source = await AddressService.GetAddressByCep(ticket.Source.CEP);
           
            if (source != null)
                NotFound();
           
            ticket.Source = source;
           
            var dest = await AddressService.GetAddressByCep(ticket.Destiny.CEP);
           
            if (dest != null)
                NotFound();
           
            ticket.Destiny = dest;
           
            var customer = (await CustomerService.GetCustomer()).Where(c=>c.Id==ticket.Customer.Id).First();
           
            if (customer != null)
                NotFound();
            
            ticket.Customer = customer;

            _context.Entry(customer).State = EntityState.Modified;
            _context.Entry(source).State = EntityState.Added;
            _context.Entry(dest).State = EntityState.Added;
            _context.Entry(source.City).State = EntityState.Added;
            _context.Entry(dest.City).State = EntityState.Added;
            _context.Entry(ticket).State = EntityState.Added;


            _context.Ticket.Add(ticket);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTicket", new { id = ticket.Id }, ticket);
        }*/

        //POST mensageria
        [HttpPost]
        public IActionResult PostMQMessage([FromBody] Ticket ticket)
        {
            using (var connection = _factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {

                    channel.QueueDeclare(
                        queue: QUEUE_NAME,
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null
                        );

                    var stringfieldMessage = JsonConvert.SerializeObject(ticket);
                    var bytesMessage = Encoding.UTF8.GetBytes(stringfieldMessage);

                    channel.BasicPublish(
                        exchange: "",
                        routingKey: QUEUE_NAME,
                        basicProperties: null,
                        body: bytesMessage
                        );
                }
            }
            return Accepted();
        }

        // DELETE: api/Tickets/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTicket(int id)
        {
            if (_context.Ticket == null)
            {
                return NotFound();
            }
            var ticket = await _context.Ticket.FindAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }

            _context.Ticket.Remove(ticket);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TicketExists(int id)
        {
            return (_context.Ticket?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
