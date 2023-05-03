using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models;

namespace AndreTurismAPP.CustomerServices.Data
{
    public class AndreTurismAPPCustomerServicesContext : DbContext
    {
        public AndreTurismAPPCustomerServicesContext (DbContextOptions<AndreTurismAPPCustomerServicesContext> options)
            : base(options)
        {
        }

        public DbSet<Models.Customer> Customer { get; set; } = default!;
    }
}
