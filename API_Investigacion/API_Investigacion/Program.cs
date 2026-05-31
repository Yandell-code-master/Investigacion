using Microsoft.EntityFrameworkCore;
using API_Investigacion.Models;
using API_Investigacion.Interfaces;
using API_Investigacion.util;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IMensajeriaService, EmailMessage>();

builder.Services.AddDbContext<DbContextInvestigacion>(
    options => options.UseSqlServer(
        builder.Configuration.GetConnectionString("StringLocal")));

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
