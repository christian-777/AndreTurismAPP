using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models;

namespace DBManipulation.Data
{
    public class DBManipulationContext : DbContext
    {
        public DBManipulationContext (DbContextOptions<DBManipulationContext> options)
            : base(options)
        {
        }

        public DbSet<Models.Ticket> Ticket { get; set; } = default!;
    }
}
