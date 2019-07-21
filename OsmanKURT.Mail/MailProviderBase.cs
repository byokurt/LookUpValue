using System;
using System.Collections.Generic;
using System.Text;

namespace OsmanKURT.Mail
{
    public abstract class MailProviderBase
    {
        protected MailProviderType _MailProvider;

        public MailProviderBase()
        {

        }

        protected abstract MailResult SendMail(MailRequestDTO mailRequestDTO);

        public MailResult Send(MailRequestDTO mailRequestDTO, MailProviderType mailProvider)
        {
            _MailProvider = mailProvider;
            return SendMail(mailRequestDTO);
        }
    }
}
