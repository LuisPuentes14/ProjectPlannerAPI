using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Utilities
{
    public class Email
    {

        public static bool sendEmail(string in_email, string in_affair, string in_message)
        {

            bool sendEmail = false;

            try
            {
                MailMessage mail = new MailMessage();
                mail.To.Add(in_email);
                mail.From = new MailAddress("alejandroangel148@gmail.com");
                mail.Subject = in_affair;
                mail.Body = in_message;
                mail.IsBodyHtml = true;

                var smtp = new SmtpClient()
                {
                    Credentials = new NetworkCredential("alejandroangel148@gmail.com", "rezrythzhkfcgykd"),
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                };

                smtp.Send(mail);
                sendEmail = true;

                return sendEmail;

            }
            catch
            {
                return sendEmail;
            }

            

        }
    }
}
