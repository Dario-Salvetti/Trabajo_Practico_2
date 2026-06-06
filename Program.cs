using Microsoft.Data.Sqlite;
string rutaBaseDedatos= "BasesDatos/BDhitProductos.db";
using var conexion = new SqliteConnection($"Data Source={rutaBaseDedatos}");
conexion.Open();
Console.WriteLine("Conexion abierta");
using var crearTabla = conexion.CreateCommand();
 crearTabla.CommandText =@"CREATE TABLE IF NOT EXISTS Catalogo
    IdTipo INTEGER PRIMARY KEY AUTOINCREMENT,
    Tipo TEXT NOT NULL
)";
crearTabla.ExecuteNonQuery();
crearTabla.CommandText =@"CREATE TABLE IF NOT EXISTS Producto
    IdProducto INTEGER PRIMARY KEY AUTOINCREMENT,
    Nombre TEXT NOT NULL,
    Precio REAL NOT NULL,
    IdTipo INTEGER NOT NULL,
    FOREIGN KEY (IdTipo) REFERENCES Catalogo(IdTipo)
)";

crearTabla.ExecuteNonQuery();
Console.WriteLine("Tablas creadas");



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
