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
        private const string SENDER_APP_PASSWORD = "ibjfqfpjxodxhalz";
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void BtnSend_Click(object sender, RoutedEventArgs e)
        {

            // Validation des champs (le mot de passe n'est plus demandé à l'utilisateur)
            if (string.IsNullOrWhiteSpace(txtFrom.Text) ||
                string.IsNullOrWhiteSpace(txtTo.Text) ||
                string.IsNullOrWhiteSpace(txtSubject.Text))
            {
                MessageBox.Show("Veuillez remplir tous les champs obligatoires (adresse expéditeur, destinataire et objet).",
                                "Champs manquants", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string senderEmail = txtFrom.Text.Trim();

            // Détection du serveur SMTP selon le domaine
            string smtpServer = "";
            int smtpPort = 587;

            if (senderEmail.EndsWith("@gmail.com", StringComparison.OrdinalIgnoreCase))
            {
                smtpServer = "smtp.gmail.com";
            }
            else if (senderEmail.EndsWith("@outlook.com", StringComparison.OrdinalIgnoreCase) ||
                     senderEmail.EndsWith("@hotmail.com", StringComparison.OrdinalIgnoreCase) ||
                     senderEmail.EndsWith("@live.com", StringComparison.OrdinalIgnoreCase))
            {
                smtpServer = "smtp.office365.com";
            }
            else
            {
                MessageBox.Show("Veuillez utiliser un compte Gmail, Outlook ou Hotmail.",
                                "Fournisseur non pris en charge", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(senderEmail);
                    mail.To.Add(txtTo.Text.Trim());
                    mail.Subject = txtSubject.Text.Trim();
                    mail.Body = txtBody.Text;

                    using (SmtpClient smtpClient = new SmtpClient(smtpServer, smtpPort))
                    {
                        smtpClient.UseDefaultCredentials = false;

                        // Utilisation de la constante non modifiable pour l'authentification
                        smtpClient.Credentials = new NetworkCredential(senderEmail, SENDER_APP_PASSWORD);
                        smtpClient.EnableSsl = true;

                        await smtpClient.SendMailAsync(mail);
                    }
                }

                MessageBox.Show("Message envoyé avec succès !", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);

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
