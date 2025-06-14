using Microsoft.AspNetCore.Mvc;

namespace Gerenciador_De_Vendas.Controllers.API
{
    public class FinancasAPIController : Controller
    {

        private readonly HttpContext _httpContext;

        public FinancasAPIController(HttpContext httpContext)
        {
            _httpContext = httpContext;
        }

        public ActionResult Index()
        {
            
            
            return View();
        }

        
    }
}
