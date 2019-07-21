using System;
using System.Collections.Generic;
using System.Text;

namespace OsmanKURT.Mail
{
    public class MailManager : IMailManager
    {
        public MailResult Send(MailRequestDTO mailRequestDTO)
        {
            MailProviderType mailProvider = MailProviderType.SMTP;
            return MailFactory.Instance.Create(mailProvider).Send(mailRequestDTO, mailProvider);
        }
    }
}
