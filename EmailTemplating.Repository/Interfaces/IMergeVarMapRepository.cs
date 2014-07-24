using EmailTemplating.Models;

namespace EmailTemplating.Repository.Interfaces
{
    /// <summary>
    /// Interface for Merge Var Map Repository
    /// </summary>
    public interface IMergeVarMapRepository : IBaseRepository<MergeVarMap, int>
    {
        /// <summary>
        /// Find By Name
        /// </summary>
        MergeVarMap FindByName(string name);
    }
}
