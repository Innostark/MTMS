using EmailTemplating.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace EmailTemplating.Web.Models
{
    public class EmailViewModel
    {
        private int SelectedDbSourceID=0;

        public Email Email { get; set; }

        public IEnumerable<Template> Templates { get; set; }
        public string SelectedTemplate { get; set; }
        
        public int SelectedDbSource { get; set; }
        
            //Selected=(((int)v) == SelectedDbSourceID)
        public IEnumerable<SelectListItem> DbSources
        {
            get
            {
               var dbSources=Enum.GetValues(typeof(DbSource)).Cast<DbSource>().Select(v => new SelectListItem
                {
                    
                    Text = v.ToString(),
                    Value = ((int)v).ToString(),
                

                });
                return dbSources;
            }
        }
        public EmailViewModel()
        {
        }
        
   
    }
}