using System;
using System.Windows;
using System.Net;
using System.Net.Mail;
using System.Windows.Shapes;
using SMTP_UFMA20210309.Model;
using System.Windows.Media;

namespace SMTP_UFMA20210309 {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {

        private ViewPortArea viewPort;
        private Line path;

        public MainWindow() {
            InitializeComponent();

            viewPort = new ViewPortArea(0,1000,0,1000);
            AddGraphics();
        }

        private void Exit_OnClick(object sender, RoutedEventArgs e) {
            this.Close();
        }

        private void Send_OnClick(object sender, RoutedEventArgs e) {
            // ToDo: Implement Classes
            try {
                #region Message
                // Credentials
                var credentials = new NetworkCredential("studentTestDCCMAPI2021@gmail.com","passWordABC");

                // Mail message
                var mail = new MailMessage() {
                    From = new MailAddress("studentTestDCCMAPI2021@gmail.com"),
                    Subject = "Dangerous Zone!",
                    Body = txtMessage.Text
                };

                mail.To.Add(new MailAddress(txtToEmail.Text));

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
                client.Send(mail);

                MessageBox.Show("Message sent!", "Message: ");
                #endregion
            } catch (Exception ex) {
                MessageBox.Show("Error message: ",ex.Message);
            }

        }

        private void AddGraphics() {
            path = new Line();
            path.X1 = viewPort.XNormalize(100, viewPortCanvas.Width);
            path.Y1 = viewPort.YNormalize(100, viewPortCanvas.Height);
            path.X2 = viewPort.XNormalize(900, viewPortCanvas.Width);
            path.Y2 = viewPort.YNormalize(900, viewPortCanvas.Height);
            path.Stroke = Brushes.Blue;
            path.StrokeThickness = 2;
            viewPortCanvas.Children.Add(path);

        }

    }
}
