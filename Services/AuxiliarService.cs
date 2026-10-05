using Microsoft.Data.Sqlite;
using TP2.Models;
using TP2.DTOs;
using Utils;
namespace TP2.Services;

public class AuxiliarService
{    public List<Producto> GetAllProductos(int idc)
    {
        List<Producto> prod = new List<Producto>();
        using var conexion = new UtilsDB().CrearConexion();
        using var comando = conexion.CreateCommand();
        comando.CommandText = "SELECT Id, Nombre, Stock, Precio, IdCatalogo FROM Productos WHERE IdCatalogo = $idc;";

        comando.Parameters.AddWithValue("$idc", idc);
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
    public ProductoIndividualDTO ObtenerPorId(int id)
    {
        using var conexion = new UtilsDB().CrearConexion();
        using var comando = conexion.CreateCommand();

        comando.CommandText = "SELECT Id, Nombre, Precio, Stock, IdCatalogo FROM Productos WHERE Id = $id;";

        comando.Parameters.AddWithValue("$id", id);

        using var leer = comando.ExecuteReader();

        if (leer.Read())
        {
            return new ProductoIndividualDTO
            {
                Nombre = leer.GetString(1),
                Precio = leer.GetInt32(2),
                Stock = leer.GetInt32(3),
                CatalogoNombre = ObtenerNombrePorId(leer.GetInt32(4))
            };
        }

        return null;
    }

    public string ObtenerNombrePorId(int id)
    {
        using var conexion = new UtilsDB().CrearConexion();
        using var comando = conexion.CreateCommand();

        comando.CommandText = "SELECT Id, CatalogoNombre FROM Catalogo WHERE Id = $id;";
        comando.Parameters.AddWithValue("$id", id);

        using var leer = comando.ExecuteReader();

        if (leer.Read())
        {
            return leer.GetString(1);
        };
        return "no existe";
    }

    public CatalogoDTO EnCatalogoPorId(int id)
    {
        return new CatalogoDTO()
        {
            IdCatalogo = id,
            CatalogoNombre = ObtenerNombrePorId(id),
            Productos = GetAllProductos(id)
        };
    }

}