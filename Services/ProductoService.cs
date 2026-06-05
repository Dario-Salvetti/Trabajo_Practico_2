using TP2.Models;
using TP2.Services;
using TP2.DTOs;

namespace TP2.Services;

public class ProductoService
{
    public Producto Crear(ProductoDTO p)
    {
        var prod = new Producto
        {
            Nombre = p.Marca+" "+p.Nombre+" "+p.Presentacion,
            Stock = p.Stock,
            Precio = p.Precio
        };

        return (prod);
    }
    
}