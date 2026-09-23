using System.Net;
using System.Net.Mail;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Progra_JauresWilson
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void BtnSend_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFrom.Text) || string.IsNullOrWhiteSpace(txtTo.Text) || string.IsNullOrWhiteSpace(txtSubject.Text))
            {
                MessageBox.Show("Veuillez remplir tous les champs obligatoires.", "Attention", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                string smtpServer = "smtp.gmail.com";
                int smtpPort = 587;

                string senderEmail = txtFrom.Text;
                string senderPassword = "votre_mot_de_passe_application";

                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(senderEmail);
                mail.To.Add(txtTo.Text);
                mail.Subject = txtSubject.Text;
                mail.Body = txtBody.Text;

                SmtpClient smtpClient = new SmtpClient(smtpServer)
                {
                    Port = smtpPort,
                    Credentials = new NetworkCredential(senderEmail, senderPassword),
                    EnableSsl = true,
                };

                // Envoi asynchrone du mail
                await smtpClient.SendMailAsync(mail);

                
                MessageBox.Show("Message envoyé avec succès !", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);

                // Réinitialisation des champs Sujet et Message après l'envoi
                txtSubject.Clear();
                txtBody.Clear();
            }
            catch (Exception ex)
            {
                // En cas d'erreur
                MessageBox.Show($"Erreur lors de l'envoi : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}