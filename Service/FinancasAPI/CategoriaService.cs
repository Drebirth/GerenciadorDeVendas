using Gerenciador_De_Vendas.Models;
using System.Text;
using System.Text.Json;

namespace Gerenciador_De_Vendas.Service.FinancasAPI;

public class CategoriaService : ICategoriaService
{
    private const string apiEndpoint = "/api/Categorias";
    private readonly JsonSerializerOptions _options;
    private readonly IHttpClientFactory _httpClientFactory;
    
    private IEnumerable<CategoriaViewModel> _categoriasViewModel;
    private CategoriaViewModel categoriaVM;
    
    public CategoriaService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
        _options = new JsonSerializerOptions
        {
          //  PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            PropertyNameCaseInsensitive = true
        };
    }

    public async Task<CategoriaViewModel> CreateAsync(CategoriaViewModel categoriavm)
    {
        var client = _httpClientFactory.CreateClient("CategoriaAPI"); 
        var categoria = JsonSerializer.Serialize(categoriaVM);
        // StringContent faz parte do corpo da requisição, que é enviada para o servidor
        StringContent content = new StringContent(categoria, Encoding.UTF8, "application/json");
        using (var response = await client.PostAsync(apiEndpoint, content))
        {
            if(response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();
                categoriaVM = await JsonSerializer.DeserializeAsync<CategoriaViewModel>(apiResponse, _options);
            }
            else { return null; }
            return categoriaVM;
        }
    }

    public Task<bool> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<CategoriaViewModel>> GetAllAsync()
    {
        var client = _httpClientFactory.CreateClient("CategoriaAPI");
       

        using (var response = await client.GetAsync(apiEndpoint))
        {
            if(response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();
                _categoriasViewModel = await JsonSerializer.DeserializeAsync<IEnumerable<CategoriaViewModel>>(apiResponse, _options);
            }
            else
            {
                return null;
            }
        }
        return _categoriasViewModel;
    }

    public Task<CategoriaViewModel> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<CategoriaViewModel> UpdateAsync(CategoriaViewModel categoria)
    {
        throw new NotImplementedException();
    }

}