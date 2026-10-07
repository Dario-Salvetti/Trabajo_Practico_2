using Microsoft.Data.Sqlite;
using TP2.DTOs;
using Utils;
namespace TP2.Services;

public class ProductoService
{
    private readonly UtilsDB _utilsDB;
    public ProductoService(UtilsDB utilsDB)
    {
        _utilsDB = utilsDB;
    }
    public void CrearProd(ProductoDTO p)
    {
        using var conexion = _utilsDB.CrearConexion();
        using var comando = conexion.CreateCommand();

        comando.CommandText = "INSERT INTO Productos (Nombre, Stock, Precio, IdCatalogo) VALUES ($n, $s, $p, $idc);";
        
        comando.Parameters.AddWithValue("$n", p.Marca+" "+p.Nombre+" "+p.Presentacion);
        comando.Parameters.AddWithValue("$s", p.Stock);
        comando.Parameters.AddWithValue("$p", p.Precio);
        comando.Parameters.AddWithValue("$idc", p.IdCatalogo);
        comando.ExecuteNonQuery();
    }

    public ProductoIndividualDTO CambiarPorId(ProductoCambioDTO x, int id)//todos los if en un update (como lo queria hacer desde un inicio)
    {
        using var conexion = new UtilsDB().CrearConexion();
        using var comando = conexion.CreateCommand();

        comando.CommandText = @"
            UPDATE Productos 
            SET Precio = CASE WHEN $precio > 0 THEN $precio ELSE Precio END,
                Stock = CASE WHEN $stock != 0 AND Stock >= $stock THEN Stock - $stock ELSE Stock END,
                IdCatalogo = CASE WHEN $cata > 0 THEN $cata ELSE IdCatalogo END
            WHERE Id = $id;";

        comando.Parameters.AddWithValue("$precio", x.Precio);
        comando.Parameters.AddWithValue("$stock", x.Stock);
        comando.Parameters.AddWithValue("$cata", x.IdCatalogo);
        comando.Parameters.AddWithValue("$id", id);

        comando.ExecuteNonQuery();

        return ObtenerPorId(id);
    }

    public ProductoIndividualDTO ObtenerPorId(int id)//lo del left join que no conociamos que nos permite mirar dos tablas en la misma consulta
    {
        using var conexion = _utilsDB.CrearConexion();
        using var comando = conexion.CreateCommand();

        comando.CommandText = @"
            SELECT p.Nombre, p.Precio, p.Stock, COALESCE(c.CatalogoNombre, 'no existe')
            FROM Productos p
            LEFT JOIN Catalogo c ON p.IdCatalogo = c.Id
            WHERE p.Id = $id;";

        comando.Parameters.AddWithValue("$id", id);

        using var leer = comando.ExecuteReader();

        if (leer.Read())
        {
            return new ProductoIndividualDTO
            {
                Nombre = leer.GetString(0),
                Precio = leer.GetInt32(1),
                Stock = leer.GetInt32(2),
                CatalogoNombre = leer.GetString(3)
            };
        }

        return null;
    }

    public void BorrarPorId (int id)
    {
        using var conexion = _utilsDB.CrearConexion();
        using var comando = conexion.CreateCommand();

        comando.CommandText = "DELETE FROM Productos WHERE Id = $id;";
        comando.Parameters.AddWithValue("$id",id);
        comando.ExecuteNonQuery();
    }
    
}
