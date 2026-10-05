using Microsoft.AspNetCore.Mvc;
using TP2.Services;
using TP2.DTOs;
namespace TP_2.Controllers;
[ApiController]
[Route("[controller]")]
public class ProductoController : ControllerBase
{
    private readonly ProductoService _productoService;

    public ProductoController(ProductoService productoService)
    {
        _productoService = productoService;
    }

    [HttpPost]
    public void Post(ProductoDTO p)
    {
        _productoService.CrearProd(p);
    }

    [HttpPut("{id}")]
     public ProductoIndividualDTO Cambiar(ProductoCambioDTO x, int id)
    {
       return _productoService.CambiarPorId(x, id);
    }
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
        _productoService.BorrarPorId(id);
    }
    [HttpGet("{id}")]
    public ProductoIndividualDTO Obtener(int id)
    {
        return _productoService.ObtenerPorId(id);
    }

}