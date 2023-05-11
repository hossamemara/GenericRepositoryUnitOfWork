using GenericRepositoryUnitOfWork.Core.Constants;
using GenericRepositoryUnitOfWork.Core.Interface;
using GenericRepositoryUnitOfWork.DAL.Context;
using GenericRepositoryUnitOfWork.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GenericRepositoryUnitOfWork.Core.Repository
{

    
    public class GenericRepository<T>: IGenericRepository<T> where T : class
    {
        #region Private Fields

        private readonly ApplicationDbContext _context;

        #endregion

        #region Constructor
        public GenericRepository(ApplicationDbContext context)
        {
            this._context = context;
        }

        #endregion

        #region Department Actions

        #region GetAllAsync
        public async Task<IEnumerable<T>> GetAllAsync()
            => await this._context.Set<T>().ToListAsync();

        #endregion

        #region GetByIdAsync
        public T GetByIdAsync(int id)
          => this._context.Set<T>().Find(id);
        #endregion

        #region MatchAll
        public async Task<IEnumerable<T>> MatchAll(Expression<Func<T, bool>> match, int? take, int? skip, Expression<Func<T, object>> orderby = null, string orderdirection = OrderBy.Ascending)
        {
            IQueryable<T> query = this._context.Set<T>();
            if (match != null)
            {
                query = query.Where(match);
            }



            if (orderby != null)
            {
                if (orderdirection == OrderBy.Ascending)
                {
                    query = query.OrderBy(orderby);

                }
                else
                {

                    query = query.OrderByDescending(orderby);
                }

            }
            if (skip.HasValue)
            {
                query = query.Skip(skip.Value);
            }
            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }


            return await query.ToListAsync();
        }
        #endregion

        #region MatchFirst
        public async Task<T> MatchFirst(Expression<Func<T, bool>> match)
            => await this._context.Set<T>().SingleOrDefaultAsync(match);
        #endregion

        #region Update
        public T Update(T model)
        {
            this._context.Update(model);

            return model;
        }
        #endregion

        #region Delete
        public T Delete(T model)
        {
            this._context.Remove(model);
            return model;
        }
        #endregion

        #region Add
        public T Add(T model)
        {
            _context.Set<T>().Add(model);
            return model;
        }
        #endregion

        #region Count
        public int Count()
        {
            return _context.Set<T>().Count();
        }
        #endregion

        #region CountWithMatch
        public int CountWithMatch(Expression<Func<T, bool>> match)
        {
            return _context.Set<T>().Count(match);
        }

        #endregion

        #region Attach
        public T Attach(T model)
        {
            _context.Set<T>().Attach(model);
            return model;
        }
        #endregion

        #region AddRange
        public IEnumerable<T> AddRange(IEnumerable<T> models)
        {
            _context.Set<T>().AddRange(models);
            return models;
        }
        #endregion

        #region DeleteRange
        public IEnumerable<T> DeleteRange(IEnumerable<T> models)
        {
            _context.Set<T>().RemoveRange(models);
            return models;
        }
        #endregion

        #region ContainsIds
        public IEnumerable<T> ContainsIds(IEnumerable<T> models)
        {

            return null;
        }
        #endregion

        #endregion



    }
}
