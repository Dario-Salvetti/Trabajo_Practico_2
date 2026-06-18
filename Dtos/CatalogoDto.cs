using TP2.Models;
namespace TP2.DTOs;

public class CatalogoDTO
{
    public string CatalogoNombre {get; set;} = "";
    public List<Producto> Productos {get; set;}
}

public class CrearCatalogoDTO
{
    public string CatalogoNombre {get; set;} = "";
    
}
