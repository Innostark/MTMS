using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailTemplating.Models
{
    public class Template
    {
        [Key]
        [Required]
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Body { get; set; }

        public int TagMapID { get; set; }

        public virtual MergeVarMap TagMap { get; set; }
    }
}
