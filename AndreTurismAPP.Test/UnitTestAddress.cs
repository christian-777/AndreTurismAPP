using AndreTurismAPP.AddressServices.Controllers;
using AndreTurismAPP.AddressServices.Data;
using Microsoft.EntityFrameworkCore;
using Models;

namespace AndreTurismAPP.Test
{
    public class UnitTestAddress
    {
        private DbContextOptions<AndreTurismAPPAddressServicesContext> options;

        private void InitializeDataBase()
        {
            // Create a Temporary Database
            options = new DbContextOptionsBuilder<AndreTurismAPPAddressServicesContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            // Insert data into the database using one instance of the context
            using (var context = new AndreTurismAPPAddressServicesContext(options))
            {
                context.Address.Add(new Address { Id = 1, Street = "Street 1", CEP = "123456789", City = new City() { Id = 1, Description = "City1" } });
                context.Address.Add(new Address { Id = 2, Street = "Street 2", CEP = "987654321", City = new City() { Id = 2, Description = "City2" } });
                context.Address.Add(new Address { Id = 3, Street = "Street 3", CEP = "159647841", City = new City() { Id = 3, Description = "City3" } });
                context.SaveChanges();
            }
        }

        [Fact]
        public void GetAll()
        {
            InitializeDataBase();

            // Use a clean instance of the context to run the test
            using (var context = new AndreTurismAPPAddressServicesContext(options))
            {
                AddressesController addressController = new AddressesController(context);
                IEnumerable<Address> address = addressController.GetAddress().Result.Value;

                Assert.Equal(3, address.Count());
            }
        }

        [Fact]
        public void GetbyId()
        {
            InitializeDataBase();

            // Use a clean instance of the context to run the test
            using (var context = new AndreTurismAPPAddressServicesContext(options))
            {
                int clientId = 2;
                AddressesController addressController = new AddressesController(context);
                Address address = addressController.GetAddress(clientId).Result.Value;
                Assert.Equal(2, address.Id);
            }
        }

        [Fact]
        public void Create()
        {
            InitializeDataBase();

            Address address = new Address()
            {
                Id = 4,
                Street = "Rua 10",
                CEP = "14804300",
                City = new() { Id = 10, Description = "City 10" }
            };

            // Use a clean instance of the context to run the test
            using (var context = new AndreTurismAPPAddressServicesContext(options))
            {
                AddressesController clientController = new AddressesController(context);
                Address ad = clientController.PostAddress(address).Result.Value;
                Assert.Equal("Avenida Alberto Benassi", ad.Street);
            }
        }

        [Fact]
        public void Update()
        {
            InitializeDataBase();

            Address address = new Address()
            {
                Id = 3,
                Street = "Rua 10 Alterada",
                City = new() { Id = 10, Description = "City 10 alterada" }
            };

            // Use a clean instance of the context to run the test
            using (var context = new AndreTurismAPPAddressServicesContext(options))
            {
                AddressesController clientController = new AddressesController(context);
                Address ad = clientController.PutAddress(3, address).Result.Value;
                Assert.Equal("Rua 10 Alterada", ad.Street);
            }
        }

        [Fact]
        public void Delete()
        {
            InitializeDataBase();

            // Use a clean instance of the context to run the test
            using (var context = new AndreTurismAPPAddressServicesContext(options))
            {
                AddressesController addressController = new AddressesController(context);
                Address address = addressController.DeleteAddress(2).Result.Value;
                Assert.Null(address);
            }
        }
    }
}