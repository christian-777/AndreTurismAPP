using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models;

namespace AndreTurismAPP.Data
{
    public class AndreTurismAPPContext : DbContext
    {
        public AndreTurismAPPContext (DbContextOptions<AndreTurismAPPContext> options)
            : base(options)
        {
        }

        public DbSet<Models.Address> Address { get; set; } = default!;

        public DbSet<Models.Customer>? Customer { get; set; }

        public DbSet<Models.Hotel>? Hotel { get; set; }
    }
}
