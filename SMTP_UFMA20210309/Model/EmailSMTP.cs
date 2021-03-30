using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;

namespace SMTP_UFMA20210309.Model {
    class EmailSMTP {

        #region Fields, Properties and Variables
        // ToDo: Implement All Properties
        private string emailToAddress;
        public string EmailToAddress {
            get { return emailToAddress; }
            set { emailToAddress = value; }
        }

        private string emailMessage;
        public string EmailMessage {
            get { return emailMessage; }
            set { emailMessage = value; }
        }

        #endregion

        #region Constructors
        public EmailSMTP() {
        }

        public EmailSMTP(string emailToAddress, string emailMessage) {
            this.emailToAddress = emailToAddress;
            this.emailMessage = emailMessage;
        }
        #endregion

        public void SendEmail() {
            // Credentials
            var credentials = new NetworkCredential("studentTestDCCMAPI2021@gmail.com", "passWordABC");

            // Mail message
            var mail = new MailMessage() {
                From = new MailAddress("studentTestDCCMAPI2021@gmail.com"),
                Subject = "Dangerous Zone!!!",
                Body = EmailMessage
            };

            mail.To.Add(new MailAddress(EmailToAddress));

            // SMTP client
            var client = new SmtpClient() {
                Port = 587,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Host = "smtp.gmail.com",
                EnableSsl = true,
                Credentials = credentials
            };

            // Send the message
            //client.Send(mail);
            string status = "Succeed!";
            client.SendAsync(mail,status);

        }

    }
}
