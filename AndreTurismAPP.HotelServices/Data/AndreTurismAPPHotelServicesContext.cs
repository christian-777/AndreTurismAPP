using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models;

namespace AndreTurismAPP.HotelServices.Data
{
    public class AndreTurismAPPHotelServicesContext : DbContext
    {
        public AndreTurismAPPHotelServicesContext (DbContextOptions<AndreTurismAPPHotelServicesContext> options)
            : base(options)
        {
        }

        public DbSet<Models.Hotel> Hotel { get; set; } = default!;
    }
}
