using Gerenciador_De_Vendas.Entities;
using Gerenciador_De_Vendas.Repository.Produtos;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Gerenciador_De_Vendas.Service.RemoverSessao;
namespace Gerenciador_De_Vendas.Service.ProdutoService
{
    public class ProdutoService
    {
        private readonly IProdutoRepository _repository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProdutoService(IProdutoRepository repository, IHttpContextAccessor httpContextAccessor)
        {
            _repository = repository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IEnumerable<Produto>> GetAllAsync()
        {   
            //var lista = _httpContextAccessor.HttpContext?.Session.Get<List<Produto>>("VendaLista");

            //foreach(var item in lista ?? new List<Produto>())
            //{
            //   var produto = await _repository.GetByIdAsync(item.Id);
            //    produto.Quantidade_Reservada -= item.Quantidade_Reservada;
            //    await _repository.UpdateAsync(produto);
            //}
            //// Antes de buscar os produtos, remover a sessão "VendaLista" se existir
            //RemoverSessao.RemoverSessao removerSessao = new RemoverSessao.RemoverSessao(new HttpContextAccessor());
            //removerSessao.Remover(); // Remove a sessão antes de buscar os produtos
            
            return await _repository.GetAllAsync();
        }

        public async Task<Produto> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }


        public async Task CreateAsync(Produto produto)
        {
            if (produto == null)
            {
                throw new ArgumentNullException(nameof(produto), "Produto cannot be null");
            }
            await _repository.CreateAsync(produto);
        }

        public async Task UpdateAsync(Produto produto)
        {
            if (produto == null)
            {
                throw new ArgumentNullException(nameof(produto), "Produto cannot be null");
            }
           
            await _repository.UpdateAsync(produto);
        }

        public async Task DeleteAsync(Produto produto)
        {
            if (produto == null)
            {
                throw new ArgumentNullException(nameof(produto), "Produto cannot be null");
            }
             var produtoExistente = await _repository.GetByIdAsync(produto.Id);
             if (produtoExistente == null)
             {
                 throw new KeyNotFoundException($"Produto with ID {produto.Id} not found");             
             }
            
            await _repository.DeleteAsync(produtoExistente);
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
                        var produto = _repository.GetByIdAsync(item.ProdutoId ?? 0).Result;
                        if (produto != null)
                        {
                            // Remove a reserva
                            produto.Quantidade_Reservada -= item.Quantidade;
                            _repository.UpdateAsync(produto).Wait();
                        }
                    }
                    // Limpa a lista da sessão
                    context.Session.Remove("VendaLista");
                }
            }
        }
    }
}
