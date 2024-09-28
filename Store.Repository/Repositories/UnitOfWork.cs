using Store.Data;
using Store.Data.Entities;
using Store.Repository.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Repository.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreDBContext _context;
        private Hashtable _repositories;

        public UnitOfWork(StoreDBContext context) 
        {
           _context = context;
        }
        public async Task<int> CompleteAsync()
            =>await _context.SaveChangesAsync();

        public IGenericRepository<TEntity, TKey> Repository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
           if(_repositories is null)
                _repositories = new Hashtable();
           var EntityKey = typeof(TEntity).Name;
            if(!_repositories.ContainsKey(EntityKey))
            {
                var  repositryType = typeof(GenericRepository<,>);  
                var repositoryInstance =Activator.CreateInstance(repositryType.MakeGenericType(typeof(TEntity),typeof(TKey)),_context);
                _repositories.Add(EntityKey, repositoryInstance);
            }
            return (IGenericRepository < TEntity, TKey >)_repositories[EntityKey];
        }
    }
}
