using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailTemplating.Models
{
    public class MergeVarMap
    {
        [Required]
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        public List<MergeVarMapItem> MapItems { get; set; }

        public List<string> AvailableMergeTags 
        {
            get
            {
                if (this.MapItems == null) { return new List<string>(); }
                else
                {
                    return this.MapItems.Select(m => m.VariableName).ToList();
                }
            }
        }
    }
}
