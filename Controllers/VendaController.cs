using Gerenciador_De_Vendas.Context;
using Gerenciador_De_Vendas.Entities;
using Gerenciador_De_Vendas.Models;
using Microsoft.AspNetCore.Mvc;

namespace Gerenciador_De_Vendas.Controllers;

 public class VendaController : Controller
 {
    private readonly EstoqueContext _context;

    public VendaController(EstoqueContext context)
    {
        _context = context;
  
    }

    public ActionResult Index()
    {
        return View();
    }

    public ActionResult Create()
    {
        Venda venda = new Venda();
        var produtoLista = _context.Produtos.ToList();     
        
        return View(venda);
    }

    [HttpPost]
    public IActionResult Create(Venda venda)
    {
        if(ModelState.IsValid)
        {
            _context.Vendas.Add(venda);
            _context.SaveChanges();
            return View(Index);
        }

        return View(Index);
    }

    public IActionResult Adicionar(int? id)
    {
        var venda = HttpContext.Session.Get<Venda>("vendaLista") ?? new Venda();


        var produto = _context.Produtos.Find(id);
        if (id == null)
        {
            return NotFound();
        }

        if (venda.Itens == null)
        {
            venda.Itens = new List<ItensVenda>();
        }
        var item = new ItensVenda
        {
            Id = produto.Id,
            produto = produto,
            Quantidade = 1,
            ValorUnitario = produto.PrecoVenda
        };
        venda.Itens.Add(item);
        venda.ValorTotal = venda.Itens.Sum(i => i.ValorUnitario * i.Quantidade);
        HttpContext.Session.Set("Venda", venda);
        return RedirectToAction("Create");



    }




    


 }