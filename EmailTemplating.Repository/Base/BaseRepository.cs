using System;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using EmailTemplating.Repository.Interfaces;

namespace EmailTemplating.Repository.Base
{
    /// <summary>
    /// Base Repository
    /// </summary>
    public abstract class BaseRepository<TDomainClass> : IBaseRepository<TDomainClass, int>
        where TDomainClass : class 
    {    
        #region Protected
        /// <summary>
        /// Primary database set
        /// </summary>
        protected abstract IDbSet<TDomainClass> DbSet { get; }

        #endregion
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        protected BaseRepository(BaseDbContext dbContext)
        {
            
            db = dbContext;
        }

        #endregion
        #region Public
        /// <summary>
        /// base Db Context
        /// </summary>
        public BaseDbContext db;

        /// <summary>
        /// Find Entity by Id
        /// </summary>
        public virtual TDomainClass Find(int id)
        {
            return DbSet.Find(id);
        }
        /// <summary>
        /// Get All Entites 
        /// </summary>
        public virtual IQueryable<TDomainClass> GetAll()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Save Changes in the entities
        /// </summary>
        public void SaveChanges()
        {
            try
            {
                db.SaveChanges();
            }
            catch(Exception ex)
            {
                // ReSharper disable PossibleIntendedRethrow
                throw ex;
                // ReSharper restore PossibleIntendedRethrow
            }
        }

        /// <summary>
        /// Delete an entry
        /// </summary>
        public virtual void Delete(TDomainClass instance)
        {
            if (db.Entry(instance).State == EntityState.Detached)
            {
                DbSet.Attach(instance);
            }
            DbSet.Remove(instance);
        }
        /// <summary>
        /// Add an entry
        /// </summary>
        public virtual void Add(TDomainClass instance)
        {
            DbSet.Add(instance);
        }
        /// <summary>
        /// Add an entry
        /// </summary>
        public virtual void Update(TDomainClass instance)
        {
            DbSet.AddOrUpdate(instance);
        }
        #endregion
    }
}
