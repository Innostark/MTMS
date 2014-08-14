using System;
using System.Configuration;
using EmailTemplating.Repository.Base;
using EmailTemplating.Repository.Interfaces;
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
        private IMergeVarMapRepository mergerVarMapRepository;
        private IMergeVarMapItemRepository mergerVarMapItemRepository;
        private ITemplateRepository templateRepository;
        private IMergeTagVarRepository mergeTagVarRepository;
        private IEmailRepository emailRepository;
        

        private bool disposed;
        /// <summary>
        /// Dispose the context
        /// </summary>
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
        /// Merge Var Map Repository
        /// </summary>
        public IMergeVarMapRepository MergerVarMapRepository
        {
            get { return mergerVarMapRepository ?? (mergerVarMapRepository = new MergeVarMapRepository(baseDbContext)); }
        }

        /// <summary>
        /// Merge Var Map Item Repository
        /// </summary>
        public IMergeVarMapItemRepository MergerVarMapItemRepository
        {
            get { return mergerVarMapItemRepository ?? (mergerVarMapItemRepository = new MergeVarMapItemRepository(baseDbContext)); }
        }
        /// <summary>
        /// Template Repository
        /// </summary>
        public ITemplateRepository TemplateRepository
        {
            get { return templateRepository ?? (templateRepository = new TemplateRepository(baseDbContext)); }
        }
        /// <summary>
        /// MergeTagVarRepository Repository
        /// </summary>
        public IMergeTagVarRepository MergeTagVarRepository
        {
            get { return mergeTagVarRepository ?? (mergeTagVarRepository = new MergeTagVarRepository(baseDbContext)); }
        }
        /// <summary>
        /// Email Repository
        /// </summary>
        public IEmailRepository EmailRepository
        {
            get { return emailRepository ?? (emailRepository = new EmailRepository(baseDbContext)); }
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
