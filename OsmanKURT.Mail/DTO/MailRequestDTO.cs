using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace OsmanKURT.Mail
{
    public class MailRequestDTO
    {
        public List<MailAddress> To { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public List<MailAddress> CC { get; set; }
        public List<MailAddress> BCC { get; set; }
    }
}
