using System;
using System.Collections.Generic;
using System.Text;

namespace OsmanKURT.Mail
{
    public interface IMailManager
    {
        MailResult Send(MailRequestDTO mailRequestDTO);
    }
}
