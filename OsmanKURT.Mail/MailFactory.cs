using System;
using System.Collections.Generic;
using System.Text;

namespace OsmanKURT.Mail
{
    public class MailFactory
    {
        private static readonly Lazy<MailFactory> _Instance = new Lazy<MailFactory>(() => new MailFactory());
        private MailFactory()
        {

        }

        public static MailFactory Instance
        {
            get
            {
                return _Instance.Value;
            }
        }

        public MailProviderBase Create(MailProviderType mailProvider)
        {
            switch (mailProvider)
            {
                case MailProviderType.SMTP:
                    return new SMTPMailProvider();
                default:
                    throw new Exception("Please select a corret mail provider type.");
            }
        }
    }
}
