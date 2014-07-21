using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailTemplating.Models
{
    public class MessageAddress
    {
        [Required]
        [Display(Name="Recipient's Name")]
        public string DisplayName { get; set; }

        [Required]
        [Display(Name="Email Address")]
        public string Address { get; set; }

    }
}
