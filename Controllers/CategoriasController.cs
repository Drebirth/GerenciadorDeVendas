using Gerenciador_De_Vendas.Models;
using Gerenciador_De_Vendas.Service.FinancasAPI;
using Microsoft.AspNetCore.Mvc;

namespace Gerenciador_De_Vendas.Controllers
{
    public class CategoriasController : Controller
    {
        private readonly ICategoriaService _categoriaService;
        public CategoriasController(ICategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }

        public async Task<ActionResult<IEnumerable<CategoriaViewModel>>> Index()
        {
            var categorias = await _categoriaService.GetAllAsync();
            if (categorias == null) return NotFound();

            return View(categorias);
        }
    }
}
