using Gerenciador_De_Vendas.Context;
using Gerenciador_De_Vendas.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Gerenciador_De_Vendas.Controllers;

[Authorize]
public class ProdutoController : Controller
{

    private readonly AppDbContext _context;

    public ProdutoController(AppDbContext context)
    {
        _context = context;
    }

    public ActionResult Index()
    {
        return View();
    }

    public IActionResult Details()
    {
        var produtos = _context.Produtos.ToList();
        return View(produtos);
    }


    public ActionResult Edit(int id)
    {   
        var produto = _context.Produtos.Find(id);
        return View(produto);
    }

    [HttpPost]
    public ActionResult Edit(Produto produto)
    {            
            
         if(ModelState.IsValid)
         {   
             _context.Produtos.Update(produto);
             _context.SaveChanges();
             return RedirectToAction("Index");
         }
         return View();
    }

    public ActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public ActionResult Create(Produto produto)
    {
        
         if(ModelState.IsValid)
         {
             _context.Produtos.Add(produto);
             _context.SaveChanges();
             return RedirectToAction("Index");
         }
        return View(produto);
    }

    public ActionResult Delete(int id)
    {
        var produto = _context.Produtos.Find(id);
        if(produto is null)
        {
            return NotFound();
        }
        return View(produto);
    }

    [HttpPost]
    public ActionResult Delete(Produto produto)
    {
        var produtoDeletado = _context.Produtos.Find(produto.Id);
        _context.Produtos.Remove(produtoDeletado);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }
}