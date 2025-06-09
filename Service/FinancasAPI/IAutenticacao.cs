using Gerenciador_De_Vendas.Models.API;

namespace Gerenciador_De_Vendas.Service.FinancasAPI
{
    public interface IAutenticacao
    {
        Task<TokenViewModel> AutenticaUsuario(UsuarioAPIViewModel usuarioAPIViewModel);
        Task<UsuarioAPIViewModel> CadastrarUsuario(UsuarioAPIViewModel usuario);
    }
}
