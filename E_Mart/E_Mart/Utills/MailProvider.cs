using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Net;
using System.Text;

namespace E_Mart.Utils
{
    class MailProvider
    {
        public static bool SendEmail(string receiverEmail, string Subject, string Body)
        {
            try
            {
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress("emart.help1@gmail.com", "E-Mart", System.Text.Encoding.UTF8);
                message.To.Add(new MailAddress(receiverEmail));
                message.Subject = Subject;
                message.IsBodyHtml = true; //to make message body as html  
                message.Body = Body;
                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com"; //for gmail host  
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("emart.help1@gmail.com", "lafumuxwoyhnihhj");
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);

            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}
