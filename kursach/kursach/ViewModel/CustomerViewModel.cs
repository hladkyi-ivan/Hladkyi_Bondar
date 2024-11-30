using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using kursach.Model;

namespace kursach.ViewModel
{
    public class CustomerViewModel : INotifyPropertyChanged
    {
        private readonly CustomerService _repository = new CustomerService();
        private string _nickName;
        private string _password;
        public string NickName
        {
            get => _nickName;
            set { _nickName = value; OnPropertyChanged(); }
        }
        public string Password
        {
            get => _password;
            set { _password = value; OnPropertyChanged(); }
        }
        public ICommand LoginCommand { get; }
        public CustomerViewModel()
        {
            LoginCommand = new RelayCommand(Login);
        }
        private void Login(object parameter)
        {
            if (_repository.ValidateUserCredentials(NickName, Password))
            {
                MessageBox.Show("Login successful!", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Invalid nickname or password.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
