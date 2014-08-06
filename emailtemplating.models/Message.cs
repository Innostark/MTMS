using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmailTemplating.Models
{
    public class Message
    {
        #region Persisted Properties

        public int TemplateID { get; set; }
        
        public string Subject { get; set; }
        
        public string Body { get; set; }

        public bool IsProcessed { get; set; }

        [Required]
        public DateTime CreateDate { get; set; }
        [Required]
        public DateTime UpdateDate { get; set; }

        #endregion
        
        #region Reference Entities

        public MessageAddress From { get; set; }

        public List<Recipient> Recipients { get; set; }
        
        public Template Template { get; set; }

        #endregion
        public Message()
        {
            IsProcessed = false;
            CreateDate = DateTime.Now;
            UpdateDate = DateTime.Now;
        }
    }
}
