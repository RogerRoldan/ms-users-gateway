using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Configura el archivo JSON de Ocelot
builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);

// Agrega Ocelot al contenedor de servicios
builder.Services.AddOcelot(builder.Configuration);

// Configuración de Swagger para Ocelot
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerForOcelot(builder.Configuration);

// Configura CORS para permitir solo solicitudes desde localhost:3000
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", builder =>
    {
        builder.WithOrigins("http://localhost:3000")
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

// Construye la aplicación
var app = builder.Build();

// Usa el middleware de CORS con la política especificada
app.UseCors("AllowSpecificOrigin");

// Configura la UI de Swagger para Ocelot
app.UseSwaggerForOcelotUI(options =>
{
    options.PathToSwaggerGenerator = "/swagger/docs";
});

// Usa Ocelot Middleware
app.UseOcelot().Wait();

// Inicia la aplicación
app.Run();
