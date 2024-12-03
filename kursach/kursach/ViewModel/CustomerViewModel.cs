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
        private string _avatarPath;
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
        public string AvatarPath
        {
            get => _avatarPath;
            set
            {
                _avatarPath = value;
                OnPropertyChanged();
            }
        }
        public ICommand LoginCommand { get; }
        public ICommand NavigateToLoginOrProfileCommand { get; }
        public ICommand NavigateToRegisterCommand {  get; }
        public ICommand NavigateToMainCommand { get; }
        public ICommand RegisterCommand {  get; }
        public ICommand LogOutCommand {  get; }
        public ICommand NavigateToFavoriteCommand { get; }
        public ICommand ChangeAvatarCommand { get; }
        public CustomerViewModel()
        {
            LoginCommand = new RelayCommand(Login);
            NavigateToLoginOrProfileCommand = new RelayCommand(NavigateToLoginOrProfile);
            NavigateToRegisterCommand = new RelayCommand(NavigateToRegister);
            RegisterCommand = new RelayCommand(Register);
            NavigateToMainCommand = new RelayCommand(NavigateToMain);
            LogOutCommand = new RelayCommand(LogOutAndLogin);
            NavigateToFavoriteCommand = new RelayCommand(NavigateToFavorite);
            ChangeAvatarCommand = new RelayCommand(ChangeAvatar);
            LoadUserAvatar();
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
            if (!ValidateFirstName(FirstName) || !ValidateLastName(SecondName) || !ValidatePhoneNumber(PhoneNumber) || !ValidateNickName(NickName) || !ValidatePassword(Password))
            {
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
        private bool ValidateFirstName(string firstName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
            {
                MessageBox.Show("Ім'я не може бути порожнім", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            
            if (firstName.Length > 25)
            {
                MessageBox.Show("Ім'я не повинно перевищувати 25 символів", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            
            if (!System.Text.RegularExpressions.Regex.IsMatch(firstName, @"^[a-zA-Zа-яА-ЯіІїЇєЄ]+$"))
            {
                MessageBox.Show("Ім'я повинно містити лише літери", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            
            return true;
        }
        private bool ValidateLastName(string lastName)
        {
            if (string.IsNullOrWhiteSpace(lastName))
            {
                MessageBox.Show("Прізвище не може бути порожнім", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            
            if (lastName.Length > 25)
            {
                MessageBox.Show("Прізвище не повинно перевищувати 25 символів", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            
            if (!System.Text.RegularExpressions.Regex.IsMatch(lastName, @"^[a-zA-Zа-яА-ЯіІїЇєЄ]+$"))
            {
                MessageBox.Show("Прізвище повинно містити лише літери", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            
            return true;
        }
        private bool ValidatePhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
            {
                MessageBox.Show("Номер телефону не може бути порожнім", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            
            if (!phoneNumber.StartsWith("+"))
            {
                MessageBox.Show("Номер телефону повинен починатися з '+'", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            
            string digitsOnly = new string(phoneNumber.Where(char.IsDigit).ToArray());
            if (digitsOnly.Length > 20)
            {
                MessageBox.Show("Номер телефону не повинен перевищувати 20 цифр", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            
            return true;
        }
        private bool ValidateNickName(string nickName)
        {
            if (string.IsNullOrWhiteSpace(nickName))
            {
                MessageBox.Show("Нік не може бути порожнім", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            
            if (nickName.Length > 20)
            {
                MessageBox.Show("Нік не повинен перевищувати 20 символів", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            
            if (nickName.Length < 6)
            {
                MessageBox.Show("Нік повинен бути не менше 6 символів", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            
            if (!System.Text.RegularExpressions.Regex.IsMatch(nickName, @"^[a-zA-Z0-9]+$"))
            {
                MessageBox.Show("Нік повинен містити лише англійські літери та цифри", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }            
            if (_repository.GetByNickName(nickName) != null)
            {
                MessageBox.Show("Такий нік вже існує", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }
        private bool ValidatePassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Пароль не може бути порожнім", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            
            if (password.Length > 20)
            {
                MessageBox.Show("Пароль не повинен перевищувати 20 символів", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            
            if (!System.Text.RegularExpressions.Regex.IsMatch(password, @"^[a-zA-Z0-9]+$"))
            {
                MessageBox.Show("Пароль повинен містити лише англійські літери та цифри", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            
            if (password.Length < 6)
            {
                MessageBox.Show("Пароль повинен бути не менше 6 символів", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }
        private void LoadUserAvatar()
        {
            if (CustomerService.CurrentCustomer == null)
            {
                AvatarPath = "/Без_названия-removebg-preview.png";
            }
            else if (string.IsNullOrEmpty(CustomerService.CurrentCustomer.AvatarPath))
            {
                AvatarPath = "/Без_названия-removebg-preview.png";
            }
            else
            {
                AvatarPath = CustomerService.CurrentCustomer.AvatarPath;
            }
        }
        private void ChangeAvatar(object parameter)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg|All files (*.*)|*.*",
                Title = "Виберіть картинку"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                string selectedPath = openFileDialog.FileName;
                string avatarsDirectory = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Аватари", CustomerService.CurrentCustomer.NickName);
                if (!System.IO.Directory.Exists(avatarsDirectory))
                {
                    System.IO.Directory.CreateDirectory(avatarsDirectory);
                }
                string fileName = $"аватар{System.IO.Path.GetExtension(selectedPath)}";
                string destinationPath = System.IO.Path.Combine(avatarsDirectory, fileName);
                try
                {
                    System.IO.File.Copy(selectedPath, destinationPath, true);
                    AvatarPath = destinationPath;
                    if (CustomerService.CurrentCustomer != null)
                    {
                        CustomerService.CurrentCustomer.AvatarPath = destinationPath;
                        _repository.UpdateCustomerAvatar(CustomerService.CurrentCustomer.NickName, destinationPath);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Не вдалося завантажити аватар: {ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
