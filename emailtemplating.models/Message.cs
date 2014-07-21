using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailTemplating.Models
{
    public class Message
    {
        [Required]
        public int ID { get; set; }

        [Required]
        public MessageAddress From { get; set; }

        [Required]
        public List<Recipient> Recipients { get; set; }

        [Required]
        public string Subject { get; set; }

        [Required]
        public string Body { get; set; }

        public Nullable<int> TemplateID { get; set; }
        public virtual Template Template { get; set; }

        //house keeping

        [Required]
        public bool IsProcessed { get; set; }

        [Required]
        public DateTime CreateDate { get; set; }
        [Required]
        public DateTime UpdateDate { get; set; }

        //constructor

        public Message()
        {
            this.IsProcessed = false;
            this.CreateDate = DateTime.Now;
            this.UpdateDate = DateTime.Now;
        }
    }
}
