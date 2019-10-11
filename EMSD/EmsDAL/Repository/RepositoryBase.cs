using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Data.Entity;
using System.Data;
using System.Threading.Tasks;

namespace EmsDAL
{
    public class RepositoryBase<T> where T : class
    {
        internal EmployeeDepartment _objDbContext;     
        internal DbSet<T> _objDbSet;

        public RepositoryBase(EmployeeDepartment DbContext)
        {
            this._objDbContext = DbContext;
            this._objDbSet = DbContext.Set<T>();
        }

        public virtual List<T> Get(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<T> query = _objDbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public virtual void Add(T entity)
        {
            _objDbSet.Add(entity);
        }

        public virtual void Delete(T entity)
        {
            _objDbSet.Attach(entity);
            _objDbSet.Remove(entity);
        }

        public virtual void Delete(Expression<Func<T, bool>> where)
        {
            IEnumerable<T> objects = _objDbSet.Where<T>(where).AsEnumerable();
            foreach (T obj in objects)
                _objDbSet.Remove(obj);
        }

        public virtual T GetById(long id)
        {
            return _objDbSet.Find(id);
        }

        public virtual T GetById(string id)
        {
            return _objDbSet.Find(id);
        }

        public virtual IEnumerable<T> GetAll()
        {
            return _objDbSet.ToList();
        }
        public virtual IEnumerable<T> GetMany(Expression<Func<T, bool>> where)
        {
            return _objDbSet.Where(where).ToList();
        }
        public T Get(Expression<Func<T, bool>> where)
        {
            return _objDbSet.Where(where).FirstOrDefault<T>();
        }
    }
}