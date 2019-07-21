using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using System.Text;

namespace OsmanKURT.Mail
{
    public class SMTPMailProvider : MailProviderBase
    {
        public string _ReplayToAddress = string.Empty;
        public string _ReplayToName = string.Empty;
        public string _From = string.Empty;
        public string _Name = string.Empty;
        public string _SMTPServerIp = string.Empty;
        public int _SMTPServerPort = 0;

        public SMTPMailProvider()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();

            _ReplayToAddress = configuration["Mail:ReplayToAddress"];
            _ReplayToName = configuration["Mail:ReplayToName"];
            _From = configuration["Mail:From"];
            _Name = configuration["Mail:Name"];
            _SMTPServerIp = configuration["Mail:SMTPServerAdres"];
            _SMTPServerPort = Convert.ToInt32(configuration["Mail:Port"]);
        }

        protected override MailResult SendMail(MailRequestDTO mailParameterDTO)
        {
            var mailResult = new MailResult();
            try
            {
                if (!ValidateVariables())
                {
                    mailResult.IsSuccessfull = false;
                    mailResult.Error = "Konfigürasyon dosyasındaki parametreleri kontrol ediniz.";
                }

                using (MailMessage email = new MailMessage())
                {
                    email.From = new MailAddress(_ReplayToAddress, _ReplayToName);

                    if (mailParameterDTO.BCC != null)
                    {
                        foreach (var loopBcc in mailParameterDTO.BCC)
                        {
                            email.Bcc.Add(new MailAddress(loopBcc.Address, loopBcc.DisplayName));
                        }
                    }

                    if (mailParameterDTO.CC != null)
                    {
                        foreach (var loopCC in mailParameterDTO.CC)
                        {
                            email.CC.Add(new MailAddress(loopCC.Address, loopCC.DisplayName));
                        }
                    }

                    email.Subject = mailParameterDTO.Subject;
                    email.Body = mailParameterDTO.Content;
                    email.IsBodyHtml = true;
                    email.BodyEncoding = Encoding.GetEncoding("utf-8");
                    email.HeadersEncoding = Encoding.GetEncoding("utf-8");
                    email.SubjectEncoding = Encoding.GetEncoding("utf-8");
                    email.ReplyToList.Add(new MailAddress(_From, _Name));

                    SmtpClient smtp = new SmtpClient(_SMTPServerIp, _SMTPServerPort)
                    {
                        EnableSsl = false
                    };

                    if (mailParameterDTO.To != null)
                    {
                        foreach (var loopMail in mailParameterDTO.To)
                        {
                            email.To.Add(new MailAddress(loopMail.Address, loopMail.DisplayName));
                        }
                    }

                    smtp.Send(email);

                    mailResult.IsSuccessfull = true;
                }

            }
            catch (Exception ex)
            {
                mailResult.IsSuccessfull = false;
                mailResult.Error = string.Format("{0} Inner Exception: {1}", ex.ToString(), ex.InnerException != null ? ex.InnerException.Message : string.Empty);
            }

            return mailResult;
        }
        private bool ValidateVariables()
        {
            if (string.IsNullOrEmpty(_ReplayToAddress) || string.IsNullOrEmpty(_ReplayToName) || string.IsNullOrEmpty(_From) || string.IsNullOrEmpty(_Name) || string.IsNullOrEmpty(_SMTPServerIp) || _SMTPServerPort == 0)
                return false;
            else
                return true;
        }
    }
}
