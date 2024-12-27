using Application.Interfaces;
using Application.Services;
using Domain.Interfaces;
using infrastructure.Data.Repositories;
using Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("LabDevConnection");

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options => {
    options.AddPolicy("AllowAngularApp", builder =>
            builder.WithOrigins("http://localhost:4200")
                   .AllowAnyMethod()
                   .AllowAnyHeader()
                   .AllowCredentials());
});

builder.Services.AddScoped<ICatProductosRepository>(provider => new CatProductosRepository(connectionString));
builder.Services.AddScoped<ITblClientesRepository>(provider => new TblClientesRepository(connectionString));
builder.Services.AddScoped<ICatTipoClienteRepository>(provider => new CatTipoClienteRepository(connectionString));
builder.Services.AddScoped<IFacturasRepository>(provider => new FacturasRepository(connectionString));
builder.Services.AddScoped<ICatProductosService, CatProductosService>();
builder.Services.AddScoped<ICatTipoClienteService, CatTipoClienteService>();
builder.Services.AddScoped<ITblClientesService, TblClientesService>();
builder.Services.AddScoped<IFacturasService, FacturaService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAngularApp");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
