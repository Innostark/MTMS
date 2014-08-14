using EmailTemplating.Models;
using EmailTemplating.Repository.Base;
using EmailTemplating.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailTemplating.Repository.Repositories
{
    public class EmailRepository : BaseRepository<Email>, IEmailRepository
    {
          #region Contructor
        /// <summary>
        /// Constructor
        /// </summary>
        public EmailRepository(BaseDbContext dbContext) : base(dbContext)
        {
        }
        #endregion
        #region Protected
        /// <summary>
        /// MergeTagVars
        /// </summary>
        protected override IDbSet<Email> DbSet
        {
            get { return db.Emails; }
        }
        #endregion


        public IEnumerable<Email> GetAllEmails()
        {
            return DbSet.Include(x => x.Template);
        }

        //public Email FindEmails(int id)
        //{
        //    //return DbSet.Include(m => m.Template).FirstOrDefault(m => m.TemplateID == id);
            
        //}
    }
}
