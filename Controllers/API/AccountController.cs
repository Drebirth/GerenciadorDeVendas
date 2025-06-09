using Gerenciador_De_Vendas.Models.API;
using Gerenciador_De_Vendas.Service.FinancasAPI;
using Microsoft.AspNetCore.Mvc;

namespace Gerenciador_De_Vendas.Controllers.API
{
    public class AccountController : Controller
    {
        private readonly IAutenticacao _autenticacaoService;

        public AccountController(IAutenticacao autenticacaoService)
        {
            _autenticacaoService = autenticacaoService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UsuarioAPIViewModel usuario)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Usuário ou senha inválidos.");

                return View(usuario);

            }

            var result = await _autenticacaoService.AutenticaUsuario(usuario);

            if (result is null)
            {
                ModelState.AddModelError(string.Empty, "Usuário ou senha inválidos.");
                return View(usuario);
            }

            // Armazenar o token em algum lugar, como em um cookie ou sessão
            Response.Cookies.Append("AuthToken", result.token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true, // Use true em produção
                SameSite = SameSiteMode.Strict
            });
            return RedirectToAction("Index", "FinancasAPI");
        }

        [HttpGet]
        public async Task<ActionResult> Cadastro()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult<UsuarioAPIViewModel>> CadastrarUsuario(UsuarioAPIViewModel usuario)
        {
            if(!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Dados inválidos.");
                return usuario;
            }

            var result = await _autenticacaoService.CadastrarUsuario(usuario);

            return RedirectToAction("Login", "Account");

        }
    }
}
