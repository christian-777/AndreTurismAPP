using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models;

namespace AndreTurismAPP.TicketServices.Data
{
    public class AndreTurismAPPTicketServicesContext : DbContext
    {
        public AndreTurismAPPTicketServicesContext (DbContextOptions<AndreTurismAPPTicketServicesContext> options)
            : base(options)
        {
        }

        public DbSet<Models.Ticket> Ticket { get; set; } = default!;
    }
}
