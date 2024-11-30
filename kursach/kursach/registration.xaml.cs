using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace kursach
{
    /// <summary>
    /// Логика взаимодействия для registration.xaml
    /// </summary>
    public partial class registration : Page
    {
        public registration()
        {
            InitializeComponent();
        }
        private void Back(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new main());
        }
        private void Openurl_google(object sender, RoutedEventArgs e)
        {
            string url = "https://accounts.google.com/";

            Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
        }

        private void Openurl_facebook(object sender, RoutedEventArgs e)
        {
            string url = "https://www.facebook.com/login/";

            Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
        }
        private void Openurl_instagram(object sender, RoutedEventArgs e)
        {
            string url = "https://www.instagram.com/";

            Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
        }

    }
}
