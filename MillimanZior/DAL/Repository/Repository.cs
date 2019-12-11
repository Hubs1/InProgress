using System;
using System.Collections.Generic;
using System.Data.Entity;//DbSet
using System.Linq;
using System.Linq.Expressions;//Expression
using System.Reflection;//PropertyInfo
using System.Text;
using System.Threading.Tasks;
namespace DAL
{
    public class Repository<T> where T : class
    {
        internal ZiorEntities _objDbContext;
        internal DbSet<T> _objDbSet;
        public Repository(ZiorEntities DbContext)
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
        /// <summary>
        /// get generic method to order
        /// </summary>
        /// <remarks>Author: Henry Heeralal</remarks>
        /// <param name="orderColumn">column against ordering</param>
        /// <param name="orderType">ordering type</param>
        /// <returns>result after ordering</returns>
        public virtual Func<IQueryable<T>, IOrderedQueryable<T>> GetOrderBy(string orderColumn, string orderType)
        {
            Type typeQueryable = typeof(IQueryable<T>);
            ParameterExpression argQueryable = Expression.Parameter(typeQueryable, "p");
            var outerExpression = Expression.Lambda(argQueryable, argQueryable);
            string[] props = orderColumn.Split('.');
            IQueryable<T> query = new List<T>().AsQueryable<T>();
            Type type = typeof(T);
            ParameterExpression arg = Expression.Parameter(type, "x");
            Expression expr = arg;
            foreach (string prop in props)
            {
                PropertyInfo pi = type.GetProperty(prop, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                expr = Expression.Property(expr, pi);
                type = pi.PropertyType;
            }
            LambdaExpression lambda = Expression.Lambda(expr, arg);
            string methodName = orderType == "asc" ? "OrderBy" : "OrderByDescending";
            MethodCallExpression resultExp =
                Expression.Call(typeof(Queryable), methodName, new Type[] { typeof(T), type }, outerExpression.Body, Expression.Quote(lambda));
            var finalLambda = Expression.Lambda(resultExp, argQueryable);
            return (Func<IQueryable<T>, IOrderedQueryable<T>>)finalLambda.Compile();
        }
    }
}
