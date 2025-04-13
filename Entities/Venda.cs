using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gerenciador_De_Vendas.Entities;
// Redesenhar a classe
public class Venda 
{
    [Key]
    public int Id {get;set;}

    public decimal? ValorTotal  {get;set;}
    public DateTime Emissao {get;set;}
    public string? NomeCliente {get;set;}
    public List<ItensVenda>? Itens {get;set;} = new List<ItensVenda>();
    public List<Produto>? Produtos { get; set; }
    //[Required(ErrorMessage = "A Quantidade do produto é obrigatório")]
    public int? Quantidade { get; set; }
    //[Required(ErrorMessage = "O codigo do produto é obrigatório")]
    public int? produtoId { get; set; }



}