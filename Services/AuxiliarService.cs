using Microsoft.Data.Sqlite;
using TP2.Models;
using TP2.DTOs;

namespace TP2.Services;

public class AuxiliarService
{
    private readonly string _rutaBaseDedatos= "BasesDatos/BDProductos.db";

    public List<Producto> GetAllProductos(int idt)
    {
        List<Producto> prod = new List<Producto>();

        using var conexion = new SqliteConnection(_rutaBaseDedatos);

        conexion.Open();

        using var comando = conexion.CreateCommand();
        comando.CommandText = "SELECT Id, Nombre, Stock, Precio FROM Productos WHERE IdTipo = $idt;";

        comando.Parameters.AddWithValue("$idt", idt);
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
    public ProductoIndividualDTO ObtenerXId(int id)
    {
        
        using var conexion = new SqliteConnection($"Data Source={_rutaBaseDedatos}");
        conexion.Open();
        using var comando = conexion.CreateCommand();

        comando.CommandText = "SELECT Id, IdTipo, Nombre, Stock, Precio FROM Productos WHERE Id = $id;";

        comando.Parameters.AddWithValue("$id", id);

        using var leer = comando.ExecuteReader();

        if (leer.Read())
        {
            return new ProductoIndividualDTO
            {
                Id = leer.GetInt32(0),
                Nombre = leer.GetString(1),
                Precio = leer.GetInt32(2),
                Stock = leer.GetInt32(3),
                Categoria = ObtenerNombreXid(leer.GetInt32(4))
            };
        }

        return null;
    }

    public string ObtenerNombreXid(int id)
    {
        using var conexion = new SqliteConnection($"Data Source={_rutaBaseDedatos}");
        conexion.Open();
        using var comando = conexion.CreateCommand();

        comando.CommandText = "SELECT Tipo FROM Catalogo WHERE Id = $id;";
        comando.Parameters.AddWithValue("$id", id);

        using var leer = comando.ExecuteReader();

        if (leer.Read())
        {
            return leer.GetString(1);
        };
        return null;
    }

    public CatalogoDTO EnCatalogoXId(int id)
    {
        
        return new CatalogoDTO()
        {
            Tipo = ObtenerNombreXid(id),
            Prods = GetAllProductos(id)
        };
    }

}