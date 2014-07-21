using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EmailTemplating.Models;

namespace EmailTemplating.Process
{
    public class SmtpMailDebuggingClient
    {
        public string DeliveryPath { get; set; }

        public SmtpMailDebuggingClient(string deliveryPath)
        {
            this.DeliveryPath = deliveryPath;
        }

        #region >> Build Message <<

        public Message BuildMessage(MessageAddress from, List<MessageAddress> to, string subject, string body)
        {
            return new Message()
            {
                From = from,
                Recipients = to.Select(m => new Recipient() { Address = m.Address, DisplayName = m.DisplayName }).ToList(),
                Subject = subject,
                Body = body
            };
        }
        public Message BuildMessage<T>(MessageAddress from, List<MessageAddress> to, string subject, string body, List<T> dataset, Func<Recipient, List<T>, T> filter, MergeVarMap map)
        {
            var ret = BuildMessage(from, to, subject, body);
            ret.ProcessMessageRecipients<T>(dataset, filter, map);
            return ret;
        }



        public Message BuildMessage(MessageAddress from, List<MessageAddress> to, string subject, Template template)
        {
            return new Message()
            {
                From = from,
                Recipients = to.Select(m => new Recipient() { Address = m.Address, DisplayName = m.DisplayName }).ToList(),
                Subject = subject,
                Body = template.Body
            };
        }
        public Message BuildMessage<T>(MessageAddress from, List<MessageAddress> to, string subject, Template template, List<T> dataset, Func<Recipient, List<T>, T> filter)
        {
            var ret = BuildMessage(from, to, subject, template);
            ret.ProcessMessageRecipients<T>(dataset, filter, template.TagMap);
            return ret;
        }

        #endregion


        #region >> Send Email <<

        //assumes message has been process and all recipients have necessary mergetag filled
        public string Send(Message message)
        {
            if (message == null) { throw new ArgumentNullException("message"); }
            if (message.From == null) { throw new ArgumentNullException("message.from"); }
            if (message.Recipients == null) { throw new ArgumentNullException("message.recipients"); }
            if (message.Recipients.Count == 0) { throw new ArgumentException("missing message.recipients"); }
            if (string.IsNullOrWhiteSpace(message.Subject)) { throw new ArgumentException("missing message.subject"); }

            //housekeeping (be sure the filesystem is ready)
            if (!System.IO.Directory.Exists(this.DeliveryPath))
            {
                throw new Exception("the email delivery folder does not exist!"); 
            }
            var path = this._CustomizedDeliveryPath(message.From);
            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);
            }

            var timestamp = DateTime.Now;
            var netMessages = message.ToNetMailMessages();  //create the individual email messages
            foreach (var item in netMessages)
            {
                string fullfilename = path + _CustomizedFilename(item.To.First(), timestamp);
                System.IO.File.WriteAllText(fullfilename, _NetMessageToText(item));
            }

            return path;
        }

        #endregion

        #region >> HELPERS <<

        private string _CustomizedDeliveryPath(MessageAddress from)
        {
            var path = this.DeliveryPath + (this.DeliveryPath.EndsWith(System.IO.Path.DirectorySeparatorChar.ToString()) ? "" : System.IO.Path.DirectorySeparatorChar.ToString());

            var folder = from.Address;
            var invalidChars = System.IO.Path.GetInvalidPathChars();
            char altCH = '_';

            //just to be certain
            if (invalidChars.Contains(altCH)) { throw new Exception("Guess what? The replacement for invalid path characters is invalid, too!"); }

            foreach (var ch in invalidChars)
            {
                if (folder.Contains(ch)) { folder = folder.Replace(ch, altCH); }
            }

            return path + folder + System.IO.Path.DirectorySeparatorChar;
        }

        private string _CustomizedFilename(System.Net.Mail.MailAddress to, DateTime timestamp)
        {
            var filename = string.Format("[{0}]{1}", timestamp.ToString("yyyyMMdd-hhmm"), to.Address);
            var invalidChars = System.IO.Path.GetInvalidFileNameChars();
            char altCH = '_';

            //just to be certain
            if (invalidChars.Contains(altCH)) { throw new Exception("Guess what? The replacement for invalid filename characters is invalid, too!"); }

            foreach (var ch in invalidChars)
            {
                if (filename.Contains(ch)) { filename = filename.Replace(ch, altCH); }
            }

            return filename + ".txt";
        }


        private string _NetMessageToText(System.Net.Mail.MailMessage netMsg)
        {
            //no error checking
            var sw = new System.IO.StringWriter();
            sw.WriteLine("To: {0}", _NetAddessToString(netMsg.To.First()));
            sw.WriteLine("From: {0}", _NetAddessToString(netMsg.From));
            sw.WriteLine("Date: {0}", DateTime.Now.ToString());
            sw.WriteLine("Subject: {0}", netMsg.Subject);
            sw.WriteLine("........... Start Messge Body .............");
            sw.WriteLine(netMsg.Body);
            sw.WriteLine("...........  End Messge Body  .............");

            return sw.ToString();
        }


        private string _NetAddessToString(System.Net.Mail.MailAddress netAddress)
        {
            //no error checking
            if (string.IsNullOrWhiteSpace(netAddress.DisplayName)) { return netAddress.Address; }
            else { return string.Format("{0} [{1}]", netAddress.DisplayName, netAddress.Address); }
        }
        #endregion

    }
}
