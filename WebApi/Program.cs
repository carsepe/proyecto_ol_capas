using BusinessLogic.Ports;
using BusinessLogic.UseCase;
using Domain.Interface;
using Domain.Model;
using Infraestructure.Mapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.OpenApi.Models;
using Microsoft.Win32;
using Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(typeof(MappingProfile));


builder.Services.AddDbContext<ModelContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();

builder.Services.AddScoped<IPipelineTransformacionDatosRepository, PipelineTransformacionDatosRepository>();
builder.Services.AddScoped<IPipelineTransformacionUseCase, PipelineTransformacionServicio>();

builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IClienteUseCase, ClienteServicio>();

builder.Services.AddScoped<IProductoRepository, ProductoRepository>();
builder.Services.AddScoped<IProductoUseCase, ProductoServicio>();

//Configuración de Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API Prueba Tecnica OL Software",
        Version = "v1",
        Description = "API para manejar cargas de archivos"
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Prueba Tecnica OL Software v1"));
}

//Middleware para servir archivos estáticos
var archivosPath = Path.Combine(Directory.GetCurrentDirectory(), "Archivos");
if (!Directory.Exists(archivosPath))
{
    Directory.CreateDirectory(archivosPath); // Crear la carpeta si no existe
}
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(archivosPath),
    RequestPath = "/Archivos"
});

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
