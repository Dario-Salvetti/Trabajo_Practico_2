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

    [HttpPut]
     public ProductoIndividualDTO Cambiar(ProductoCambioDTO x)
    {
       return _productoService.CambiarXId(x);
    }
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
        _productoService.BorrarXId(id);
    }
    [HttpGet("{id}")]
    public ProductoIndividualDTO Obtener(int id)
    {
        return _productoService.ObtenerXId(id);
    }

}