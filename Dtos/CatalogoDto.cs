using TP2.Models;
namespace TP2.DTOs;

public class CatalogoDTO
{
    public int IdCatalogo {get; set;}
    public string CatalogoNombre {get; set;}
    public List<Producto> Productos {get; set;}
}

public class CrearCatalogoDTO
{
    public string CatalogoNombre {get; set;} = "";
    
}
