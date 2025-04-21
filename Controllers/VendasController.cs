using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Gerenciador_De_Vendas.Context;
using Gerenciador_De_Vendas.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Gerenciador_De_Vendas.Migrations;
using Microsoft.AspNetCore.Authorization;

namespace Gerenciador_De_Vendas.Controllers
{
    [Authorize]
    public class VendasController : Controller
    {
        private readonly AppDbContext _context;

        public VendasController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Vendas
        public async Task<IActionResult> Index()
        {
            //var itens = HttpContext.Session.Get<List<ItensVenda>>("VendaLista") ?? new List<ItensVenda>();
            //Venda venda = new Venda();
            //venda.Itens = itens;

            //var venda = _context.Vendas.ToList();
            //return View(venda);
            return View();
        }

        // GET: Vendas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}

            //var venda = await _context.Vendas
            //    .FirstOrDefaultAsync(m => m.Id == id);
            //if (venda == null)
            //{
            //    return NotFound();
            //}

            //return View(venda);


            var venda = _context.Vendas.ToList();
            return View(venda);
        }

        // GET: Vendas/Create
        public IActionResult Create()
        {
            var itens = HttpContext.Session.Get<List<ItensVenda>>("VendaLista") ?? new List<ItensVenda>();
            Venda venda = new Venda();
            venda.Produtos = _context.Produtos.ToList();
            venda.Itens = itens;
            venda.ValorTotal = 0;
            foreach(var produto in itens)
            {
                venda.ValorTotal += produto.SubTotal;
            }
            return View(venda);
        }

        // POST: Vendas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ValorTotal,Emissao,NomeCliente")] Venda venda)
        {
            ItensVenda itens = new ItensVenda();
            var listaItens = HttpContext.Session.Get<List<ItensVenda>>("VendaLista");
            if (ModelState.IsValid)
            {
                //foreach(var produto in teste)
                //{
                   

                //    var saldoEstoque = _context.Produtos.Find(produto.ProdutoId);
                //    //// Entry é um método do Entity Framework que permite rastrear o estado de uma entidade
                //    _context.Entry(saldoEstoque).State = EntityState.Modified;
                //    produto.Saldo_Estoque -= produto.Quantidade;
                //    _context.SaveChanges();
                //}

                
                _context.Add(venda);
                await _context.SaveChangesAsync();

               // var listaItens = HttpContext.Session.Get<List<ItensVenda>>("VendaLista");

                foreach (var itenslista in listaItens)
                {
                    itens = itenslista;
                    itens.VendaId = venda.Id;
                    var estoque = _context.Produtos.Find(itens.ProdutoId);
                    estoque.Saldo_Estoque -= itens.Quantidade;

                    _context.Itens.Add(itens);
                    _context.SaveChanges();
                }

                // Limpa a sessão após salvar a venda
                HttpContext.Session.Remove("VendaLista");
                return RedirectToAction(nameof(Index));
            }
            return View(venda);
        }

