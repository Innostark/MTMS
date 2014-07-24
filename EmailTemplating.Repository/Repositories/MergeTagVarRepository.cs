using System.Data.Entity;
using EmailTemplating.Models;
using EmailTemplating.Repository.Base;
using EmailTemplating.Repository.Interfaces;

namespace EmailTemplating.Repository.Repositories
{
    /// <summary>
    /// Merge Tag Var Repository
    /// </summary>
    public sealed class MergeTagVarRepository : BaseRepository<MergeTagVar>, IMergeTagVarRepository
    {
        #region Contructor
        /// <summary>
        /// Constructor
        /// </summary>
        public MergeTagVarRepository(BaseDbContext dbContext) : base(dbContext)
        {
        }
        #endregion

        #region Protected
        /// <summary>
        /// MergeTagVars
        /// </summary>
        protected override IDbSet<MergeTagVar> DbSet
        {
            get { return db.MergeTagVars; }
        }
        #endregion
    }
}
