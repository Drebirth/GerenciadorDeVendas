using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Gerenciador_De_Vendas.Entities;

public class ItensVenda
{
    [Key]
    public int Id {get;set;}
    public int? VendaId { get; set; }
    public int? ProdutoId { get; set; }
    public string? Nome { get; set; }
    public string? Descricao { get; set; }
    public int? Saldo_Estoque { get; set; }
    public decimal PrecoVenda { get; set; }
    public int Quantidade { get; set; }
    public decimal SubTotal => PrecoVenda * Quantidade;


}