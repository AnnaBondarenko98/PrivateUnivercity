using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using Anna_Bondarenko_FinalTask.BLL.DTO;
using Anna_Bondarenko_FinalTask.BLL.Interfaces.MessageSenderInterface;
using NLog;

namespace Anna_Bondarenko_FinalTask.BLL.Infrastructure.MessageSender
{
    public class MessageSender:IMessageSender
    {

        private readonly ILogger _logger;

        private const string MailFrom = "anna.bondarenko@nure.ua";
        private const string Password = "526375fyz";
        private const string SmtpClient = "smtp.gmail.com";
        private const int Port = 465;

        public MessageSender(ILogger logger)
        {
            _logger = logger;
        }

        public static bool WriteAsFile = true;
        public static string FileLocation = "E:\\emails";

        public  void SendToUs(MessageDto sendMessage)
        {
           
                _logger.Info($"Try to create and send the message.");

                var from = new MailAddress(MailFrom, $"Message from {sendMessage.From}");

                var to = new MailAddress(sendMessage.To);

                var message = new MailMessage(from, to)
                {
                    Subject = $"Message from {sendMessage.From}",

                    Body = sendMessage.Subject + sendMessage.Body,
                    //IsBodyHtml = true,
                };

                if (sendMessage.Subject == @"E:\doc.pdf")
                {
                    var data = new Attachment(sendMessage.Subject, MediaTypeNames.Application.Pdf);

                    message.Attachments.Add(data);
                }
                else
                {
                    message.BodyEncoding = Encoding.UTF8;
                }

                //var smtp = new SmtpClient(SmtpClient, Port)
                SmtpClient smtp = new SmtpClient()//
                {
                       UseDefaultCredentials = false,
                   // Credentials = new NetworkCredential(MailFrom, Password),
                    EnableSsl = false, //false
                                         DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory,//
                                         PickupDirectoryLocation = FileLocation,//
                };

                smtp.Send(message);

                _logger.Info($"The message for sending was created and sent");
          
        }

        public  void SendForgotLink(string callbackUrl, string email)
        {
            var from = new MailAddress(MailFrom, "");
            var to = new MailAddress("anna.bondarenko@nure.ua");

            var message = new MailMessage(from, to)
            {
                Subject = "Reset password",
                Body = "<a href=\"" + callbackUrl + "\"> Click here</a>",
                IsBodyHtml = true
            };

            var smtp = new SmtpClient(SmtpClient, Port)
            {
                Credentials = new NetworkCredential(MailFrom, Password),
                EnableSsl = true
            };

           
                _logger.Info($"Sending a link to reset the password by this Email: {email }");
                smtp.Send(message);
            
        }
    }
}
