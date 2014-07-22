using System.Data.Entity;
using System.Linq;
using EmailTemplating.Models;
using EmailTemplating.Repository.Base;
using EmailTemplating.Repository.Interfaces;

namespace EmailTemplating.Repository.Repositories
{
    /// <summary>
    /// Message Repository
    /// </summary>
    public class MessageRepository : BaseRepository<Message> , IMessageRepository
    {
        #region Constructor
        internal MessageRepository(BaseDbContext baseDbContext) : base(baseDbContext)
        {            
        }
        #endregion
        #region Protected
        /// <summary>
        /// DbSet
        /// </summary>
        protected override IDbSet<Message> DbSet
        {
            get { return db.Messages; }
        }
        #endregion

        #region Public
        /// <summary>
        /// Get all messages  //TODO: Sample Method
        /// </summary>
        public IQueryable<Message> GetAllMessageSorted()
        {
            return DbSet.OrderBy(message => message.CreateDate);
        }

        /// <summary>
        /// Finds a message by the given Id //TODO: Sample Method 
        /// </summary>
        public Message FindById(int id)
        {
            return DbSet.Include(message => message.From).Include(message => message.Recipients).Include(message => message.Template).FirstOrDefault(message => message.ID == id);
        }
        #endregion
    }
}
