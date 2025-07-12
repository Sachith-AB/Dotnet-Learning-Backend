using Data;
using Dotnet_backend.Interfaces;
using Dotnet_backend.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();      // For minimal APIs
builder.Services.AddSwaggerGen();                // For Swagger UI

// Register ApplicationDBContext using SQL Server
builder.Services.AddDbContext<ApplicationDBContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

builder.Services.AddScoped<IStockRepository, StockRepository>(); // Register StockRepository
builder.Services.AddScoped<ICommentRepository, CommentRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();              // Enable middleware to serve swagger.json
    app.UseSwaggerUI();           // Enable Swagger UI
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
