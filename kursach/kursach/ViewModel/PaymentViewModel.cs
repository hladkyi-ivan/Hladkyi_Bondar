using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace kursach.ViewModel
{
    public class PaymentViewModel : INotifyPropertyChanged
    {
        private string _cardNumber;
        private int? _code;
        private string _period;
        private string _owner;
        private string _adress;
        public string CardNumber
        {
            get => _cardNumber;
            set
            {
                _cardNumber = value;
                OnPropertyChanged(nameof(CardNumber));
            }
        }
        public int? Code
        {
            get => _code;
            set
            {
                _code = value;
                OnPropertyChanged(nameof(Code));
            }
        }
        public string Period
        {
            get => _period;
            set
            {
                _period = value;
                OnPropertyChanged(nameof(Period));
            }
        }
        public string Owner
        {
            get => _owner;
            set
            {
                _owner = value;
                OnPropertyChanged(nameof(Owner));
            }
        }
        public string Adress
        {
            get => _adress;
            set
            {
                _adress = value;
                OnPropertyChanged(nameof(Adress));
            }
        }
        public ICommand PaymentCommand { get; }
        public PaymentViewModel()
        {
            PaymentCommand = new RelayCommand(ExecuteProcessPayment);
        }
        private string ValidatePaymentData()
        {
            if (string.IsNullOrWhiteSpace(CardNumber))
                return "Введіть номер карти";
            string cleanNumber = CardNumber.Replace(" ", "").Replace("-", "");
            if (cleanNumber.Length != 16 || !cleanNumber.All(char.IsDigit))
                return "Номер карти повинен містити 16 цифр";
            if (!_code.HasValue || _code <= 0 || _code > 999)
                return "CVV-код повинен бути числом від 001 до 999";
            if (string.IsNullOrWhiteSpace(Period))
                return "Введіть термін дії карти";
            if (!Regex.IsMatch(Period, @"^(0[1-9]|1[0-2])\/([0-9]{2})$"))
                return "Невірний формат терміну дії (MM/YY)";
            string[] parts = Period.Split('/');
            if (!int.TryParse(parts[0], out int month) || !int.TryParse(parts[1], out int year))
                return "Невірний формат терміну дії";
            year += 2000;
            var cardExpiry = new DateTime(year, month, 1).AddMonths(1).AddDays(-1);
            if (cardExpiry <= DateTime.Today)
                return "Термін дії карти минув";
            if (string.IsNullOrWhiteSpace(Owner))
                return "Введіть ім'я власника";
            if (string.IsNullOrWhiteSpace(Adress))
                return "Введіть адресу";
            return string.Empty; 
        }
        private void ExecuteProcessPayment(object parameter)
        {
            string validationError = ValidatePaymentData();

            if (string.IsNullOrEmpty(validationError))
            {
                MessageBox.Show("Оплата успішно проведена!", "Успіх", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show(validationError, "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
