using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EmailTemplating.Models
{
    public class Recipient: MessageAddress
    {
        [Required]
        public int RecipientID { get; set; }


        public List<MergeTagVar> MergeTags { get; set; }
    }
}
