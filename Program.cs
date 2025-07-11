using Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();      // For minimal APIs
builder.Services.AddSwaggerGen();                // For Swagger UI

// Register ApplicationDBContext using SQL Server
builder.Services.AddDbContext<ApplicationDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();              // Enable middleware to serve swagger.json
    app.UseSwaggerUI();           // Enable Swagger UI
}

app.UseHttpsRedirection();

app.Run();
