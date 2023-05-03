using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using AndreTurismAPP.Data;
using AndreTurismAPP.Services;
using AndreTurismAPP.Controllers;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AndreTurismAPPContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AndreTurismAPPContext") ?? throw new InvalidOperationException("Connection string 'AndreTurismAPPContext' not found.")));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

/*builder.Services.AddSingleton<AddressesController>();
builder.Services.AddSingleton<CustomersController>();*/

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
