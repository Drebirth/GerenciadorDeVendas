using Gerenciador_De_Vendas.Models;
using Gerenciador_De_Vendas.Service.FinancasAPI;
using Microsoft.AspNetCore.Mvc;

namespace Gerenciador_De_Vendas.Controllers.API
{
    public class CategoriasController : Controller
    {
        private readonly ICategoriaService _service;

        public CategoriasController(ICategoriaService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoriaViewModel>>> Index()
        {
            var categorias = await _service.GetAllAsync();
            if (categorias == null) return NotFound();

            return View(categorias);
        }
    }
}
