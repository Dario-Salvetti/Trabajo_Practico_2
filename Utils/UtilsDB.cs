using Microsoft.Data.Sqlite;
namespace Utils

{
    public class UtilsDB
    {
        public SqliteConnection CrearConexion()
        {
            var conexion = new SqliteConnection("Data Source=BasesDatos/BDProductos.db");
            conexion.Open();
            return conexion;
        }
    }
}