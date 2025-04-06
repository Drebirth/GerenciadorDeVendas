using Gerenciador_De_Vendas.Entities;
using Microsoft.EntityFrameworkCore;

namespace Gerenciador_De_Vendas.Context;

public class EstoqueContext : DbContext
{
    public EstoqueContext(DbContextOptions<EstoqueContext> options) : base(options)
    {

    }

    public DbSet<Produto> Produtos {get;set;}
    public DbSet<Venda> Vendas {get;set;}
}