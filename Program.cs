using Microsoft.EntityFrameworkCore;
using SimpleEntry.Models;
using SimpleEntry.Rules;
using SimpleEntry.Services;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure Entity Framework Core to use SQL Server.
builder.Services.AddDbContext<SimpleEntryContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("ProductionDB")));

// Add the RegistroAccionRepository service to the container.
builder.Services.AddScoped<RegistroAccionRepository>();
builder.Services.AddScoped<AuthRule>();

// Add JSON options to handle object cycles.
builder.Services.AddControllers().AddJsonOptions(opt =>
{
    opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();

