using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Gerenciador_De_Vendas.Context;
using Gerenciador_De_Vendas.Entities;
using Microsoft.AspNetCore.Authorization;
using Gerenciador_De_Vendas.Service.VendaService;
using Gerenciador_De_Vendas.Service.ProdutoService;
using System.Threading.Tasks;

namespace Gerenciador_De_Vendas.Controllers
{
    [Authorize]
    public class VendasController : Controller
    {
        private readonly VendaService _service;
        private readonly ProdutoService _produtoService;
        public VendasController(VendaService service, ProdutoService produtoService)
        {
            _service = service;
            _produtoService = produtoService;
        }
        

        // GET: Vendas
        public async Task<IActionResult> Index()
        {
            return View();
        }

        // GET: Vendas/Details/5
        public async Task<IActionResult> ListaDePedidos()
        {
            
            return View( await _service.GetAllAsync());
        }

        // GET: Vendas/Create
        public async Task<IActionResult> Create()
        {
            var itens = HttpContext.Session.Get<List<ItensVenda>>("VendaLista") ?? new List<ItensVenda>();
            Venda venda = new Venda
            {
                Itens = itens,
                Produtos = (await _produtoService.GetAllAsync()).ToList(), // Obtém todos os produtos para o dropdown
                ValorTotal = itens.Sum(x => x.SubTotal) // Calcula o valor total com base nos itens
            };
            return View(venda);
        }

        // POST: Vendas/Create
        [HttpPost]
        public async Task<IActionResult> Create(Venda venda)
        {                        
            if (ModelState.IsValid)
            {
                try
                {
                    await _service.CreateAsync(venda);
                    // Limpa a sessão após salvar a venda
                    HttpContext.Session.Remove("VendaLista");
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View(venda);
        }                    

        
        public async Task<IActionResult> AdicionarItem(int produtoId, int quantidade, Venda? venda)
        {
            
            
            var resultado = await _service.Adicionar(produtoId, quantidade, venda);
            if (!resultado.Sucesso)
            {
                ModelState.AddModelError("", resultado.Mensagem);
                // Retorne a view com a mensagem de erro
                return View("Create", venda);
            }
            
            return RedirectToAction("Create", venda);
        }
    }
}
