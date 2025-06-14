using System.Threading.Tasks;
using Gerenciador_De_Vendas.Context;
using Gerenciador_De_Vendas.Entities;
using Gerenciador_De_Vendas.Service.ProdutoService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Gerenciador_De_Vendas.Controllers;

[Authorize]
public class ProdutoController : Controller
{

    //private readonly AppDbContext _context;
    private readonly ProdutoService _service;

    public ProdutoController(ProdutoService service/*AppDbContext context*/)
    {
        //_context = context;
        _service = service;
    }

    public IActionResult Index()
    {
        return View();
    }

    public async Task<IActionResult> GetAll()
    {
        //var produtos = _context.Produtos.ToList();
        //_service.VerificarCookieReserva("VendaLista"); // Verifica se o cookie de reserva está ativo
        return View(await _service.GetAllAsync());
    }


    public async Task<IActionResult> Edit(int id)
    {   
        //var produto = _context.Produtos.Find(id);
        return View(await _service.GetByIdAsync(id));
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Produto produto)
    {
        if (ModelState.IsValid)
        {
            await _service.UpdateAsync(produto);
            return RedirectToAction("Index");
        }
        return View(produto);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Produto produto)
    {
        
         if(ModelState.IsValid)
         {
            await _service.CreateAsync(produto);
            return RedirectToAction("Index");
         }
        return View(produto);
    }

    public async Task<IActionResult> Delete(int id)
    {
        
        return View(await _service.GetByIdAsync(id));
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(Produto produto)
    {
        
        await _service.DeleteAsync(produto);
        return RedirectToAction(nameof(Index));
    }
}