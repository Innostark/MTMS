using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using EmailTemplating.Models;

namespace EmailTemplating.Repository.Base
{
    /// <summary>
    /// Base Db Context class 
    /// </summary>
    public class BaseDbContext : DbContext
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public BaseDbContext(string connectionString) : base(connectionString)
        {            
        }
        #endregion
        #region Protected
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            Configuration.LazyLoadingEnabled = true;
        }
        #endregion 

        #region Public
        /// <summary>
        /// Messages
        /// </summary>
        public DbSet<Message> Messages { get; set; }

        /// <summary>
        /// Merge Var Maps
        /// </summary>
        public DbSet<MergeVarMap> MergeVarMaps { get; set; }

        /// <summary>
        /// Merge Tag Vars 
        /// </summary>
        public DbSet<MergeTagVar> MergeTagVars { get; set; }

        /// <summary>
        /// Merge Var Map Items
        /// </summary>
        public DbSet<MergeVarMapItem> MergeVarMapsItmes { get; set; }

        /// <summary>
        /// Message Addresses
        /// </summary>
        public DbSet<MessageAddress> MessageAddresses { get; set; }

        /// <summary>
        /// Templates
        /// </summary>
        public DbSet<Template> Templates { get; set; } 
        /// <summary>
        /// Recipients
        /// </summary>
        public DbSet<Recipient> Recipients { get; set; } 
        #endregion
    }


}
