using Gerenciador_De_Vendas.Context;
using Gerenciador_De_Vendas.Entities;

namespace Gerenciador_De_Vendas.Repository.ItensVendas
{
    public class ItensVendaRepository : Repository<ItensVenda>, IItensVendaRepository
    {
        public ItensVendaRepository(AppDbContext context) : base(context)
        {
        }
        
    }
}
