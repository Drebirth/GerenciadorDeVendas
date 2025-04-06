using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gerenciador_De_Vendas.Entities;

public class Produto
{
    [Key]
    public int Id {get; set;}

    [Required(ErrorMessage ="O nome do produto é obrigatório")]
    public string? Nome {get; set;}
    public string? Descricao {get;set;}
    public int? Saldo_Estoque {get;set;}

    public DateTime Inclusao_Produto {get;set;} 
    public decimal? PrecoCompra {get;set;}

    public decimal PrecoVenda {get; set;}

}