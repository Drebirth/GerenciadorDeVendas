using Gerenciador_De_Vendas.Models;

namespace Gerenciador_De_Vendas.Service.FinancasAPI;

public interface ICategoriaService
{
    Task<IEnumerable<CategoriaViewModel>> GetAllAsync();
    Task<CategoriaViewModel> GetByIdAsync(int id);
    Task<CategoriaViewModel> CreateAsync(CategoriaViewModel categoria);
    Task<CategoriaViewModel> UpdateAsync(CategoriaViewModel categoria);
    Task<bool> DeleteAsync(int id);
}