using System.ComponentModel.DataAnnotations;

namespace EmailTemplating.Models
{
    public class MessageAddress
    {
        [Key]
        [Required]
        public int MessageAddressID { get; set; }

        [Required]
        [Display(Name="Recipient's Name")]
        public string DisplayName { get; set; }

        [Required]
        [Display(Name="Email Address")]
        public string Address { get; set; }

    }
}
