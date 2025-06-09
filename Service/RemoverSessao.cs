namespace Gerenciador_De_Vendas.Service.RemoverSessao
{
    public class RemoverSessao
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RemoverSessao(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void Remover()
        {
           
            _httpContextAccessor.HttpContext.Session.Remove("VendaLista");
        }
    }
}