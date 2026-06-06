using Microsoft.Data.Sqlite;
using TP2.Models;
using TP2.DTOs;

namespace TP2.Services;

public class ProductoService
{
    private readonly string _rutaBaseDedatos= "BasesDatos/BDProductos.db";
    public void CrearProd(ProductoDTO p)
    {
        using var conexion = new SqliteConnection($"Data Source={_rutaBaseDedatos}");
        conexion.Open();
        using var comando = conexion.CreateCommand();

        comando.CommandText = @"
            INSERT INTO Productos (Nombre, Stock, Precio) VALUES ($n, $s, $p);";
        
        comando.Parameters.AddWithValue("$n", p.Marca+" "+p.Nombre+" "+p.Presentacion);
        comando.Parameters.AddWithValue("$s", p.Stock);
        comando.Parameters.AddWithValue("$p", p.Precio);

        comando.ExecuteNonQuery();
    }
    
    public List<Producto> GetAllProductos()
    {
        List<Producto> prod = new List<Producto>();

        using var conexion = new SqliteConnection(_rutaBaseDedatos);

        conexion.Open();

        using var comando = conexion.CreateCommand();
        comando.CommandText = "SELECT Id, Nombre, Stock, Precio FROM Productos";
        using var res = comando.ExecuteReader();
        
        while (res.Read())
        {
            prod.Add(new Producto
            {
                Id = res.GetInt32(0),
                Nombre = res.GetString(1),
                Stock = res.GetInt32(2),
                Precio = res.GetInt32(3)

            });
        }

        return prod;
    }
}