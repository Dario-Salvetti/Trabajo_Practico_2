using Microsoft.Data.Sqlite;
using TP2.Models;
using TP2.DTOs;
using TP2.Services;

namespace TP2.Services;

public class ProductoService  : IProductoService
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
            comando.CommandText = "UPDATE Productos SET Stock = (Stock - $stock) WHERE Id = $id;";

            comando.Parameters.AddWithValue("$stock", x.Stock);
            comando.Parameters.AddWithValue("$id", x.Id);

            comando.ExecuteNonQuery();
        }

        if (x.Categoria > 0)
        {
            comando.Parameters.Clear();
            comando.CommandText = "UPDATE Productos SET IdTipo = $cate WHERE Id = $id;";

            comando.Parameters.AddWithValue("$cate", x.Categoria);
            comando.Parameters.AddWithValue("$id", x.Id);

            comando.ExecuteNonQuery();
        }

        return ObtenerXId(x.Id);
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
                Categoria = _catalogoService.Value.ObtenerNombreXid(leer.GetInt32(4))
            };
        }

        return null;
    }

    private readonly Lazy<ICatalogoService> _catalogoService;

    public ProductoService(Lazy<ICatalogoService> catalogoService)
    {
         _catalogoService = catalogoService;
    }

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


    //
}
public interface IProductoService
{
    void CrearProd(ProductoDTO p);
    ProductoIndividualDTO CambiarXId(ProductoCambioDTO x);
    void BorrarXId (int id);
    ProductoIndividualDTO ObtenerXId(int id);
    List<Producto> GetAllProductos(int idt);
}