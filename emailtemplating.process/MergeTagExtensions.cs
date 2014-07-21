using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EmailTemplating.Models;

namespace EmailTemplating.Process
{
    public static class MergeTagExtensions
    {
        public static string ToMergeCode(this MergeTagVar tag)
        {
            if (tag == null || string.IsNullOrWhiteSpace(tag.Name)) { return null; }
            //else
            return string.Format("*|{0}|*", tag.Name.ToLower());
        }
    }
}
