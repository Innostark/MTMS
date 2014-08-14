using EmailTemplating.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailTemplating.Repository.Interfaces
{
   public interface IEmailRepository : IBaseRepository<Email, int>
    {
        IEnumerable<Email> GetAllEmails();
        //Email FindEmails(int id);

    }
}
