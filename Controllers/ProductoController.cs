using Microsoft.AspNetCore.Mvc;
using TP2.Services;
using TP2.DTOs;
using TP2.Models;
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

    [HttpPost("{idc}")]
    public void Post(ProductoDTO p, int idc)
    {
        _productoService.CrearProd(p, idc);
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