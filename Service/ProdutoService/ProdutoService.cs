using Gerenciador_De_Vendas.Entities;
using Gerenciador_De_Vendas.Repository.Produtos;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Gerenciador_De_Vendas.Service.ProdutoService
{
    public class ProdutoService
    {
        private readonly IProdutoRepository _repository;

        public ProdutoService(IProdutoRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Produto>> GetAllAsync()
        {
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
    }
}
