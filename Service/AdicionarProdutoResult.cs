using Gerenciador_De_Vendas.Entities;

namespace Gerenciador_De_Vendas.Service.AdicionarProduto;

public class AdicionarProdutoResult
{
        public bool Sucesso { get; set; }
        public string? Mensagem { get; set; }
        public List<ItensVenda>? venda { get; set; }
}