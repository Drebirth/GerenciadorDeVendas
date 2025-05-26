using Gerenciador_De_Vendas.Context;
using Gerenciador_De_Vendas.Entities;

namespace Gerenciador_De_Vendas.Repository.Vendas;

public class VendaRepository : Repository<Venda>, IVendaRepository
{
    public VendaRepository(AppDbContext context) : base(context)
    {
    }
}