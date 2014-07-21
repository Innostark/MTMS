using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EmailTemplating.Models;
using System.Linq.Expressions;

namespace EmailTemplating.Process
{
    public static class MessageProcessingExtensions
    {

        public static void ProcessMessageRecipients<T>(this Message message, List<T> dataset, Func<Recipient, List<T>, T> filter)
        {
            if (message.Template == null) { throw new ArgumentNullException("message.Template"); }
            message.ProcessMessageRecipients<T>(dataset, filter, message.Template.TagMap);
        }
        public static void ProcessMessageRecipients<T>(this Message message, List<T> dataset, Func<Recipient, List<T>, T> filter, Template template)
        {
            if (template == null) { throw new ArgumentNullException("template"); }
            message.ProcessMessageRecipients<T>(dataset, filter, template.TagMap);
        }
        public static void ProcessMessageRecipients<T>(this Message message, List<T> dataset, Func<Recipient, List<T>, T> filter, MergeVarMap map)
        {
            if (message == null) { throw new ArgumentNullException("message"); }
            if (dataset == null) { throw new ArgumentNullException("dataset"); }
            if (map == null) { throw new ArgumentNullException("map"); }

            foreach (var recip in message.Recipients)
            {
                recip.ProcessMergeTags(filter(recip, dataset), map.MapItems);
            }
        }



        public static List<System.Net.Mail.MailMessage> ToNetMailMessages(this Message message)
        {
            if (message == null) { throw new ArgumentNullException("message"); }

            var ret = new List<System.Net.Mail.MailMessage>(message.Recipients.Count);
            foreach (var recipient in message.Recipients)
            {
                var netMsg = new System.Net.Mail.MailMessage()
                {
                    From = message.From.ToNetMailAddress(),
                    Subject = message.Subject,
                    IsBodyHtml = false                   
                };
                ret.Add(netMsg);

                netMsg.To.Add(((MessageAddress)recipient).ToNetMailAddress());
                if (message.Template == null || !string.IsNullOrWhiteSpace(message.Body))
                {
                    netMsg.Body = message.Body;
                }
                else
                {
                    netMsg.Body = message.Template.Body;
                }

                if (recipient.MergeTags != null)
                {
                    foreach (var tag in recipient.MergeTags)
                    {
                        string code = tag.ToMergeCode();
                        if (code != null && netMsg.Body.Contains(code))
                        {
                            netMsg.Body = netMsg.Body.Replace(code, tag.Value);
                        }
                    }
                }
            }

            return ret;
        }




    }
}
