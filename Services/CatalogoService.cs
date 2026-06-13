using Microsoft.Data.Sqlite;
using TP2.Models;
using TP2.DTOs;
using TP2.Services;

namespace TP2.Services;

public class CatalogoService : ICatalogoService
{
    private readonly string _rutaBaseDedatos= "BasesDatos/BDProductos.db";
    public void NuevaCat (CatalogoDTO c)
    {
        using var conexion = new SqliteConnection($"Data Source={_rutaBaseDedatos}");
        conexion.Open();
        using var comando = conexion.CreateCommand();

        comando.CommandText = @"
            INSERT INTO Catalogo (Tipo) VALUES ($tipo);
        ";

        comando.Parameters.AddWithValue("$tipo", c.Tipo);

        comando.ExecuteNonQuery();
    }

    public List<Catalogo> GetCatalogos()
    {
        List<Catalogo> catalogos = new List<Catalogo>();

        using var conexion = new SqliteConnection($"Data Source={_rutaBaseDedatos}");
        conexion.Open();
        using var comando = conexion.CreateCommand();

        comando.CommandText = "SELECT Id, Tipo FROM Catalogo;";
        using var res = comando.ExecuteReader();

        while (res.Read())
        {
            catalogos.Add(new Catalogo
            {
                IdTipo = res.GetInt32(0),
                Tipo = res.GetString(1)

            });
        }

        return catalogos;
    }

    public void BorrarCatalogos(int id)
    {
        using var conexion = new SqliteConnection($"Data Source={_rutaBaseDedatos}");
        conexion.Open();
        using var comando = conexion.CreateCommand();

        comando.CommandText = "DELETE FROM Catalogo WHERE Id = $id;";
        comando.Parameters.AddWithValue("$id",id);
        comando.ExecuteNonQuery();
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
            Prods = _productoService.Value.GetAllProductos(id)
        };
    }

    public List<CatalogoDTO> EnTodosCatalogos()
    {
        List<CatalogoDTO> catalogos = new List<CatalogoDTO>();

        using var conexion = new SqliteConnection($"Data Source={_rutaBaseDedatos}");
        conexion.Open();
        using var comando = conexion.CreateCommand();

        comando.CommandText = "SELECT Id FROM Catalogo;";
        using var res = comando.ExecuteReader();

        while (res.Read())
        {
            catalogos.Add(EnCatalogoXId(res.GetInt32(0)));
        }

        return catalogos;
    }

    private readonly Lazy<IProductoService> _productoService;

    public CatalogoService(Lazy<IProductoService> productoService)
    {
         _productoService = productoService;
    }
}


public interface ICatalogoService
{
    void NuevaCat (CatalogoDTO c);
    List<Catalogo> GetCatalogos();
    void BorrarCatalogos(int id);
    string ObtenerNombreXid(int id);
    CatalogoDTO EnCatalogoXId(int id);
    List<CatalogoDTO> EnTodosCatalogos();
}
