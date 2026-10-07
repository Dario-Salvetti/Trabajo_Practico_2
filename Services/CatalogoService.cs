using Microsoft.Data.Sqlite;
using TP2.DTOs;
using TP2.Models;
using Utils;
namespace TP2.Services;

public class CatalogoService
{
    private readonly UtilsDB _utilsDB;
    public void CrearCatalogo (CrearCatalogoDTO c)
    {
        using var conexion = _utilsDB.CrearConexion();
        using var comando = conexion.CreateCommand();

        comando.CommandText = @"
            
            INSERT INTO Catalogo (CatalogoNombre)
            SELECT $catalogo
            WHERE NOT EXISTS (
                SELECT 1 
                FROM Catalogo 
                WHERE LOWER(CatalogoNombre) = LOWER($catalogo)
            );
            ";
        comando.Parameters.AddWithValue("$catalogo", c.CatalogoNombre);

        comando.ExecuteNonQuery();
    }

    public CatalogoDTO EnCatalogoPorId(int id)
    {
        using var conexion = _utilsDB.CrearConexion();
        using var comando = conexion.CreateCommand();

        comando.CommandText = "SELECT Id, CatalogoNombre FROM Catalogo WHERE Id = $id;";
        comando.Parameters.AddWithValue("$id", id);

        using var leer = comando.ExecuteReader();

        if (leer.Read())
        {
            string nombreCatalogo = leer.GetString(1);

            return new CatalogoDTO
            {
                IdCatalogo = id,
                CatalogoNombre = nombreCatalogo,
                Productos = ObtenerProductosPorCatalogo(id)
            };
        };
        return null;
    }

    public List<CatalogoDTO> EnTodosCatalogos()
    {
        List<CatalogoDTO> catalogos = new List<CatalogoDTO>();

        using var conexion = _utilsDB.CrearConexion();
        using var comando = conexion.CreateCommand();

        comando.CommandText = "SELECT Id FROM Catalogo;";
        using var res = comando.ExecuteReader();

        while (res.Read())
        {
            int id = res.GetInt32(0);
            catalogos.Add(EnCatalogoPorId(id));
        }

        return catalogos;
    }

    public void BorrarCatalogo(int id)
    {
        using var conexion = _utilsDB.CrearConexion();
        using var comando = conexion.CreateCommand();

        comando.CommandText = "DELETE FROM Catalogo WHERE Id = $id;";
        comando.Parameters.AddWithValue("$id",id);
        comando.ExecuteNonQuery();
    }
    private List<Producto> ObtenerProductosPorCatalogo(int idCatalogo)//antes era GetAllProductos
    {
        List<Producto> productos = new List<Producto>();
        using var conexion = _utilsDB.CrearConexion();
        using var comando = conexion.CreateCommand();

        comando.CommandText = "SELECT Id, Nombre, Stock, Precio, IdCatalogo FROM Productos WHERE IdCatalogo = $idc;";
        comando.Parameters.AddWithValue("$idc", idCatalogo);

        using var res = comando.ExecuteReader();
        while (res.Read())
        {
            productos.Add(new Producto
            {
                Id = res.GetInt32(0),
                Nombre = res.GetString(1),
                Stock = res.GetInt32(2),
                Precio = res.GetInt32(3)
            });
        }

        return productos;
    }
}


