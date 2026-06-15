using TP2.Models;
namespace TP2.DTOs;

public class CatalogoDTO
{
    public string Tipo {get; set;} = "";
    public List<Producto> Prods {get; set;}
}

public class CrearCatalogoDTO
{
    public string Tipo {get; set;} = "";
    
}
