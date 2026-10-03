using Microsoft.Data.Sqlite;
using TP2.DTOs;
using Utils;
namespace TP2.Services;

public class CatalogoService
{
    private SqliteConnection ObtenerConexion()
    {
        var conexion = new UtilsDB().CrearConexion();
        return conexion;
    }
    public void NuevaCat (CrearCatalogoDTO c)
    {
        using var conexion = ObtenerConexion();
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

    public CatalogoDTO EnCatalogoXId(int id)
    {
        return _auxiliarservice.EnCatalogoXId(id);
    }

    public List<CatalogoDTO> EnTodosCatalogos()
    {
        List<CatalogoDTO> catalogos = new List<CatalogoDTO>();

        using var conexion = ObtenerConexion();
        using var comando = conexion.CreateCommand();

        comando.CommandText = "SELECT Id FROM Catalogo;";
        using var res = comando.ExecuteReader();

        while (res.Read())
        {
            catalogos.Add(_auxiliarservice.EnCatalogoXId(res.GetInt32(0)));
        }

        return catalogos;
    }

    public void BorrarCatalogos(int id)
    {
        using var conexion = ObtenerConexion();
        using var comando = conexion.CreateCommand();

        comando.CommandText = "DELETE FROM Catalogo WHERE Id = $id;";
        comando.Parameters.AddWithValue("$id",id);
        comando.ExecuteNonQuery();
    }

    private readonly AuxiliarService _auxiliarservice;

    public CatalogoService(AuxiliarService auxiliarservice)
    {
         _auxiliarservice = auxiliarservice;
    }
}


