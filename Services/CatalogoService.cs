using Microsoft.Data.Sqlite;
using TP2.Models;
using TP2.DTOs;

namespace TP2.Services;

public class CatalogoService
{
    private readonly string _rutaBaseDedatos= "BasesDatos/BDProductos.db";
    public void NuevaCat (CatalogoDTO c)
    {
        using var conexion = new SqliteConnection($"Data Source={_rutaBaseDedatos}");
        conexion.Open();
        using var comando = conexion.CreateCommand();

        comando.CommandText = @"
            INSERT INTO Tipos (Tipo) VALUES ($tipo);
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

        comando.CommandText = "SELECT Id, Tipo FROM Tipos";
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

        comando.CommandText = "DELETE FROM Tipos WHERE IdTipo = $id";
        comando.Parameters.AddWithValue("$id",id);
        comando.ExecuteNonQuery();
    }
}