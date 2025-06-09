namespace Gerenciador_De_Vendas.Models.API
{
    public class TokenViewModel
    {

        public string? token { get; set; }
        public string? RefreshToken { get; set; }
        
        public DateTime RefreshTokenExpiryTime { get; set; }
    }
}
