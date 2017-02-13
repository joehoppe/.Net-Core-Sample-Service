using System;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace Blog.Models.Mail
{
    ///Using MailKit to connect to SendGrid because at the time of development a SendGrid .Net core API had not yet been released
    public class SendGridAdapter : IMailProviderAdapter
    {
        private SmtpClient _smtpClient = new SmtpClient();

        public void Connect(string host, int port, SecureSocketOptions secureSocketOptions)
        {
            _smtpClient.Connect(host, port, secureSocketOptions);
        }

        public void Authenticate(string userName, string password)
        {
            _smtpClient.Authenticate(userName, password);
        }

        public void Send(MimeMessage mimeMessage)
        {
            _smtpClient.Send(mimeMessage);
        }

        public void Disconnect(bool quit)
        {
            _smtpClient.Disconnect(quit);
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
    }
}
