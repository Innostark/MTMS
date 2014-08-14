using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailTemplating.Models
{
    public class Email
    {
        #region Persisted Properties
        [Key]
        [Required]
        public int EmailID { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Subject { get; set; }
        
        [Required]
        public int DbSource { get; set; }

        
        public int TemplateID { get; set; }
      
        [ForeignKey("TemplateID")]
        public Template Template { get; set; }


       
        #endregion
    }
}
