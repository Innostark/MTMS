using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmailTemplating.Models
{
    public class Message
    {
        #region Persisted Properties
        [Key]
        [Required]
        public int MessageID { get; set; }
        
        public int? MessageAddressID { get; set; }

        [Required]
        public string Subject { get; set; }
        
        [Required]
        public string Body { get; set; }

        public int? TemplateID { get; set; }

        [Required]
        public bool IsProcessed { get; set; }

        [Required]
        public DateTime CreateDate { get; set; }
        [Required]
        public DateTime UpdateDate { get; set; }

        #endregion
        
        #region Reference Entities

        [Required]
        [ForeignKey("MessageAddressID")]
        public MessageAddress From { get; set; }

        [Required]
        public List<Recipient> Recipients { get; set; }

        
        [ForeignKey("TemplateID")]
        public virtual Template Template { get; set; }

        #endregion
        public Message()
        {
            IsProcessed = false;
            CreateDate = DateTime.Now;
            UpdateDate = DateTime.Now;
        }
    }
}
