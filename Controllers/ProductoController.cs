using Microsoft.AspNetCore.Mvc;
using TP2.Services;
using TP2.DTOs;
using TP2.Models;
using System.Transactions;
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
    public void Post(ProductoDTO p, int idt)
    {
        _productoService.CrearProd(p, idt);
    }

    [HttpPut]
     public void Cambiar(ProductoCambioDTO x)
    {
        _productoService.CambiarXId(x);
    }
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
        _productoService.BorrarXId(id);
    }
    [HttpGet("{id}")]
    public void Obtener(int id)
    {
        _productoService.ObtenerXId(id);
    }

}