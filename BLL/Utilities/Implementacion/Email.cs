using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using BLL.Utilities.Interfaces;
using Microsoft.Extensions.Options;
using BLL.ModelsAppsettings;

namespace BLL.Utilities.Implementacion
{
    public class Email : IEmail
    {
        private readonly SMTP _smtp;

        public Email(IOptions<SMTP> smtp)
        {
            _smtp = smtp.Value;
        }
        public void sendEmail(string in_email, string in_affair, string in_message)
        {
            try
            {
                // string testemail = "alejandroangel148@gmail.com";
                //string testemail = "sanchezalvarez124@gmail.com";


                MailMessage mail = new MailMessage();
                mail.To.Add(in_email);
                mail.From = new MailAddress(_smtp.SenderEmail);
                mail.Subject = in_affair;
                mail.Body = in_message;
                mail.IsBodyHtml = true;

                var smtp = new SmtpClient()
                {
                    Credentials = new NetworkCredential(_smtp.SenderEmail, _smtp.SenderPassword),
                    Host = _smtp.Host,
                    Port = _smtp.Port,
                    EnableSsl = true,
                };

                smtp.Send(mail);

            }
            catch (Exception)
            {
                throw new Exception("Correno no enviado.");
            }

        }
    }
}
