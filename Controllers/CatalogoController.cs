using Microsoft.AspNetCore.Mvc;
using TP2.Services;
using TP2.DTOs;
using TP2.Models;
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
}