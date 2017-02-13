using Blog.Models.Mail;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using MimeKit;
using System;
using System.Text;

namespace Blog.Services
{
    /// <summary>
    /// Handle uncaught exceptions
    /// </summary>
    public class UncaughtExceptionFilter : ExceptionFilterAttribute
    {
        private readonly ILogger _logger;
        private IMailProviderAdapter _mailProviderAdapter;

        /// <summary>
        /// Handle uncaught exceptions
        /// </summary>
        public UncaughtExceptionFilter(ILoggerFactory loggerFactory, SendGridAdapter mailProviderAdapter)
        {
            _logger = loggerFactory.CreateLogger("UncaughtExceptionFilter");
            _mailProviderAdapter = mailProviderAdapter;
        }

        /// <summary>
        /// Handle uncaught exceptions
        /// </summary>
        public override void OnException(ExceptionContext context)
        {
            _logger.LogInformation("OnException");
            EmailError(context.Exception);
            base.OnException(context);
        }

        private void EmailError(Exception ex)
        {
            try
            {
                var message = new MimeMessage();
                var email = new Email(_mailProviderAdapter);

                message.From.Add(new MailboxAddress(string.Empty, "myemailaddress@gmail.com"));
                message.To.Add(new MailboxAddress("", "myemailaddress@gmail.com"));
                message.Subject = "Error";
                message.Body = new TextPart("plain")
                {
                    Text = GenerateEmailBody(ex)
                };

                email.Send(message);
            }
            catch (Exception caughtException)
            {
                _logger.LogError(caughtException.ToString(), null);
            }
        }

        private string GenerateEmailBody(Exception ex)
        {
            var body = new StringBuilder("Date/time: " + DateTime.Now.ToString());
            body.AppendLine();
            body.AppendLine();
            body.AppendLine("UTC: " + DateTime.UtcNow.ToString());
            body.AppendLine();
            body.AppendLine();
            body.AppendLine("Exception: " + ex.ToString());
            return body.ToString();
        }
    }
}