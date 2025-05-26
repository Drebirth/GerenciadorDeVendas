using Gerenciador_De_Vendas.Context;
using Gerenciador_De_Vendas.Entities;

namespace Gerenciador_De_Vendas.Repository.Produtos;


public class ProdutoRepository : Repository<Produto>, IProdutoRepository
{
    public ProdutoRepository(AppDbContext context) : base(context)
    {
    }
}