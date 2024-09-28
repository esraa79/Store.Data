using Store.Data.Entities;
using Store.Repository.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Repository.Interfaces
{
    public interface IGenericRepository<TEntity,TKey> where TEntity : BaseEntity<TKey>
    {
        Task<TEntity> GetByIdAsync(TKey? Id);
        Task<IReadOnlyList<TEntity>> GetAllAsync();
        Task<IReadOnlyList<TEntity>> GetAllAsNoTrackingAsync();

        Task<TEntity> GetWithSpecByIdAsync(ISpecification<TEntity> spec);
        Task<IReadOnlyList<TEntity>> GetAllWithSpecAsync(ISpecification<TEntity> spec );

        Task<int> GetCountSpecAsync(ISpecification<TEntity> spec);
        Task AddAsync(TEntity entity);
        void UpdateAsync(TEntity entity);

        void DeleteAsync(TEntity entity);
    }
}
