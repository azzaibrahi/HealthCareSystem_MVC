using HealthCareSystem_MVC.BLL.Interfaces;
using HealthCareSystem_MVC.DAL.Data;
using HealthCareSystem_MVC.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace HealthCareSystem_MVC.BLL.Implementations;

/// <inheritdoc />
public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
{
    private readonly ApplicationDbContext _dbContext;
    protected readonly DbSet<TEntity> _dbSet;

    public GenericRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = _dbContext.Set<TEntity>();
    }

    public async Task CreateAsync(TEntity entity)
        => await _dbSet.AddAsync(entity);

    public void Delete(TEntity entity)
        => _dbSet.Remove(entity);

    public async Task<IEnumerable<TEntity>> GetAllAsync()
        => await _dbSet.ToListAsync();

    public async Task<TEntity?> GetAsync(int id)
        => await _dbSet.FindAsync(id);

    public void Update(TEntity entity)
        => _dbSet.Update(entity);
}
