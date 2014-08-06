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
        [StringLength(250, ErrorMessage = "Variable name must not exceed 250 characters")]
        public string VariableName { get; set; }

        [Required]
        [StringLength(250, ErrorMessage = "Property Name must not exceed 250 characters")]
        public string PropertyName { get; set; }

        [Required]
        [ForeignKey("MergeVarMap")]
        public int MergeVarMapID { get; set; }
        
        [ForeignKey("MergeVarMapID")]
        public MergeVarMap MergeVarMap { get; set; }        
    }
}