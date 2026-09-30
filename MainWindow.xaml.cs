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
        private readonly string emailExpediteur = "tatebongwilson@gmail.com";
        private const string motDePasseExpediteur = "ibjfqfpjxodxhalz";
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void BtnSend_Click(object sender, RoutedEventArgs e)
        {

            if (string.IsNullOrWhiteSpace(txtTo.Text) || string.IsNullOrWhiteSpace(txtSubject.Text))
            {
                MessageBox.Show("Veuillez remplir tous les champs obligatoires (destinataire et objet).",
                                "Champs manquants", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Configuration SMTP automatique pour Gmail (basée sur emailExpediteur)
            string smtpServer = "smtp.gmail.com";
            int smtpPort = 587;

            try
            {
                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(emailExpediteur);
                    mail.To.Add(txtTo.Text.Trim());
                    mail.Subject = txtSubject.Text.Trim();
                    mail.Body = txtBody.Text;

                    using (SmtpClient smtpClient = new SmtpClient(smtpServer, smtpPort))
                    {
                        smtpClient.UseDefaultCredentials = false;
                        smtpClient.Credentials = new NetworkCredential(emailExpediteur, motDePasseExpediteur);
                        smtpClient.EnableSsl = true;

                        await smtpClient.SendMailAsync(mail);
                    }
                }

                MessageBox.Show("Message envoyé avec succès !", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);

                // Réinitialisation des champs après envoi
                txtTo.Clear();
                txtSubject.Clear();
                txtBody.Clear();
            }
            catch (SmtpException ex)
            {
                MessageBox.Show($"Erreur d'authentification ou SMTP : {ex.Message}",
                                "Erreur d'envoi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Une erreur est survenue : {ex.Message}",
                                "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            Menu_principal menu = new Menu_principal();
            menu.Show();
            this.Close();
        }
    }
}
