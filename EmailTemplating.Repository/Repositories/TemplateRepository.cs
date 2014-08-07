using System.Collections.Generic;
using System.Data.Entity;
using EmailTemplating.Models;
using EmailTemplating.Repository.Base;
using EmailTemplating.Repository.Interfaces;

namespace EmailTemplating.Repository.Repositories
{
    /// <summary>
    /// Template Repository
    /// </summary>
    public sealed class TemplateRepository : BaseRepository<Template>, ITemplateRepository
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public TemplateRepository(BaseDbContext dbContext)
            : base(dbContext)
        {
        }
        #endregion
        #region Protected
        /// <summary>
        /// Templates
        /// </summary>
        protected override IDbSet<Template> DbSet
        {
            get { return db.Templates; }
        }
        #endregion

        public IEnumerable<Template> GetAllTemplates()
        {
            return DbSet.Include(x => x.TagMap);
        } 
    }
}
