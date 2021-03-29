using System;
using System.Windows;
using System.Net;
using System.Net.Mail;
using System.Windows.Shapes;
using SMTP_UFMA20210309.Model;
using System.Windows.Media;
using System.Windows.Controls;

namespace SMTP_UFMA20210309 {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {

        private ViewPortArea viewPort;
        private Line path;
        private EmailSMTP emailSMTP;

        private Ellipse rover;
        private Ellipse target;

        public MainWindow() {
            InitializeComponent();

            viewPort = new ViewPortArea(0,1000,0,1000);
            AddGraphics();

            emailSMTP = new EmailSMTP(txtToEmail.Text, txtMessage.Text);
        }

        private void Exit_OnClick(object sender, RoutedEventArgs e) {
            this.Close();
        }

        private void Send_OnClick(object sender, RoutedEventArgs e) {
            try {
                #region Message
                emailSMTP.SendEmail();
                MessageBox.Show("Message sent!", "Message: ");
                #endregion
            } catch (Exception ex) {
                MessageBox.Show("Error message: ",ex.Message);
            }

        }

        private void AddGraphics() {
            // Distance Rover - Target
            path = new Line();
            path.X1 = viewPort.XNormalize(100, viewPortCanvas.Width);
            path.Y1 = viewPort.YNormalize(100, viewPortCanvas.Height);
            path.X2 = viewPort.XNormalize(900, viewPortCanvas.Width);
            path.Y2 = viewPort.YNormalize(900, viewPortCanvas.Height);
            path.Stroke = Brushes.Blue;
            path.StrokeThickness = 2;
            viewPortCanvas.Children.Add(path);

            viewPortCanvas.Children.Remove(rover);
            rover = new Ellipse();
            rover.Fill = new SolidColorBrush(Color.FromRgb(50, 50, 50));
            rover.Stroke = Brushes.Red;
            rover.StrokeThickness = 2;
            rover.Width = 10;
            rover.Height = 10;
            Canvas.SetLeft(rover, (viewPort.XNormalize(200, viewPortCanvas.Width) - 5));
            Canvas.SetTop(rover, (viewPort.YNormalize(500, viewPortCanvas.Height) - 5));
            viewPortCanvas.Children.Add(rover);

            viewPortCanvas.Children.Remove(target);
            target = new Ellipse();
            target.Stroke = Brushes.Red;
            target.StrokeThickness = 2;
            target.Width = 50;
            target.Height = 50;
            Canvas.SetLeft(target, (viewPort.XNormalize(700, viewPortCanvas.Width) - (target.Width/2)));
            Canvas.SetTop(target, (viewPort.YNormalize(500, viewPortCanvas.Height) - (target.Height/2)));
            viewPortCanvas.Children.Add(target);

        }

    }
}
