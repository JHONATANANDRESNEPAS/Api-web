using AppiWeb.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models; // 🔹 Asegurar importación de OpenAPI

var builder = WebApplication.CreateBuilder(args);

// Variable para la cadena de conexión
var connectionString = builder.Configuration.GetConnectionString("Connection");

// Registramos el servicio de DbContext con SQL Server
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

// Configuración de Swagger con versión válida
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Mi API en Español",
        Version = "3.0.4", // 🔹 Debe coincidir con OpenAPI en el JSON
        Description = "API para gestionar recursos"
    });
});

var app = builder.Build();

// Configuración del pipeline de solicitudes HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Mi API en Español v1");
        c.DocumentTitle = "Documentación API";
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
