using EmailTemplating.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace EmailTemplating.Web.Models
{
    public class ParkingViewModel
    {
        public List<SampleData.Employee> dataset { get; set; }
        public IEnumerable<Email> Emails { get; set; }
        public string SelectedEmail { get; set; }
        public bool IsPreview { get; set; }
    
   
    }
}