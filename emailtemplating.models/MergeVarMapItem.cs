using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailTemplating.Models
{
    public class MergeVarMapItem
    {
        [Key]
        [Required]
        public int ID { get; set; }

        [Required]
        public string VariableName { get; set; }

        [Required]
        public string PropertyName { get; set; }
    }
}
