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
        private double pathDistance;

        public MainWindow() {
            InitializeComponent();

            // Initializing coordinates with UTM coordinate system
            viewPort = new ViewPortArea(576158, 576954, 9716967, 9717580);
            viewPort.RoverX = 576246;
            viewPort.RoverY = 9717055;
            viewPort.TargetX = 576493;
            viewPort.TargetY = 9717345;
            pathDistance = viewPort.PathDistance();

            lblRover.Text = "Rover: " + viewPort.RoverX.ToString() + " x " + viewPort.RoverY.ToString();
            lblTarget.Text = "Target: " + viewPort.TargetX.ToString() + " x " + viewPort.TargetY.ToString();
            lblDistance.Text = "Distance: " + Convert.ToInt32(pathDistance).ToString();

            AddGraphics();

            emailSMTP = new EmailSMTP(txtToEmail.Text, txtMessage.Text);
        }

        private void Exit_OnClick(object sender, RoutedEventArgs e) {
            this.Close();
        }

        private void Send_OnClick(object sender, RoutedEventArgs e) {
            try {
                #region Email
                emailSMTP.EmailToAddress = txtToEmail.Text;
                emailSMTP.EmailMessage = txtMessage.Text;
                emailSMTP.SendEmail();
                MessageBox.Show("Message sent!", "Message: ");
                #endregion
            } catch (Exception ex) {
                MessageBox.Show("Error message: ", ex.Message);
            }

        }

        private void AddGraphics() {
            // Distance Rover - Target
            viewPortCanvas.Children.Remove(path);
            path = new Line();
            path.X1 = viewPort.XNormalize(viewPort.RoverX, viewPortCanvas.Width);
            path.Y1 = viewPort.YNormalize(viewPort.RoverY, viewPortCanvas.Height);
            path.X2 = viewPort.XNormalize(viewPort.TargetX, viewPortCanvas.Width);
            path.Y2 = viewPort.YNormalize(viewPort.TargetY, viewPortCanvas.Height);
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
            Canvas.SetLeft(rover, (viewPort.XNormalize(viewPort.RoverX, viewPortCanvas.Width) - 5));
            Canvas.SetTop(rover, (viewPort.YNormalize(viewPort.RoverY, viewPortCanvas.Height) - 5));
            viewPortCanvas.Children.Add(rover);

            viewPortCanvas.Children.Remove(target);
            target = new Ellipse();
            if (pathDistance < 50) {
                target.Stroke = Brushes.Red;
            } else {
                target.Stroke = Brushes.Green;
            }
            target.StrokeThickness = 2;
            target.Width = 50;
            target.Height = 50;
            Canvas.SetLeft(target, (viewPort.XNormalize(viewPort.TargetX, viewPortCanvas.Width) - (target.Width / 2)));
            Canvas.SetTop(target, (viewPort.YNormalize(viewPort.TargetY, viewPortCanvas.Height) - (target.Height / 2)));
            viewPortCanvas.Children.Add(target);

        }

        private void Forward(object sender, RoutedEventArgs e) {
            viewPort.ForwardRover(75);
            lblRover.Text = "Rover: " + Convert.ToInt32(viewPort.RoverX).ToString() + " x " + Convert.ToInt32(viewPort.RoverY).ToString();
            pathDistance = viewPort.PathDistance();
            lblDistance.Text = "Distance: " + Convert.ToInt32(pathDistance).ToString();
            AddGraphics();

            if (pathDistance < 50) {
                #region Email
                emailSMTP.EmailToAddress = txtToEmail.Text;
                emailSMTP.EmailMessage = txtMessage.Text;
                emailSMTP.SendEmail();
                MessageBox.Show("Message sent!", "Message: ");
                #endregion
            }
        }

        private void Restarting_OnClick(object sender, RoutedEventArgs e) {
            viewPort.RoverX = 576246;
            viewPort.RoverY = 9717055;
            lblRover.Text = "Rover: " + viewPort.RoverX.ToString() + " x " + viewPort.RoverY.ToString();
            pathDistance = viewPort.PathDistance();
            lblDistance.Text = "Distance: " + Convert.ToInt32(pathDistance).ToString();
            AddGraphics();
        }

    }
}
