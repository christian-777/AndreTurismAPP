using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AndreTurismAPP.CustomerServices.Data;
using AndreTurismAPP.HotelServices.Controllers;
using AndreTurismAPP.HotelServices.Data;
using Microsoft.EntityFrameworkCore;
using Models;

namespace AndreTurismAPP.Test
{
    public class UnitTestHotel
    {
        private DbContextOptions<AndreTurismAPPHotelServicesContext> options;

        private void InitializeDataBase()
        {
            // Create a Temporary Database
            options = new DbContextOptionsBuilder<AndreTurismAPPHotelServicesContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            // Insert data into the database using one instance of the context
            using (var context = new AndreTurismAPPHotelServicesContext(options))
            {
                context.Hotel.Add(new Hotel()
                {

                    Id = 1,
                    Name = "hotel1",
                    Cost=1,
                    RegisterDate = "04-04-2000",
                    Address = new()
                    {
                        CEP = "11111111",
                        City = new()
                        {
                            Description = "cidade1"
                        }
                    }

                });

                context.Hotel.Add(new Hotel()
                {
                    Id = 2,
                    Name = "hotel2",
                    Cost = 2,
                    RegisterDate = "04-04-2000",
                    Address = new()
                    {
                        CEP = "22222222",
                        City = new()
                        {
                            Description = "cidade2"
                        }
                    }

                });

                context.Hotel.Add(new Hotel()
                {
                    Id = 3,
                    Name = "hotel3",
                    Cost = 3,
                    RegisterDate = "04-04-2000",
                    Address = new()
                    {
                        CEP = "33333333",
                        City = new()
                        {
                            Description = "cidade3"
                        }
                    }

                });
                context.SaveChanges();
            }
        }

        [Fact]
        public void GetAll()
        {
            InitializeDataBase();

            // Use a clean instance of the context to run the test
            using (var context = new AndreTurismAPPHotelServicesContext(options))
            {
                HotelsController hotelsController = new HotelsController(context);
                IEnumerable<Hotel> hotel = hotelsController.GetHotel().Result.Value;

                Assert.Equal(3, hotel.Count());
            }
        }

        [Fact]
        public void GetbyId()
        {
            InitializeDataBase();

            // Use a clean instance of the context to run the test
            using (var context = new AndreTurismAPPHotelServicesContext(options))
            {
                int clientId = 2;
                HotelsController hotelsController = new HotelsController(context);
                Hotel hotel = hotelsController.GetHotel(clientId).Result.Value;
                Assert.Equal(2, hotel.Id);
            }
        }

        [Fact]
        public void Create()
        {
            InitializeDataBase();

            Hotel hotel = new Hotel()
            {
                Id = 4,
                Name = "chateau",
                Cost = 1,
                RegisterDate = "04-04-2000",
                Address = new()
                {
                    CEP = "11111111",
                    City = new()
                    {
                        Description = "cidade1"
                    },
                    
                }
            };

            // Use a clean instance of the context to run the test
            using (var context = new AndreTurismAPPHotelServicesContext(options))
            {
                HotelsController hotelsController = new HotelsController(context);
                Hotel ad = hotelsController.PostHotel(hotel).Result.Value;
                Assert.Equal("chateau", ad.Name);
            }
        }

        [Fact]
        public void Update()
        {
            InitializeDataBase();

            Hotel hotel = new()
            {
                Id = 2,
                Name = "noble",
                Cost = 1,
                RegisterDate = "04-04-2000",
                Address = new()
                {
                    CEP = "11111111",
                    City = new()
                    {
                        Description = "cidade1"
                    }
                }
            };

            // Use a clean instance of the context to run the test
            using (var context = new AndreTurismAPPHotelServicesContext(options))
            {
                HotelsController hotelsController = new HotelsController(context);
                Hotel ad = hotelsController.PutHotel(2, hotel).Result.Value;
                Assert.Equal("noble", ad.Name);
            }
        }

        [Fact]
        public void Delete()
        {
            InitializeDataBase();

            // Use a clean instance of the context to run the test
            using (var context = new AndreTurismAPPHotelServicesContext(options))
            {
                HotelsController hotelsController = new HotelsController(context);
                var customer = hotelsController.DeleteHotel(2).Result.Value;
                Assert.Equal(1, customer);
            }
        }
    }
}
