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
using Microsoft.Win32;

namespace kursach.ViewModel
{
    public class CustomerViewModel : INotifyPropertyChanged
    {
        private readonly CustomerService _repository = new CustomerService();
        private string _firstName;
        private string _secondName;
        private string _nickName;
        private string _phoneNumber;
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
        public string FirstName
        {
            get => _firstName;
            set { _firstName = value; OnPropertyChanged(); }
        }
        public string SecondName
        {
            get => _secondName;
            set { _secondName = value; OnPropertyChanged(); }
        }
        public string PhoneNumber
        {
            get => _phoneNumber;
            set { _phoneNumber = value; OnPropertyChanged(); }
        }
        public string PhoneNumberProfile
        {
            get
            {
                if (CustomerService.CurrentCustomer != null)
                {
                    return CustomerService.CurrentCustomer.PhoneNumber;
                }
                else { return ""; }
            }
            set
            {
                if (_phoneNumber != value)
                {
                    _phoneNumber = value;
                    OnPropertyChanged();
                }
            }
        }
        public string NickNameProfile
        {
            get
            {
                if (CustomerService.CurrentCustomer != null)
                {
                    return CustomerService.CurrentCustomer.NickName;
                }
                else { return ""; }
            }
            set
            {
                if (_phoneNumber != value)
                {
                    _phoneNumber = value;
                    OnPropertyChanged();
                }
            }
        }
        public string FirstNameProfile
        {
            get
            {
                if (CustomerService.CurrentCustomer != null)
                {
                    return CustomerService.CurrentCustomer.FirstName;
                }
                else { return ""; }
            }
            set
            {
                if (_phoneNumber != value)
                {
                    _phoneNumber = value;
                    OnPropertyChanged();
                }
            }
        }
        public string SecondNameProfile
        {
            get
            {
                if (CustomerService.CurrentCustomer != null)
                {
                    return CustomerService.CurrentCustomer.SecondName;
                }
                else { return ""; }
            }
            set
            {
                if (_phoneNumber != value)
                {
                    _phoneNumber = value;
                    OnPropertyChanged();
                }
            }
        }
        public ICommand LoginCommand { get; }
        public ICommand NavigateToLoginOrProfileCommand { get; }
        public ICommand NavigateToRegisterCommand {  get; }
        public ICommand NavigateToMainCommand { get; }
        public ICommand RegisterCommand {  get; }
        public ICommand LogOutCommand {  get; }
        public ICommand NavigateToFavoriteCommand { get; }
        public CustomerViewModel()
        {
            LoginCommand = new RelayCommand(Login);
            NavigateToLoginOrProfileCommand = new RelayCommand(NavigateToLoginOrProfile);
            NavigateToRegisterCommand = new RelayCommand(NavigateToRegister);
            RegisterCommand = new RelayCommand(Register);
            NavigateToMainCommand = new RelayCommand(NavigateToMain);
            LogOutCommand = new RelayCommand(LogOutAndLogin);
            NavigateToFavoriteCommand = new RelayCommand(NavigateToFavorite);
        }
        private void NavigateToFavorite(object parametr)
        {
            if (CustomerService.IsUserLoggedIn == true)
            {
                if (Application.Current.MainWindow is MainWindow mainWindow)
                {
                    mainWindow.MyFrame.Navigate(new Uri("favorite.xaml", UriKind.Relative));
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
        private void LogOutAndLogin(object parametr)
        {
            CustomerService.IsUserLoggedIn = false;
            if (Application.Current.MainWindow is MainWindow mainWindow)
            {
                mainWindow.MyFrame.Navigate(new Uri("login.xaml", UriKind.Relative));
            }
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
                MessageBox.Show("Вітаємо!", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                if (Application.Current.MainWindow is MainWindow mainWindow)
                {
                    mainWindow.MyFrame.Navigate(new Uri("profile.xaml", UriKind.Relative));
                }
            }
            else
            {
                MessageBox.Show("Логін або пароль невірний", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void Register (object parameter)
        {
            if (string.IsNullOrEmpty(FirstName) || string.IsNullOrEmpty(SecondName) || string.IsNullOrEmpty(NickName) || string.IsNullOrEmpty(PhoneNumber) || string.IsNullOrEmpty(Password))
            {
                MessageBox.Show("Заповніть всі поля!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var existingCustomer = _repository.GetByNickName(NickName);
            if (existingCustomer != null)
            {
                MessageBox.Show("Логін зайнятий", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var customer = new Customer(FirstName, SecondName, PhoneNumber, NickName, Password);
            _repository.SaveCustomer(customer);

            MessageBox.Show("Регістрація успішна!", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            CustomerService.IsUserLoggedIn = false;
            if (Application.Current.MainWindow is MainWindow mainWindow)
            {
                mainWindow.MyFrame.Navigate(new Uri("login.xaml", UriKind.Relative));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
