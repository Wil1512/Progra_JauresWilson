using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Progra_JauresWilson
{
    /// <summary>
    /// Logique d'interaction pour Menu_principal.xaml
    /// </summary>
    public partial class Menu_principal : Window
    {
        public Menu_principal()
        {
            InitializeComponent();
            MainContent.Content = new Menu_principal();
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new Menu_principal();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new MainWindow();
        }

        private void BtnEmail_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mailWindow = new MainWindow();
            mailWindow.Show();
            this.Close(); // Ferme le menu principal (optionnel)
        }

        private void BtnHome_Click(object sender, RoutedEventArgs e)
        {
            // Logique à exécuter au clic (ex: ouvrir la fenêtre principale)
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }
    }
}
