using EscuelasPrueba.Core.Application.Interfaces.IRepository;
using EscuelasPrueba.Core.Application.Interfaces.IServices;
using EscuelasPrueba.Core.Application.Services;
using EscuelasPrueba.Infraestructure.Data;
using EscuelasPrueba.Infraestructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<IAlumnoRepository, AlumnoRepository>();
builder.Services.AddScoped<IEscuelaRepository, EscuelaRepository>();
builder.Services.AddScoped<IProfesorRepository, ProfesorRepository>();

builder.Services.AddScoped<IAlumnoService, AlumnoService>();
builder.Services.AddScoped<IEscuelaService, EscuelaService>();
builder.Services.AddScoped<IProfesorService, ProfesorService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<MusicaDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Cadena"));
}
);
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAllOrigins");
app.UseAuthorization();

app.MapControllers();

app.Run();
