using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EmailTemplating.Models;

namespace EmailTemplating.Web.Models
{
    public class TemplateViewModel
    {
        public Template Template { get; set; }
        public IEnumerable<MergeVarMap> MergeVarMaps { get; set; }
        public string SelectedMergeVarMap { get; set; }
    }
}