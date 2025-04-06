using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Gerenciador_De_Vendas.Entities;

public class ItensVenda
{
    [Key]
    public int Id {get;set;}
    public List<Produto>? Produtos {get; set;}

    public int vendaId {get; set;}
    public Produto? produto {get;set;}

    public int Quantidade {get; set;}
    public decimal ValorUnitario {get; set;}
    public decimal subtotal => Quantidade * produto.PrecoVenda;
}