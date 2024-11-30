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
using System.Windows.Navigation;

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
        public ICommand NavigateToLoginOrProfileCommand { get; }
        public ICommand NavigateToRegisterCommand {  get; }
        public ICommand NavigateToMainCommand { get; }

        public CustomerViewModel()
        {
            LoginCommand = new RelayCommand(Login);
            NavigateToLoginOrProfileCommand = new RelayCommand(NavigateToLoginOrProfile);
            NavigateToRegisterCommand = new RelayCommand(NavigateToRegister);
            NavigateToMainCommand = new RelayCommand(NavigateToMain);
        }
          private void NavigateToLoginOrProfile(object parameter)
          {
            if (CustomerService.IsUserLoggedIn == true)
            {
                if (Application.Current.MainWindow is MainWindow mainWindow)
                {
                    mainWindow.MyFrame.Navigate(new Uri("profile.xaml", UriKind.Relative));
                }
            }
            else
            {
                if (Application.Current.MainWindow is MainWindow mainWindow)
                {
                    mainWindow.MyFrame.Navigate(new Uri("login.xaml", UriKind.Relative));
                }
            }
          }
          private void NavigateToRegister(object parameter)
          {
            if (Application.Current.MainWindow is MainWindow mainWindow)
            {
                mainWindow.MyFrame.Navigate(new Uri("registration.xaml", UriKind.Relative));
            }
          }
        public void NavigateToMain(object parameter)
        {
            if (Application.Current.MainWindow is MainWindow mainWindow)
            {
                mainWindow.MyFrame.Navigate(new Uri("main.xaml", UriKind.Relative));
            }
        }
        private void Login(object parameter)
            {
            if (_repository.ValidateUserCredentials(NickName, Password))
            {
                CustomerService.IsUserLoggedIn = true;
                MessageBox.Show("Login successful!", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                if (Application.Current.MainWindow is MainWindow mainWindow)
                {
                    mainWindow.MyFrame.Navigate(new Uri("profile.xaml", UriKind.Relative));
                }
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
