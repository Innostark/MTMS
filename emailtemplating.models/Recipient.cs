using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EmailTemplating.Models
{
    public class Recipient: MessageAddress
    {
        public List<MergeTagVar> MergeTags { get; set; }
    }
}
