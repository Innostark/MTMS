using System.Data.Entity;
using EmailTemplating.Models;
using EmailTemplating.Repository.Base;
using EmailTemplating.Repository.Interfaces;

namespace EmailTemplating.Repository.Repositories
{
    /// <summary>
    /// Merge Var Map Items Repository
    /// </summary>
    public sealed class MergeVarMapItemRepository : BaseRepository<MergeVarMapItem>, IMergeVarMapItemRepository
    {
        #region Constructor
        /// <summary>
        /// Constuctor
        /// </summary>
        public MergeVarMapItemRepository(BaseDbContext dbContext) : base(dbContext)
        {
        }
        #endregion
        #region Protected 
        /// <summary>
        /// MergeVarMapsItems
        /// </summary>
        protected override IDbSet<MergeVarMapItem> DbSet
        {
            get { return db.MergeVarMapsItmes; }
        }
        #endregion
    }
}
