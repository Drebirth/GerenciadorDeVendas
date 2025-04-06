using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gerenciador_De_Vendas.Entities;

public class Venda 
{
  [Key]
    public int Id {get;set;}

    public decimal? ValorTotal  {get;set;}

    // public DateTime Emissao {get;set;}

    // public string NomeCliente {get;set;}

    // public List<Pagamento>? FormaDePagamento {get;set;}

    // public int PagamentoId {get;set;}
    public List<ItensVenda>? Itens {get;set;}

    public List<Produto>? produtos {get;set;}

    
  
  
  
}