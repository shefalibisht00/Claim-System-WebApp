using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ClaimSystem.DAL.CommonRepository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private DbSet<T> _objectSet;
        private readonly ClaimContext _dbContext = new ClaimContext();

        public Repository()
        {
            this._objectSet = _dbContext.Set<T>();
            this._dbContext.Configuration.ProxyCreationEnabled = false;
        }
        
        public Task<int> Insert(T obj)
        {
            _objectSet.Add(obj);
            return _dbContext.SaveChangesAsync();
        }

        public int Update(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            return _dbContext.SaveChanges();
        }

        public int Delete(T obj)
        {
            _objectSet.Remove(obj);
            return _dbContext.SaveChanges();
        }

        public T SelectById(int? id)
        {
            return _objectSet.Find(id);
        }

        public List<T> SelectAll()
        {
            return _objectSet.ToList();
        }
        
        public Task<int> SaveItRepo()
        {
            return _dbContext.SaveChangesAsync();
        }
        

        public int DeleteById(int? id)
        {
            var a = _objectSet.Find(id);
            _objectSet.Remove(a);
            return _dbContext.SaveChanges();
        }

        

    }
}
