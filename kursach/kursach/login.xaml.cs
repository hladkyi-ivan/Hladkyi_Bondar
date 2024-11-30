using kursach.ViewModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace kursach
{
    public partial class login : Page
    {
        public login()
        {
            InitializeComponent();
        }
        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e) // PasswordBox не підтримує біндінг, тому я не знайшовши варіантів реалізації порушив правила патерну та додав цю логіку поки що сюди. Сподіваюся до здачі курсової вирішити цю проблему.
        {
            var passwordBox = sender as PasswordBox;
            if (passwordBox != null)
            {
                var viewModel = (CustomerViewModel)this.DataContext;
                viewModel.Password = passwordBox.Password; 
            }
        }
    }
}
