using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailTemplating.Models
{
    public class MergeTagVar
    {
        [Key]
        [Required]
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Value { get; set; }
    }
}
