using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using EmailTemplating.Models;
using EmailTemplating.Repository.Base;
using EmailTemplating.Repository.Interfaces;

namespace EmailTemplating.Repository.Repositories
{
    /// <summary>
    /// Merge Var Mar Repository
    /// </summary>
    public class MergeVarMapRepository : BaseRepository<MergeVarMap>, IMergeVarMapRepository
    {
        #region Contructor
        /// <summary>
        /// Constructor
        /// </summary>
        internal MergeVarMapRepository(BaseDbContext dbContext) : base(dbContext)
        {
        }
        #endregion 
        #region Protected 
        /// <summary>
        /// DbSet
        /// </summary>
        protected override IDbSet<MergeVarMap> DbSet
        {
            get { return db.MergeVarMaps; }
        }


        #endregion

        public MergeVarMap FindByName(string name)
        {
            return DbSet.Include(m => m.MapItems).FirstOrDefault(m => m.Name == name);
        }

        public IEnumerable<MergeVarMap> GetAllMergeVarMap()
        {
            return DbSet.Include(x => x.MapItems);
        } 
    }
}
