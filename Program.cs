using Microsoft.EntityFrameworkCore;
using SampleCRUD.Data.Context;
using System.Configuration;
using System.Data;
using System.Data.Repository;
using System.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Build the configuration
var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();

//builder.Services.AddSingleton<IDbConnection>(c => new SqlConnection(configuration.GetConnectionString("SimpleCRUDConnectionString")));

builder.Services.AddDbContext<SimpleCRUDDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("SimpleCRUDConnectionString")));

// Create and configure your IDbConnection for Dapper
builder.Services.AddScoped<IDbConnection>(c =>
{
    var connectionString = configuration.GetConnectionString("SimpleCRUDConnectionString");
    return new SqlConnection(connectionString);
});
builder.Services.AddScoped<DeviceRepository>();

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
