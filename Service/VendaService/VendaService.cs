using Gerenciador_De_Vendas.Entities;
using Gerenciador_De_Vendas.Repository.ItensVendas;
using Gerenciador_De_Vendas.Repository.Produtos;
using Gerenciador_De_Vendas.Repository.Vendas;
using Gerenciador_De_Vendas.Service.AdicionarProduto;

namespace Gerenciador_De_Vendas.Service.VendaService
{
    public class VendaService
    {
        private readonly IVendaRepository _vendaRepository;
        private readonly IItensVendaRepository _itensRepository;

        private readonly IProdutoRepository _produtoRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public VendaService(IVendaRepository vendaRepository, IItensVendaRepository itensRepository, IProdutoRepository produtoRepository, IHttpContextAccessor httpContextAccessor)
        {
            _vendaRepository = vendaRepository;
            _itensRepository = itensRepository;
            _produtoRepository = produtoRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IEnumerable<Venda>> GetAllAsync()
        {
            return await _vendaRepository.GetAllAsync();
        }

        public async Task<Venda> GetByIdAsync(int id)
        {
            return await _vendaRepository.GetByIdAsync(id);
        }

        public async Task CreateAsync(Venda venda)
        {
            var listaItens = _httpContextAccessor.HttpContext.Session.Get<List<ItensVenda>>("VendaLista");
            venda.Itens = listaItens;
            foreach (var produtos in venda.Itens)
            {
                var produto = await _produtoRepository.GetByIdAsync((int)produtos.ProdutoId);

                produto.Saldo_Estoque -= produtos.Quantidade;
                produto.Quantidade_Reservada -= produtos.Quantidade;

                await _produtoRepository.UpdateAsync(produto);
            }
            await _vendaRepository.CreateAsync(venda);
        }

        // Toda modificação realizada seja remover ou adicionar o produto precisa passar novamente na session
        public async Task<AdicionarProdutoResult> Adicionar(int produtoId, int quantidade, Venda? venda)
        {
            var vendaAtual = _httpContextAccessor.HttpContext.Session.Get<List<ItensVenda>>("VendaLista") ?? new List<ItensVenda>();
            var produto = await _produtoRepository.GetByIdAsync(produtoId);

            if (produto == null)
                return new AdicionarProdutoResult { Sucesso = false, Mensagem = "Produto não encontrado." };

            if ((produto.Saldo_Estoque - produto.Quantidade_Reservada)  < quantidade)
            {

                return new AdicionarProdutoResult { Sucesso = false, Mensagem = $"Quantidade solicitada ({quantidade}) é maior que o estoque disponível ({produto.Saldo_Estoque}).", venda = vendaAtual };
                 
            }
            else
            {
                // Atualiza o saldo do estoque do produto
                produto.Quantidade_Reservada += quantidade;
                await _produtoRepository.UpdateAsync(produto);
            }

            var produtoVenda = vendaAtual.FirstOrDefault(x => x.ProdutoId == produtoId);

            if (produtoVenda != null)
            {
                produtoVenda.Quantidade += quantidade;
            }
            else
            {
                vendaAtual.Add(new ItensVenda
                {
                    ProdutoId = produtoId,
                    Nome = produto.Nome,
                    Descricao = produto.Descricao,
                    PrecoVenda = produto.PrecoVenda,
                    Quantidade = quantidade
                });
                _httpContextAccessor.HttpContext.Session.Set("VendaLista", vendaAtual);
            }

            return new AdicionarProdutoResult { Sucesso = true };
        }
                
        
        public void VerificarCookieReserva(string cookieName)
        {
            var context = _httpContextAccessor.HttpContext;
            if (context == null)
                return;

            //var cookie = context.Request.Cookies[cookieName];
            var cookie = context.Session.GetString(cookieName);
            if (string.IsNullOrEmpty(cookie))
            {
                // Cookie expirou ou não existe, remover reservas
                var vendaAtual = context.Session.Get<List<ItensVenda>>("VendaLista");
                if (vendaAtual != null)
                {
                    foreach (var item in vendaAtual)
                    {
                        var produto = _produtoRepository.GetByIdAsync(item.ProdutoId ?? 0).Result;
                        if (produto != null)
                        {
                            // Remove a reserva
                            produto.Quantidade_Reservada -= item.Quantidade;
                            _produtoRepository.UpdateAsync(produto).Wait();
                        }
                    }
                    // Limpa a lista da sessão
                    context.Session.Remove("VendaLista");
                }
            }
        }
    }
}
