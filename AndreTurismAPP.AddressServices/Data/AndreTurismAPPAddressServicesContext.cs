using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models;

namespace AndreTurismAPP.AddressServices.Data
{
    public class AndreTurismAPPAddressServicesContext : DbContext
    {
        public AndreTurismAPPAddressServicesContext (DbContextOptions<AndreTurismAPPAddressServicesContext> options)
            : base(options)
        {
        }

        public DbSet<Models.Address> Address { get; set; } = default!;
    }
}
