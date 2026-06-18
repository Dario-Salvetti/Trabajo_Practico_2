namespace TP2.DTOs;

public class ProductoDTO
{
    public string Marca {get; set;} = "";
    public string Nombre {get; set;} = "";
    public string Presentacion {get; set;} = "";
    public int Stock {get; set;} = 0;
    public int Precio {get; set;} = 0;
}

public class ProductoIndividualDTO
{
    public int Id {get; set;}
    public string CatalogoNombre {get; set;}
    public string Nombre {get; set;}
    public int Stock {get; set;}
    public int Precio {get; set;}
}
public class ProductoCambioDTO
{
    public int Id {get; set;}
    public int Precio {get; set;} = 0;
    public int Stock {get; set;} = 0;
    public int CatalogoNombre {get; set;} = 0;
}