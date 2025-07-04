using System.Collections;

namespace Gerenciador_De_Vendas.Repository;

public interface IRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> GetByIdAsync(int id);
    Task CreateAsync(T entity);
    Task UpdateAsync(T entity);

    Task DeleteAsync(T entity);

    
}