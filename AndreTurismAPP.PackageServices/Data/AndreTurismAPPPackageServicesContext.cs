using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models;

namespace AndreTurismAPP.PackageServices.Data
{
    public class AndreTurismAPPPackageServicesContext : DbContext
    {
        public AndreTurismAPPPackageServicesContext (DbContextOptions<AndreTurismAPPPackageServicesContext> options)
            : base(options)
        {
        }

        public DbSet<Models.Package> Package { get; set; } = default!;
    }
}
