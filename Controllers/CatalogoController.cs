using Microsoft.AspNetCore.Mvc;
using TP2.Services;
using TP2.DTOs;
using TP2.Models;
namespace TP_2.Controllers;

[ApiController]
[Route("[controller]")]
public class CatalogoController : ControllerBase
{
    private readonly ICatalogoService _catalogoService;

    public CatalogoController(ICatalogoService catalogoService)
    {
        _catalogoService = catalogoService;
    }

    [HttpPost]
    public void Post(CatalogoDTO c)
    {
        _catalogoService.NuevaCat(c);
    }

    [HttpGet]
    public List<Catalogo> Get()
    {
        return _catalogoService.GetCatalogos();
    }

    [HttpDelete("{id}")]
    public void Delete(int id)
    {
        _catalogoService.BorrarCatalogos(id);
    }
    [HttpGet("{id}")]
    public string Get(int id)
    {
        return _catalogoService.ObtenerNombreXid(id);
    }
    [HttpGet("{id}/Productos")]
    public CatalogoDTO GetProductos(int id)
    {
        return _catalogoService.EnCatalogoXId(id);
    }
    [HttpGet("Productos")]
    public List<CatalogoDTO> GetProductos()
    {
        return _catalogoService.EnTodosCatalogos();
    }   
}
