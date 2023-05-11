using GenericRepositoryUnitOfWork.Core.Constants;
using GenericRepositoryUnitOfWork.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GenericRepositoryUnitOfWork.Core.Interface
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        T GetByIdAsync(int id);
        T Update(T model);
        T Delete(T model);
        IEnumerable<T> DeleteRange(IEnumerable<T> models);
        T Add(T model);
        IEnumerable<T> AddRange(IEnumerable<T> models);
        Task<T> MatchFirst(Expression<Func<T,bool>> match);
        Task<IEnumerable<T>> MatchAll(Expression<Func<T, bool>> match, int? take, int? skip, Expression<Func<T, object>> orderby = null, string orderdirection = OrderBy.Ascending);
        int Count();
        int CountWithMatch(Expression<Func<T, bool>> match);
        T Attach(T model);
        IEnumerable<T> ContainsIds(IEnumerable<T> models);
        


    }
}
