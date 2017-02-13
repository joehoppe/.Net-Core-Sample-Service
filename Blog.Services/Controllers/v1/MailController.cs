using Blog.Models.Mail;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Text.Encodings.Web;

namespace Blog.Services.Controllers.v1.Mail
{
    /// <summary>
    /// Send email
    /// </summary>
    [Route("api/v1/[controller]")]
    public class MailController
    {
        private IMailProviderAdapter _mailProviderAdapter;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

        public MailController(IMailProviderAdapter mailProviderAdapter)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            _mailProviderAdapter = mailProviderAdapter;
        }

        /// <summary>
        /// Send email
        /// </summary>
        [Route("Send")]
        [HttpPost]
        public IActionResult Send([FromQuery]string from, [FromQuery]string text) //using [FromQuery] because Swagger doesn't support body yet
        {
            var stringArgs = new Dictionary<string, string> { { "from", from }, { "text", text } };
            ValidateArgumentNullException(stringArgs);

            var message = new MimeMessage();
            var email = new Email(_mailProviderAdapter);

            //todo: move email address to config file
            message.From.Add(new MailboxAddress(string.Empty, "myemailaddress@gmail.com"));
            message.To.Add(new MailboxAddress("", "myemailaddress@gmail.com"));
            message.Subject = "User Feedback";
            message.Body = new TextPart("plain")
            {
                Text = "From: " + HtmlEncoder.Default.Encode(from) + Environment.NewLine 
                    + Environment.NewLine + HtmlEncoder.Default.Encode(text)
            };

            email.Send(message);

            return new StatusCodeResult(200);
        }

        private void ValidateArgumentNullException(Dictionary<string, string> dictionary)
        {
            foreach (var key in dictionary)
            {
                if (!string.IsNullOrWhiteSpace(key.Value)) continue;
                throw new ArgumentNullException("Parameter cannot be null or white space: " + key.Key);
            }
        }
    }
}