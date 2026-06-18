using Microsoft.Data.Sqlite;
using TP2.Models;
using TP2.DTOs;
using TP2.Services;

namespace TP2.Services;

public class ProductoService
{
    private readonly string _rutaBaseDedatos= "BasesDatos/BDProductos.db";
    public void CrearProd(ProductoDTO p, int idc)
    {
        using var conexion = new SqliteConnection($"Data Source={_rutaBaseDedatos}");
        conexion.Open();
        using var comando = conexion.CreateCommand();

        comando.CommandText = "INSERT INTO Productos (Nombre, Stock, Precio, IdCatalogo) VALUES ($n, $s, $p, $idc);";
        
        comando.Parameters.AddWithValue("$n", p.Marca+" "+p.Nombre+" "+p.Presentacion);
        comando.Parameters.AddWithValue("$s", p.Stock);
        comando.Parameters.AddWithValue("$p", p.Precio);
        comando.Parameters.AddWithValue("$idc", idc);
        comando.ExecuteNonQuery();
    }

    public ProductoIndividualDTO CambiarXId(ProductoCambioDTO x)
    {
        using var conexion = new SqliteConnection($"Data Source={_rutaBaseDedatos}");
        conexion.Open();
        using var comando = conexion.CreateCommand();

        if (x.Precio > 0)
        {
            comando.Parameters.Clear();
            comando.CommandText = "UPDATE Productos SET Precio = $precio WHERE Id = $id;";

            comando.Parameters.AddWithValue("$precio", x.Precio);
            comando.Parameters.AddWithValue("$id", x.Id);

            comando.ExecuteNonQuery();
        }
        
        if (x.Stock != 0)
        {
            comando.Parameters.Clear();
            
            comando.CommandText = "UPDATE Productos SET Stock = Stock - $stock WHERE Id = $id AND Stock >= $stock;";
            comando.Parameters.AddWithValue("$stock", x.Stock);
            comando.Parameters.AddWithValue("$id", x.Id);

            comando.ExecuteNonQuery();
        }

        if (x.CatalogoNombre > 0)
        {
            comando.Parameters.Clear();
            comando.CommandText = "UPDATE Productos SET IdCatalogo = $cata WHERE Id = $id;";

            comando.Parameters.AddWithValue("$cata", x.CatalogoNombre);
            comando.Parameters.AddWithValue("$id", x.Id);

            comando.ExecuteNonQuery();
        }

        return _auxiliarservice.ObtenerXId(x.Id);
    }

    public void BorrarXId (int id)
    {
        using var conexion = new SqliteConnection($"Data Source={_rutaBaseDedatos}");
        conexion.Open();
        using var comando = conexion.CreateCommand();

        comando.CommandText = "DELETE FROM Productos WHERE Id = $id;";
        comando.Parameters.AddWithValue("$id",id);
        comando.ExecuteNonQuery();
    }
    public ProductoIndividualDTO ObtenerXId(int id)
    {
        return _auxiliarservice.ObtenerXId(id);
    }

    private readonly AuxiliarService _auxiliarservice;

    public ProductoService(AuxiliarService auxiliarservice)
    {
         _auxiliarservice = auxiliarservice;
    }

    
}
