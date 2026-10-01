using Microsoft.Data.Sqlite;
namespace Utils

{
    public static class UtilsDB
    {
        public static SqliteConnection CrearConexion()
        {
            var conexion = new SqliteConnection("BasesDatos/BDProductos.db");
            conexion.Open();
            return conexion;
        }
    }
}