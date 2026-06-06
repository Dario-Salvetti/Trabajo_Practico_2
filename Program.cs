using Microsoft.Data.Sqlite;
using TP2.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<CatalogoService>();
builder.Services.AddScoped<ProductoService>();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

string rutaBaseDedatos= "BasesDatos/BDProductos.db";
using var conexion = new SqliteConnection($"Data Source={rutaBaseDedatos}");
conexion.Open();
Console.WriteLine("Conexion abierta");
using var crearTabla = conexion.CreateCommand();
 crearTabla.CommandText =@"CREATE TABLE IF NOT EXISTS Catalogo(
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Tipo TEXT NOT NULL
);";
crearTabla.ExecuteNonQuery();
crearTabla.CommandText =@"CREATE TABLE IF NOT EXISTS Productos(
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Nombre TEXT NOT NULL,
    Precio INTEGER NOT NULL,
    Stock INTEGER NOT NULL,
    IdTipo INTEGER NOT NULL,
    FOREIGN KEY (IdTipo) REFERENCES Catalogo(Id)
);";

crearTabla.ExecuteNonQuery();
Console.WriteLine("Tablas creadas");


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
