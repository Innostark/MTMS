using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace EmailTemplating.Models
{
    public class MergeVarMap
    {
        #region Persisted Properties
        [Key]
        [Required]
        public int MergeVarMapID { get; set; }


        [Required]
        [StringLength(250, ErrorMessage = "Merge Var Map name must not exceed 250 characters")]
        public string Name { get; set; }
        #endregion

        #region Reference

        public virtual List<MergeVarMapItem> MapItems { get; set; }

        #endregion

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
