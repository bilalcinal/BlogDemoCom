using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Data.Services.Interfaces
{
    public interface IRepository<T> where T : class
    {
        T Find(object id);
        IQueryable<T> Where(Expression<Func<T, bool>> Filter = null);
        IQueryable<T> Include(Expression<Func<T, bool>> Filter = null);
        bool RemoveById(object id);
        bool Remove(T entity);
        int Remove(Expression<Func<T, bool>> Filter = null);
        int RemoveRange(Expression<Func<T, bool>> Filter = null);
        T RemoveByIdandGet(object id);
        bool Update(T entity);
        bool updateRange();
        bool Add(T entity, Expression<Func<T, bool>> InsertControl = null);
        List<T> ToList();
        int CountByWhere(Expression<Func<T, bool>> Filter = null);
        T FirstOrDefault(Expression<Func<T, bool>> Filter = null);        
    }
}