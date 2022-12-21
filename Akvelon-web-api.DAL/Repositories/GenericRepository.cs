using System.Linq.Expressions;
using Akvelon_web_api.DAL.Interfaces.BaseInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Akvelon_web_api.DAL.Interfaces.Implementations;

public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
{
    private readonly ApplicationDbContext _context;
    private readonly DbSet<TEntity> _dbSet;

    public GenericRepository(ApplicationDbContext context)
    {
        _context = context;
        
        //Setting concrete entity type
        _dbSet = context.Set<TEntity>();
    }

    public async Task<bool> Create(TEntity entity)
    {
        _dbSet.Add(entity);
        await _context.SaveChangesAsync();
        
        return true;
    }

    public async Task<TEntity> FindById(int id)
    {
        return (await _dbSet.FindAsync(id))!;
    }

    public async Task<List<TEntity>> Get()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<List<TEntity>> Get(Func<TEntity, bool> predicate)
    {
        return await Task.FromResult(_dbSet.Where(predicate).ToList());
    }

    public async Task<bool> Remove(TEntity entity)
    {
        _dbSet.Remove(entity);
        await _context.SaveChangesAsync();
        
        return true;
    }

    public async Task<bool> Update(TEntity entity)
    { 
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return true;
    }
    
    public async Task<List<TEntity>> GetWithInclude(params Expression<Func<TEntity, object>>[] includeProperties)
    {
        return await Include(includeProperties).ToListAsync();
    }
 
    public async Task<List<TEntity>> GetWithInclude(Func<TEntity,bool> predicate, 
        params Expression<Func<TEntity, object>>[] includeProperties)
    {
        var query =  Include(includeProperties);
        return await Task.FromResult(query.AsEnumerable().Where(predicate).ToList());
    }
 
    private IQueryable<TEntity> Include(params Expression<Func<TEntity, object>>[] includeProperties)
    {
        IQueryable<TEntity> query = _dbSet.AsNoTracking();
        return includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
    }
}