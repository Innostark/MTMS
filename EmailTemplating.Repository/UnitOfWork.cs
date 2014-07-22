using System;
using System.Configuration;
using EmailTemplating.Repository.Base;
using EmailTemplating.Repository.Repositories;

namespace EmailTemplating.Repository
{
    /// <summary>
    /// Unit of Work 
    /// </summary>
    public class UnitOfWork : IDisposable
    {
        #region Private 
        readonly string connectionString = ConfigurationManager.ConnectionStrings["EmailTemplatingConnectionString"].ConnectionString;
        private readonly BaseDbContext baseDbContext;
        private MessageRepository messageRepository;
        private bool disposed;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    baseDbContext.Dispose();
                }
            }
            disposed = true;
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public UnitOfWork()
        {
            baseDbContext = new BaseDbContext(connectionString);
        }
        #endregion

        #region Public
        /// <summary>
        /// Message Repository
        /// </summary>
        public MessageRepository MessageRepository
        {
            get { return messageRepository ?? (messageRepository = new MessageRepository(baseDbContext)); }
        }
        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
