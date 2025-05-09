using Gerenciador_De_Vendas.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace Gerenciador_De_Vendas.Context;

public class AppDbContext : IdentityDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }

    public DbSet<Produto> Produtos {get;set;}
    public DbSet<Venda> Vendas {get;set;}
    public DbSet<ItensVenda> Itens { get; set; }

}