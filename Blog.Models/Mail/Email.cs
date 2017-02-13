using System;
using MailKit.Security;
using MimeKit;

namespace Blog.Models.Mail
{
    public class Email : IDisposable
    {
        private IMailProviderAdapter _smtpClient;

        public Email(IMailProviderAdapter smtpClient)
        {
            _smtpClient = smtpClient;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing) return;
            if (_smtpClient == null) return;
            _smtpClient.Dispose();
            _smtpClient = null;
        }

        public void Send(MimeMessage mimeMessage)
        {
            _smtpClient.Connect("smtp.sendgrid.net", 465, SecureSocketOptions.SslOnConnect);
            
            //move email address and password to conifg files
            _smtpClient.Authenticate("account@azure.com", "password");
            _smtpClient.Send(mimeMessage);
            _smtpClient.Disconnect(true);
        }
    }
}
