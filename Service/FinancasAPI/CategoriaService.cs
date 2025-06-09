using Gerenciador_De_Vendas.Models;
using System.Text.Json;

namespace Gerenciador_De_Vendas.Service.FinancasAPI;

public class CategoriaService : ICategoriaService
{
    private const string apiEndpoint = "/api/Categorias";
    private readonly JsonSerializerOptions _options;
    private readonly IHttpClientFactory _httpClientFactory;
    
    private IEnumerable<CategoriaViewModel> _categoriasViewModel;
    
    public CategoriaService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
        _options = new JsonSerializerOptions
        {
          //  PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            PropertyNameCaseInsensitive = true
        };
    }

    public Task<CategoriaViewModel> CreateAsync(CategoriaViewModel categoria)
    {
        throw new NotImplementedException();
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