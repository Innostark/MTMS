using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace EmailTemplating.Models
{
    public class Template
    {
        [Key]
        [Required]
        public int TemplateID { get; set; }

        [Required]
        [StringLength(250, ErrorMessage = "Template name must not exceed 250 characters")]
        public string Name { get; set; }

        [Required]
        [StringLength(1000, ErrorMessage = "Template description must not exceed 1000 characters")]
        public string Description { get; set; }

        [Required]        
        public string Body { get; set; }

        [ForeignKey("TagMap")]
        public int MergeVarMapID { get; set; }
        
        [ForeignKey("MergeVarMapID")]
        public virtual MergeVarMap TagMap { get; set; }
        
    }
}
