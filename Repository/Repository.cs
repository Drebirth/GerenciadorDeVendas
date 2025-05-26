using System.Collections;
using Gerenciador_De_Vendas.Context;
using Microsoft.EntityFrameworkCore;

namespace Gerenciador_De_Vendas.Repository;

public class Repository<T> : IRepository<T> where T : class
{
    protected readonly AppDbContext _context;

    public Repository(AppDbContext context)
    {
        _context = context;
    }

    public async Task CreateAsync(T entity)
    {
       await  _context.AddAsync(entity);
       await  _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(T entity)
    {
         _context.Remove(entity);
       await  _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _context.Set<T>().ToListAsync();
    }

    public async Task<T> GetByIdAsync(int id)
    {
        return await _context.Set<T>().FindAsync(id);
    }

    public async Task UpdateAsync(T entity)
    {
        _context.Set<T>().Entry(entity).State = EntityState.Modified;
        //_context.Update(entity);
        await _context.SaveChangesAsync();
        
    }

  
}