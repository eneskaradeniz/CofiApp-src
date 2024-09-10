namespace CofiApp.Contracts.Emails
{
    public class MailRequest
    {
        public MailRequest()
        {
        }

        public MailRequest(string emailTo, string subject, string body, bool isHtml)
        {
            EmailTo = emailTo;
            Subject = subject;
            Body = body;
            IsHtml = isHtml;
        }

        public string EmailTo { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool IsHtml { get; set; }
    }
}
