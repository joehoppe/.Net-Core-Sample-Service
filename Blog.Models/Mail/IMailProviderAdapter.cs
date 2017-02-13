using System;
using MailKit.Security;
using MimeKit;

namespace Blog.Models.Mail
{
    public interface IMailProviderAdapter : IDisposable
    {
        void Connect(string host, int port, SecureSocketOptions secureSocketOptions);
        void Authenticate(string userName, string password);
        void Send(MimeMessage mimeMessage);
        void Disconnect(bool quit);           
    }
}