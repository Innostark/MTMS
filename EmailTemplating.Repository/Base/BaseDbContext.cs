using System.ComponentModel.DataAnnotations.Schema;
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
        public BaseDbContext(string connectionString) : base(connectionString)
        {            
        }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            Configuration.LazyLoadingEnabled = true;
        }

        #region Public 

        public DbSet<Message> Messages { get; set; }

        public DbSet<MessageAddress> MessageAddresses { get; set; }
        
        #endregion
    }


}
