using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2
{
    internal class smpt_mail
    {
        public static MailMessage CreateMail(string name, string emailFrom, string emailTo, string subject, string body)
        {
            var from = new MailAddress(emailFrom, name);
            var to = new MailAddress(emailTo);
            var mail = new MailMessage(from, to);
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml= false;
            return mail;
        }

        public static void SendMail(string host, int smptPort, string emailFrom, string password, MailMessage mail)
        {
            SmtpClient smtp = new SmtpClient(host, smptPort);
            smtp.Credentials = new NetworkCredential(emailFrom, password);
            smtp.EnableSsl=true;

            smtp.Send(mail);
        }
    }
}
