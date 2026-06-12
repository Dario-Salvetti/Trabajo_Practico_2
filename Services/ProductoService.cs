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

        comando.CommandText = "INSERT INTO Productos (Nombre, Stock, Precio) VALUES ($n, $s, $p);";
        
        comando.Parameters.AddWithValue("$n", p.Marca+" "+p.Nombre+" "+p.Presentacion);
        comando.Parameters.AddWithValue("$s", p.Stock);
        comando.Parameters.AddWithValue("$p", p.Precio);

        comando.ExecuteNonQuery();
    }

    public void CambiarPrecioXId(int id, int nprecio)
    {
        using var conexion = new SqliteConnection($"Data Source={_rutaBaseDedatos}");
        conexion.Open();
        using var comando = conexion.CreateCommand();

        comando.CommandText = "UPDATE Productos SET Precio = $precio WHERE Id = $id;";

        comando.Parameters.AddWithValue("$precio", nprecio);
        comando.Parameters.AddWithValue("$id", id);

        comando.ExecuteNonQuery();
    }

    public void CambiarStockXId(int id, int nstock)
    {
        using var conexion = new SqliteConnection($"Data Source={_rutaBaseDedatos}");
        conexion.Open();
        using var comando = conexion.CreateCommand();

        comando.CommandText = "UPDATE Productos SET Stock = (Stock - $stock) WHERE Id = $id;";

        comando.Parameters.AddWithValue("$stock", nstock);
        comando.Parameters.AddWithValue("$id", id);

        comando.ExecuteNonQuery();
    }

    public void CambiarCategoriaXId(int id, int ncateg)
    {
        using var conexion = new SqliteConnection($"Data Source={_rutaBaseDedatos}");
        conexion.Open();
        using var comando = conexion.CreateCommand();

        comando.CommandText = "UPDATE Productos SET IdTipo = $cate WHERE Id = $id;";

        comando.Parameters.AddWithValue("$cate", ncateg);
        comando.Parameters.AddWithValue("$id", id);

        comando.ExecuteNonQuery();
    }

    

    //MODIFICAR LUEGO LO QUE ESTE DEBAJO DE ESTA LINEA
    public List<Producto> GetAllProductos()
    {
        List<Producto> prod = new List<Producto>();

        using var conexion = new SqliteConnection(_rutaBaseDedatos);

        conexion.Open();

        using var comando = conexion.CreateCommand();
        comando.CommandText = "SELECT Id, IdTipo, Nombre, Stock, Precio FROM Productos;";
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
    public Producto ObtenerXId(int id)
    {
        using var conexion = new SqliteConnection($"Data Source={_rutaBaseDedatos}");
        conexion.Open();
        using var comando = conexion.CreateCommand();

        comando.CommandText = "SELECT Id, IdTipo, Nombre, Stock, Precio FROM Productos WHERE Id = $id;";

        comando.Parameters.AddWithValue("$id", id);

        using var leer = comando.ExecuteReader();

        if (leer.Read())
        {
            return new Producto
            {
                Id = leer.GetInt32(0),
                Nombre = leer.GetString(1),
                Precio = leer.GetInt32(2),
                Stock = leer.GetInt32(3),
            };
        }

        return null;
    }

    //
}