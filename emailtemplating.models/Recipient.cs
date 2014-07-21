using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailTemplating.Models
{
    public class Recipient: MessageAddress
    {
        public List<MergeTagVar> MergeTags { get; set; }
    }
}
