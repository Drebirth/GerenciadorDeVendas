using Gerenciador_De_Vendas.Models.API;
using System.Text;
using System.Text.Json;

namespace Gerenciador_De_Vendas.Service.FinancasAPI
{
    public class Autenticacao : IAutenticacao
    {
        private readonly IHttpClientFactory _httpClientFactory;
        const string apiEndpoint = "/api/Auth/Login";
        const string apiRegister = "/api/Auth/Register";
        private readonly JsonSerializerOptions _options;
        private TokenViewModel token;

        public Autenticacao(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        public async Task<TokenViewModel> AutenticaUsuario(UsuarioAPIViewModel usuarioAPIViewModel)
        {
            var client = _httpClientFactory.CreateClient("CategoriaAPI");
            var usuario = JsonSerializer.Serialize(usuarioAPIViewModel);
            StringContent content = new StringContent(usuario, Encoding.UTF8, "application/json");

            using (var response = await client.PostAsync(apiEndpoint, content))
            {
                if(response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStreamAsync();
                    token = await JsonSerializer.DeserializeAsync<TokenViewModel>(apiResponse, _options);
                }
            }
            return token;
        }

        public async Task<UsuarioAPIViewModel> CadastrarUsuario(UsuarioAPIViewModel usuario)
        {
            var client = _httpClientFactory.CreateClient("CategoriaAPI");
            var user = JsonSerializer.Serialize(usuario);
            StringContent content = new StringContent(user, Encoding.UTF8, "application/json");

            using (var response = await client.PostAsync(apiRegister, content))
            {
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStreamAsync();                    
                    usuario = await JsonSerializer.DeserializeAsync<UsuarioAPIViewModel>(apiResponse, _options);

                }
                else
                {
                    return null;
                }

            }

            return usuario;

        }
       
    }
}
