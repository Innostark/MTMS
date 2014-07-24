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
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Body { get; set; }

        public int MergeVarMapID { get; set; }
        
        [ForeignKey("MergeVarMapID")]
        public virtual MergeVarMap TagMap { get; set; }
        
    }
}
