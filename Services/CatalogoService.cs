using Microsoft.Data.Sqlite;
using TP2.Models;
using TP2.DTOs;
using TP2.Services;

namespace TP2.Services;

public class CatalogoService
{
    private readonly string _rutaBaseDedatos= "BasesDatos/BDProductos.db";
    public void NuevaCat (CrearCatalogoDTO c)
    {
        using var conexion = new SqliteConnection($"Data Source={_rutaBaseDedatos}");
        conexion.Open();
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

    public string ObtenerNombreXid(int id)
    {
        return _auxiliarservice.ObtenerNombreXid(id);
    }

    public CatalogoDTO EnCatalogoXId(int id)
    {
        return _auxiliarservice.EnCatalogoXId(id);
    }

    public List<Catalogo> GetCatalogos()
    {
        List<Catalogo> catalogos = new List<Catalogo>();

        using var conexion = new SqliteConnection($"Data Source={_rutaBaseDedatos}");
        conexion.Open();
        using var comando = conexion.CreateCommand();

        comando.CommandText = "SELECT Id, CatalogoNombre FROM Catalogo;";
        using var res = comando.ExecuteReader();

        while (res.Read())
        {
            catalogos.Add(new Catalogo
            {
                IdCatalogo = res.GetInt32(0),
                CatalogoNombre = res.GetString(1)

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
            catalogos.Add(_auxiliarservice.EnCatalogoXId(res.GetInt32(0)));
        }

        return catalogos;
    }

    private readonly AuxiliarService _auxiliarservice;

    public CatalogoService(AuxiliarService auxiliarservice)
    {
         _auxiliarservice = auxiliarservice;
    }
}