        // GET: Vendas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venda = await _context.Vendas.FindAsync(id);
            if (venda == null)
            {
                return NotFound();
            }
            return View(venda);
        }

        // POST: Vendas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ValorTotal,Emissao,NomeCliente")] Venda venda)
        {
            if (id != venda.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(venda);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VendaExists(venda.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(venda);
        }

        // GET: Vendas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venda = await _context.Vendas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (venda == null)
            {
                return NotFound();
            }

            return View(venda);
        }

        // POST: Vendas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var venda = await _context.Vendas.FindAsync(id);
            if (venda != null)
            {
                _context.Vendas.Remove(venda);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VendaExists(int id)
        {
            return _context.Vendas.Any(e => e.Id == id);
        }



        // Toda modificação realizada seja remover ou adicionar o produto precisa passar novamente na session
        public IActionResult Adicionar(int produtoId,int Quantidade, ItensVenda? Itensvenda, Venda? venda)
        {  // Se a venda for nula, inicializa a lista de vendas
            var VendaLista = HttpContext.Session.Get<List<ItensVenda>>("VendaLista") ?? new List<ItensVenda>();

            var produto = _context.Produtos.Find(produtoId);
            if (produto == null)
            {
                ModelState.AddModelError("ProdutoId", $"Produto {produtoId} não encontrado, favor verificar o codigo correto no cadastro de produtos");
                return View("Create", venda);

            }

            if (produto.Saldo_Estoque < Quantidade)
            {
                venda.Quantidade = Quantidade;
                ModelState.AddModelError("Quantidade", $"Não é possível incluir a quantidade: {Quantidade}, pois não há estoque suficiente");
                return View("Create", venda);
            }

            if (ModelState.IsValid)
            { 

             

            // itemExistente é o item que já existe na lista de vendas
            var itemExistente = VendaLista.FirstOrDefault(i => i.ProdutoId == produtoId);

                //var saldoEstoque = produto;
                //// Entry é um método do Entity Framework que permite rastrear o estado de uma entidade
                //_context.Entry(produto).State = EntityState.Modified;
                //produto.Saldo_Estoque -= Quantidade;
                //_context.SaveChanges();
                /*
                 * Tipos por Referência: Incluem objetos como classes (List<>, arrays, etc.). Quando você atribui um tipo por referência a outra variável,
                 * ambas as variáveis apontam para o mesmo objeto na memória. Alterações feitas através de uma variável serão refletidas na outra.
                 */
                if (itemExistente != null)
            {
                // Ponteiro? 
                // já que itemExistente é igual a VendaLista, caso seja atualizada e passamos o novo valor a lista será atualizada também 
                // Se o item já existe, apenas atualiza a quantidade
                itemExistente.Quantidade += Quantidade;
                HttpContext.Session.Set("VendaLista", VendaLista);
                return RedirectToAction("Create");
            }
            else
            {
                if (produto != null)
                {
                    var vendaItens = new ItensVenda
                    {
                        ProdutoId = produto.Id,
                        Nome = produto.Nome,
                           Descricao = produto.Descricao,
                        Quantidade = Quantidade,
                        PrecoVenda = produto.PrecoVenda
                    };
                    VendaLista.Add(vendaItens);
                    HttpContext.Session.Set("VendaLista", VendaLista);
                }
            }
            }
            return RedirectToAction("Create");


                    //var VendaLista = HttpContext.Session.Get<List<ItensVenda>>("VendaLista") ?? new List<ItensVenda>();

                    //var produto = _context.Produtos.Find(produtoId);
                    //if(produto is null)
                    //{
                    //    return NotFound("Produto não encontrado");
                    //}

                    //var itemExistente = VendaLista.FirstOrDefault(i => i.ProdutoId == produtoId);

                    //if (itemExistente != null)
                    //{

                    //    itemExistente.Quantidade += ;

                    //}
                    //else
                    //{
                    //    var vendaItens = new ItensVenda
                    //        {
                    //            ProdutoId = produto.Id,
                    //            Nome = produto.Nome,
                    //            Quantidade = Quantidade,
                    //            PrecoVenda = produto.PrecoVenda
                    //        };



                    //        VendaLista.Add(vendaItens);
                    //        HttpContext.Session.Set("VendaLista", VendaLista);

                    //}
                    ////if (produto != null)
                    ////{
                    ////    var vendaItens = new ItensVenda
                    ////    {
                    ////        ProdutoId = produto.Id,
                    ////        Nome = produto.Nome,
                    ////        Quantidade = Quantidade,
                    ////        PrecoVenda = produto.PrecoVenda
                    ////    };



                    ////    VendaLista.Add(vendaItens);
                    ////    HttpContext.Session.Set("VendaLista", VendaLista);
                    ////}
                    //return RedirectToAction("Create");
                }
    }
}
