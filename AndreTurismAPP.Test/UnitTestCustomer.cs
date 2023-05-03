using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AndreTurismAPP.CustomerServices.Controllers;
using AndreTurismAPP.CustomerServices.Data;
using Microsoft.EntityFrameworkCore;
using Models;

namespace AndreTurismAPP.Test
{
    public class UnitTestCustomer
    {
        private DbContextOptions<AndreTurismAPPCustomerServicesContext> options;

        private void InitializeDataBase()
        {
            // Create a Temporary Database
            options = new DbContextOptionsBuilder<AndreTurismAPPCustomerServicesContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            // Insert data into the database using one instance of the context
            using (var context = new AndreTurismAPPCustomerServicesContext(options))
            {
                context.Customer.Add(new Customer() 
                { 
                    Id = 1, 
                    Name="nome1",
                    Phone="1",
                    RegisterDate="04-04-2000",
                    Address= new()
                    {
                        CEP="11111111",
                        City= new()
                        {
                            Description="cidade1"
                        }
                    }

                });

                context.Customer.Add(new Customer()
                {
                    Id = 2,
                    Name = "nome2",
                    Phone = "2",
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

                context.Customer.Add(new Customer()
                {
                    Id = 3,
                    Name = "nome3",
                    Phone = "3",
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
            using (var context = new AndreTurismAPPCustomerServicesContext(options))
            {
                CustomersController customerController = new CustomersController(context);
                IEnumerable<Customer> customer = customerController.GetCustomer().Result.Value;

                Assert.Equal(3, customer.Count());
            }
        }

        [Fact]
        public void GetbyId()
        {
            InitializeDataBase();

            // Use a clean instance of the context to run the test
            using (var context = new AndreTurismAPPCustomerServicesContext(options))
            {
                int clientId = 2;
                CustomersController customerController = new CustomersController(context);
                Customer customer = customerController.GetCustomer(clientId).Result.Value;
                Assert.Equal(2, customer.Id);
            }
        }

        [Fact]
        public void Create()
        {
            InitializeDataBase();

            Customer customer = new()
            {
                Name="primo",
                Phone="190",
                Address=new Address 
                {
                    CEP="14804064",
                    RegisterDate="04-04-2000"
                },
                RegisterDate="04-04-2000"
            };

            // Use a clean instance of the context to run the test
            using (var context = new AndreTurismAPPCustomerServicesContext(options))
            {
                CustomersController customersController = new CustomersController(context);
                Customer ad = customersController.PostCustomer(customer).Result.Value;
                Assert.Equal("primo", ad.Name);
            }
        }

        [Fact]
        public void Update()
        {
            InitializeDataBase();

            Customer customer = new()
            {
                Id=2,
                Name = "shaolin matador de porco",
                Phone = "190",
                Address = new Address
                {
                    CEP = "14804064",
                    RegisterDate = "04-04-2000"
                },
                RegisterDate = "04-04-2000"
            };

            // Use a clean instance of the context to run the test
            using (var context = new AndreTurismAPPCustomerServicesContext(options))
            {
                CustomersController customersController = new CustomersController(context);
                Customer ad = customersController.PutCustomer(2, customer).Result.Value;
                Assert.Equal("shaolin matador de porco", ad.Name);
            }
        }

        [Fact]
        public void Delete()
        {
            InitializeDataBase();

            // Use a clean instance of the context to run the test
            using (var context = new AndreTurismAPPCustomerServicesContext(options))
            {
                CustomersController customersController = new CustomersController(context);
                Customer customer = customersController.DeleteCustomer(2).Result.Value;
                Assert.Null(customer);
            }
        }
    }
}
