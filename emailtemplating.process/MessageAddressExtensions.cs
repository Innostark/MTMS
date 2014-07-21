using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EmailTemplating.Models;

namespace EmailTemplating.Process
{
    public static class MessageAddressExtensions
    {
        public static System.Net.Mail.MailAddress ToNetMailAddress(this MessageAddress model)
        {
            if (model == null) { return null; }
            //else
            return new System.Net.Mail.MailAddress(model.Address, model.DisplayName);
        }
    }
}
