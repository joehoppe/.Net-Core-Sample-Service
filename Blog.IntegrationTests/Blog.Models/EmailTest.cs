using Blog.Models.Mail;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MimeKit;
using System;

namespace Blog.IntegrationTests.Blog.Models
{
    [TestClass]
    public class EmailTest
    {
        [TestMethod]
        public void emails_can_be_sent()
        {
            var mailProviderAdapter = new SendGridAdapter();
            var email = new Email(mailProviderAdapter);

            var emailMessage = new MimeMessage();

            //todo: move to config file
            emailMessage.From.Add(new MailboxAddress(string.Empty, "myemailaddress@gmail.com"));
            emailMessage.To.Add(new MailboxAddress("", "myemailaddress@gmail.com"));
            emailMessage.Subject = "User Feedback";
            emailMessage.Body = new TextPart("plain") { Text = "Message" };

            email.Send(emailMessage);
        }
    }
}