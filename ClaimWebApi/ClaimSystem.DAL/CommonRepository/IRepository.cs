using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ClaimSystem.DAL.CommonRepository
{
    public interface IRepository<TEntity> where TEntity : class
    {

        Task<int> Insert(TEntity entity);

        int Update(TEntity entity);

        int Delete(TEntity obj);

        int DeleteById(int? id);

        ///void Delete(Expression<Func<TEntit> where);

        TEntity SelectById(int? id);

        List<TEntity> SelectAll();

        // Get an entity using delegate
        //TEntity Get(Expression<Func<TEntity, bool>> where);

        // Gets entities using delegate
        //IEnumerable<TEntity> GetMany(Expression<Func<TEntity, bool>> where);

    }
}
