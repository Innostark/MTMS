using System.Linq;
using EmailTemplating.Models;
namespace EmailTemplating.Repository.Interfaces
{
    /// <summary>
    /// IMessage Repository
    /// </summary>
    public interface IMessageRepository : IBaseRepository<Message, int>
    {
        /// <summary>
        /// Get All Message Sorted
        /// </summary>
        /// <returns></returns>
        IQueryable<Message> GetAllMessageSorted();

        /// <summary>
        /// Find By Id
        /// </summary>
        Message FindById(int id);
    }
}
