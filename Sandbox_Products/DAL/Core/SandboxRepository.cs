using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Linq.Expressions;

namespace Sandbox_Products.DAL.Core
{
    public class SandboxRepository
    {
        protected DbContext _context;

        public SandboxRepository(DbContext context)
        {
            this._context = context;
        }

        public IQueryable<T> Get<T>() where T : class
        {
            return _context.Set<T>();
        }

        public T Get<T>(Expression<Func<T, bool>> condition) where T : class
        {
            return this._context.Set<T>().FirstOrDefault(condition);
        }

        public void Add<T>(T item) where T : class
        {
            this._context.Set<T>().Add(item);
        }

        public void Update<T>(T item) where T : class
        {
            this._context.Entry(item).State = EntityState.Modified;
        }

        public void Delete<T>(T item) where T : class
        {
            this._context.Set<T>().Remove(item);
        }
    }
}
