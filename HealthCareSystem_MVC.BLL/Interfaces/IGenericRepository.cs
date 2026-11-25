using HealthCareSystem_MVC.DAL.Models;

namespace HealthCareSystem_MVC.BLL.Interfaces;

/// <summary>Common data operations for entities that inherit <see cref="BaseEntity"/>.</summary>
public interface IGenericRepository<TEntity> where TEntity : BaseEntity
{
    Task CreateAsync(TEntity entity);

    void Update(TEntity entity);

    void Delete(TEntity entity);

    Task<TEntity?> GetAsync(int id);

    Task<IEnumerable<TEntity>> GetAllAsync();
}
