using Microsoft.EntityFrameworkCore;
using Store.Data;
using Store.Data.Entities;
using Store.Repository.Interfaces;
using Store.Repository.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Repository.Repositories
{
    public class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        private readonly StoreDBContext _context;

        public GenericRepository(StoreDBContext context)
        {
            _context = context;
        }
        public async Task AddAsync(TEntity entity)
             => await _context.Set<TEntity>().AddAsync(entity);

        public void DeleteAsync(TEntity entity)
            =>  _context.Set<TEntity>().Remove(entity);

        public async Task<IReadOnlyList<TEntity>> GetAllAsNoTrackingAsync()
         => await _context.Set<TEntity>().AsNoTracking().ToListAsync();

        public async Task<IReadOnlyList<TEntity>> GetAllAsync()
            => await _context.Set<TEntity>().ToListAsync();

        

        public async Task<TEntity> GetByIdAsync(TKey? Id)
            => await _context.Set<TEntity>().FindAsync(Id);
        public void UpdateAsync(TEntity entity)
            => _context.Set<TEntity>().Update(entity);
        public async Task<TEntity> GetWithSpecByIdAsync(ISpecification<TEntity> spec)
        => await ApplySpecification(spec).FirstOrDefaultAsync();

        public async Task<IReadOnlyList<TEntity>> GetAllWithSpecAsync(ISpecification<TEntity> spec)
            =>await ApplySpecification(spec).ToListAsync();

        private IQueryable<TEntity> ApplySpecification(ISpecification<TEntity> specs)
            => SpecificationEvaluater<TEntity, TKey>.GetQuery(_context.Set<TEntity>(), specs);

        public async Task<int> GetCountSpecAsync(ISpecification<TEntity> spec)
            => await ApplySpecification(spec).CountAsync(); 
    }
}
