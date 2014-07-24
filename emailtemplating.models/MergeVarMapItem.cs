using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmailTemplating.Models
{
    public class MergeVarMapItem
    {
        [Key]
        [Required]
        public int MergeVarMapItemID { get; set; }

        [Required]
        public string VariableName { get; set; }

        [Required]
        public string PropertyName { get; set; }

        [Required]        
        public int MergeVarMapID { get; set; }
        
        [ForeignKey("MergeVarMapID")]
        public MergeVarMap MergeVarMap { get; set; }        
    }
}