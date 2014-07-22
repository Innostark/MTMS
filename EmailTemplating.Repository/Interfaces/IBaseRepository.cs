
namespace EmailTemplating.Repository.Interfaces
{
    /// <summary>
    /// Interface for the Base Repository
    /// </summary>
    public interface IBaseRepository<TDomainClass, TKeyType>
        where TDomainClass : class
    {
        /// <summary>
        /// Find Entiry base Repository 
        /// </summary>
        TDomainClass Find(TKeyType id);

        /// <summary>
        /// Save Changes in the context
        /// </summary>
        void SaveChanges();

        /// <summary>
        /// Delete an entry
        /// </summary>
        void Delete(TDomainClass instance);

        /// <summary>
        /// Add an entry
        /// </summary>       
        void Add(TDomainClass instance);
    }
}
