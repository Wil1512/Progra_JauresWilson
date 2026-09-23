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
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new EmailView();
        }
    }
}
