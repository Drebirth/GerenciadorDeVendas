using Gerenciador_De_Vendas.Context;
using Gerenciador_De_Vendas.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Gerenciador_De_Vendas.Controllers;

public class ProdutoController : Controller
{

    private readonly EstoqueContext _context;

    public ProdutoController(EstoqueContext context)
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
    public ActionResult Edit()
    {            
            
        // if(ModelState.IsValid)
        // {   
        //     _context.Produtos.Update();
        //     _context.SaveChanges();
        //     return RedirectToAction("Index");
        // }
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