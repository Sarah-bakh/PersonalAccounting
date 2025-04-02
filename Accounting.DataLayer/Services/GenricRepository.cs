using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.DataLayer.Services
{
    public class GenricRepository<TEnitity> where TEnitity : class
    {
        private Accounting_DBEntities _db;
        private DbSet<TEnitity> _dbSet;

        public GenricRepository(Accounting_DBEntities db)  // constractor method 
        {
            _db = db;
            _dbSet = db.Set<TEnitity>();
        }


        // find

        public virtual IEnumerable<TEnitity> GET(Expression<Func<TEnitity,bool>> WHERE=null)
        {
            IQueryable<TEnitity> query = _dbSet;
            if (WHERE!=null)
            {
                query= query.Where( WHERE );    
            }
            return query;
        }
        public virtual TEnitity GetById(object id)
        {
            return _dbSet.Find(id);
        }

        //Add
        public virtual void Insert(TEnitity enitity)
        {
            _dbSet.Add(enitity);
        }


        // Update

        public virtual  void Update(TEnitity enitity)
        {
            _dbSet.Attach(enitity);
            _db.Entry(enitity).State = EntityState.Modified;
        }

        // Delete
        public virtual void Delete(TEnitity enitity)
        {
            if (_db.Entry(enitity).State==EntityState.Detached)
            {
                _dbSet.Attach(enitity);
            }
            _dbSet.Remove(enitity);
        }

        public virtual void Delete(object id)
        {
            var entety = GetById(id);
            Delete(entety); 
        }
    } 
    
}
