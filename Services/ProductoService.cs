using Microsoft.Data.Sqlite;
using TP2.DTOs;
using Utils;
namespace TP2.Services;

public class ProductoService
{    public void CrearProd(ProductoDTO p)
    {
        using var conexion = new UtilsDB().CrearConexion();
        using var comando = conexion.CreateCommand();

        comando.CommandText = "INSERT INTO Productos (Nombre, Stock, Precio, IdCatalogo) VALUES ($n, $s, $p, $idc);";
        
        comando.Parameters.AddWithValue("$n", p.Marca+" "+p.Nombre+" "+p.Presentacion);
        comando.Parameters.AddWithValue("$s", p.Stock);
        comando.Parameters.AddWithValue("$p", p.Precio);
        comando.Parameters.AddWithValue("$idc", p.IdCatalogo);
        comando.ExecuteNonQuery();
    }

    public ProductoIndividualDTO CambiarPorId(ProductoCambioDTO x, int id)
    {
        using var conexion = new UtilsDB().CrearConexion();
        using var comando = conexion.CreateCommand();

        if (x.Precio > 0)
        {
            comando.Parameters.Clear();
            comando.CommandText = "UPDATE Productos SET Precio = $precio WHERE Id = $id;";

            comando.Parameters.AddWithValue("$precio", x.Precio);
            comando.Parameters.AddWithValue("$id", id);

            comando.ExecuteNonQuery();
        }
        
        if (x.Stock != 0)
        {
            comando.Parameters.Clear();
            
            comando.CommandText = "UPDATE Productos SET Stock = Stock - $stock WHERE Id = $id AND Stock >= $stock;";
            comando.Parameters.AddWithValue("$stock", x.Stock);
            comando.Parameters.AddWithValue("$id", id);

            comando.ExecuteNonQuery();
        }

        if (x.IdCatalogo > 0)
        {
            comando.Parameters.Clear();
            comando.CommandText = "UPDATE Productos SET IdCatalogo = $cata WHERE Id = $id;";

            comando.Parameters.AddWithValue("$cata", x.IdCatalogo);
            comando.Parameters.AddWithValue("$id", id);

            comando.ExecuteNonQuery();
        }

        return _auxiliarservice.ObtenerPorId(id);
    }

    public void BorrarPorId (int id)
    {
        using var conexion = new UtilsDB().CrearConexion();
        using var comando = conexion.CreateCommand();

        comando.CommandText = "DELETE FROM Productos WHERE Id = $id;";
        comando.Parameters.AddWithValue("$id",id);
        comando.ExecuteNonQuery();
    }
    public ProductoIndividualDTO ObtenerPorId(int id)
    {
        return _auxiliarservice.ObtenerPorId(id);
    }

    private readonly AuxiliarService _auxiliarservice;

    public ProductoService(AuxiliarService auxiliarservice)
    {
         _auxiliarservice = auxiliarservice;
    }

    
}
