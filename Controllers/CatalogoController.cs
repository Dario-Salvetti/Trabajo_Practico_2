using Microsoft.AspNetCore.Mvc;
using TP2.Services;
using TP2.DTOs;
namespace TP_2.Controllers;

[ApiController]
[Route("[controller]")]
public class CatalogoController : ControllerBase
{
    private readonly CatalogoService _catalogoService;

    public CatalogoController(CatalogoService catalogoService)
    {
        _catalogoService = catalogoService;
    }

    [HttpGet]
    public List<CatalogoDTO> GetProductos()
    {
        return _catalogoService.EnTodosCatalogos();
    }

    [HttpGet("{id}")]
    public CatalogoDTO GetProductos(int id)
    {
        return _catalogoService.EnCatalogoPorId(id);
    }

    [HttpPost]
    public void Post(CrearCatalogoDTO c)
    {
        _catalogoService.NuevaCat(c);
    }

    [HttpDelete("{id}")]
    public void Delete(int id)
    {
        _catalogoService.BorrarCatalogo(id);
    }   
}
