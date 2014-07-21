using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EmailTemplating.Models;

namespace EmailTemplating.Process
{
    public static class RecipientProcessingExtensions
    {
        public static void ProcessMergeTags(this Recipient recipient, object model, List<MergeVarMapItem> mergeTags)
        {
            if (recipient == null) { throw new ArgumentNullException("model"); }
            recipient.MergeTags = new List<MergeTagVar>();  //reset
            foreach (var tag in mergeTags)
            {
                object value;
                try
                {
                    value = model.GetType().GetProperty(tag.PropertyName).GetValue(model, null);
                }
                catch (Exception)
                {
                    value = "?problem property - " + tag.PropertyName + "?";
                }
                recipient.MergeTags.Add(new MergeTagVar()
                {
                    Name = tag.VariableName,
                    Value = value == null ? (string)null : value.ToString()
                });
            }
        }
        
    }
}
