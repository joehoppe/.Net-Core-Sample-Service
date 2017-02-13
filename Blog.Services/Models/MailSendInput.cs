namespace Blog.Services.Models
{
    /// <summary>
    /// Model for sending email
    /// </summary>
    public class MailSendInput
    {
        public string From { get; set; }
        public string Message { get; set; }
    }
}