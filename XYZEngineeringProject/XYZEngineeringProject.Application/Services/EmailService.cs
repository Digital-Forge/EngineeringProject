using System.ComponentModel;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using XYZEngineeringProject.Application.Extensions;
using XYZEngineeringProject.Application.Interfaces;
using XYZEngineeringProject.Domain.Interfaces;
using XYZEngineeringProject.Infrastructure.Utils;

namespace XYZEngineeringProject.Application.Services
{
    public class EmailService : IEmailService
    {
        private readonly IEmailRepository _emailRepository;
        private readonly Logger _logger;

        private MailMessage mailMessage;
        private SmtpClient smtp;

        public EmailService(IEmailRepository emailRepository, Logger logger)
        {
            _emailRepository = emailRepository;
            _logger = logger;
        }

        public bool SendMail(string to, string title, string text)
        {
            var conf = _emailRepository.GetCurrentEmailConfig();
            if (conf != null)
            {
                _logger.Log(Logger.Source.Service, Logger.InfoType.Warning, "Can't take email configuration");
                return false;
            }

            mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(conf.SenderEmail, conf.SenderName);
            mailMessage.To.Add(new MailAddress(to));
            mailMessage.IsBodyHtml = true;
            mailMessage.Subject = title;
            mailMessage.BodyEncoding = Encoding.UTF8;
            mailMessage.SubjectEncoding = Encoding.UTF8;

            mailMessage.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(text.StripHTML(), null, MediaTypeNames.Text.Plain));
            mailMessage.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(htmlContent(text), null, MediaTypeNames.Text.Html));

            smtp = new SmtpClient
            {
                Host = conf.HostSmtp,
                EnableSsl = conf.EnableSSL,
                Port = conf.Port,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(conf.SenderEmail, conf.SenderEmailPassword)
            };

            try
            {
                smtp.SendCompleted += OnSendCompleted;
                smtp.SendMailAsync(mailMessage).Wait();
            }
            catch (Exception e)
            {
                _logger.Log(Logger.Source.Service, Logger.InfoType.Error, $"Exeption on send Email : {e.Message}");
                return false;
            }

            _logger.Log(Logger.Source.Service, Logger.InfoType.Info, $"Send Email - To: {to}, Title: {title}, Content: {text}");
            return true;
        }

        private void OnSendCompleted(object sender, AsyncCompletedEventArgs e)
        {
            smtp.Dispose();
            mailMessage.Dispose();
        }

        private string htmlContent(string text)
        {
            return $@"
            <html>
                <head>
                </head>
                <body>
                    <div style='font-size: 16px; padding: 10px; font-family: Arial; line-height: 1.4;'>
                        {text}
                    </div>
                </body>
            </html>";
        }
    }
}
