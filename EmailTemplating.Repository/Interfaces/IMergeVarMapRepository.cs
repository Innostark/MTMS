using System.Collections.Generic;
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
        /// <summary>
        /// Get All Merge var Maps
        /// </summary>
        IEnumerable<MergeVarMap> GetAllMergeVarMap();
    }
}
