using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using kursach.Model;
using kursach;

namespace kursach.ViewModel
{
    public class TicketViewModel : INotifyPropertyChanged
    {
        private DateTime? _startDateFilter;
        private string _destinationFilter;
        private string _periodFilter;
        private string _departureFilter; private ObservableCollection<Ticket> _tickets;
        private ObservableCollection<Ticket> _filteredTickets;
        private static ObservableCollection<Ticket> _likedTickets;
        private static Customer _lastCustomer;
        public ObservableCollection<string> PossibleDestinations { get; private set; }
        public ObservableCollection<string> PossiblePeriod { get; private set; }
        public ObservableCollection<string> PossibleDepartures { get; private set; }
        public ObservableCollection<Ticket> Tickets
        {
            get => _tickets;
            set
            {
                _tickets = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<Ticket> FiltertedTickets
        {
            get => _filteredTickets;
            set
            {
                _filteredTickets = value;
                OnPropertyChanged();
            }
        }
        public DateTime? StartDateFilter
        {
            get => _startDateFilter;
            set
            {
                _startDateFilter = value;
                OnPropertyChanged();
            }
        }
        public string DestinationFilter
        {
            get => _destinationFilter;
            set
            {
                _destinationFilter = value;
                OnPropertyChanged();
            }
        }
        public string PeriodFilter
        {
            get => _periodFilter;
            set
            {
                _periodFilter = value;
                OnPropertyChanged();
            }
        }
        public string DepartureFilter
        {
            get => _departureFilter;
            set
            {
                _departureFilter = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<Ticket> LikedTickets
        {
            get
            {
                if (_lastCustomer != CustomerService.CurrentCustomer)
                {
                    _likedTickets = CustomerService.IsUserLoggedIn && CustomerService.CurrentCustomer != null ? new ObservableCollection<Ticket>(CustomerService.CurrentCustomer.LikedTickets) : new ObservableCollection<Ticket>();     
                    _lastCustomer = CustomerService.CurrentCustomer;
                }
                return _likedTickets;
            }
            set
            {
                _likedTickets = value;
                OnPropertyChanged();
            }
        }
        public ICommand ApplyFiltersCommand { get; }
        public ICommand BuyCommand { get; }
        public ICommand ResetFiltersCommand { get; }
        public ICommand LikeCommand { get; private set; }
        public ICommand RemoveFromLikedCommand { get; private set; }
        public TicketViewModel()
        {
            Tickets = new ObservableCollection<Ticket>
            {
                new Ticket {
                   Destination = "Egypt",
                    Date = new DateTime(2024, 12, 6),
                    Description = "Grand Blue Saint Maria Aqua Park 3*\nЄгипет",
                    HotelImage = "/egipt.jpg",
                    Period = "Період: 8 ночей",
                    TicketAvailability = "Наявність квитків: ✔",
                    Price = "28 236 UAH",
                    Departure = "Warsaw",
                    HotelName = "Grand Blue Saint Maria Aqua Park 3*"
                },
                new Ticket {
                    Destination = "Paris",
                    Date = new DateTime(2024, 12, 10),
                    Description = "Le Grand Hotel Paris 4*\nФранція, Париж",
                    HotelImage = "/Le Grand Hotel Paris 4.jpg",
                    Period = "Період: 7 ночей",
                    TicketAvailability = "Наявність квитків: ✔",
                    Price = "32 450 UAH",
                    Departure = "London",
                    HotelName = "Le Grand Hotel Paris"
                },
                new Ticket {
                    Destination = "Rome",
                    Date = new DateTime(2025, 1, 15),
                    Description = "Roma Palace Hotel 4*\nІталія, Рим",
                    HotelImage = "/Roma Palace Hotel 4.jpg",
                    Period = "Період: 7 ночей",
                    TicketAvailability = "Наявність квитків: ✔",
                    Price = "30 800 UAH",
                    Departure = "Berlin",
                    HotelName = "Roma Palace Hotel"
                },
                new Ticket {
                    Destination = "Bali",
                    Date = new DateTime(2024, 12, 20),
                    Description = "Bali Paradise Resort 5*\nІндонезія, Балі",
                    HotelImage = "/Bali Paradise Resort 5.jpg",
                    Period = "Період: 7 ночей",
                    TicketAvailability = "Наявність квитків: ✔",
                    Price = "45 000 UAH",
                    Departure = "Warsaw",
                    HotelName = "Bali Paradise Resort 5*"
                },
                new Ticket {
                    Destination = "Barcelona",
                    Date = new DateTime(2024, 12, 12),
                    Description = "Catalonia Plaza Hotel 4*\nІспанія, Барселона",
                    HotelImage = "/Catalonia Plaza Hotel 4.jpg",
                    Period = "Період: 7 ночей",
                    TicketAvailability = "Наявність квитків: ✔",
                    Price = "35 200 UAH",
                    Departure = "London",
                    HotelName = "Catalonia Plaza Hotel 4*"
                },
                new Ticket {
                    Destination = "New York",
                    Date = new DateTime(2025, 1, 5),
                    Description = "Manhattan Grand Hotel 5*\nСША, Нью-Йорк",
                    HotelImage = "/Manhattan Grand Hotel 5.jpg",
                    Period = "Період: 7 ночей",
                    TicketAvailability = "Наявність квитків: ✔",
                    Price = "50 000 UAH",
                    Departure = "Paris",
                    HotelName = "Manhattan Grand Hotel 5*"
                },
                new Ticket {
                    Destination = "Venice",
                    Date = new DateTime(2025, 2, 14),
                    Description = "Venetian Palace Hotel 4*\nІталія, Венеція",
                    HotelImage = "/Venetian Palace Hotel 4.jpg",
                    Period = "Період: 7 ночей",
                    TicketAvailability = "Наявність квитків: ✔",
                    Price = "31 500 UAH",
                    Departure = "Berlin",
                    HotelName = "Venetian Palace Hotel"
                },
                new Ticket {
                    Destination = "Amsterdam",
                    Date = new DateTime(2024, 12, 22),
                    Description = "Amsterdam Art Hotel 4*\nНідерланди, Амстердам",
                    HotelImage = "/Amsterdam Art Hotel 4.jpg",
                    Period = "Період: 7 ночей",
                    TicketAvailability = "Наявність квитків: ✔",
                    Price = "29 900 UAH",
                    Departure = "Paris",
                    HotelName = "Amsterdam Art Hotel"
                },
                new Ticket {
                    Destination = "Antalya",
                    Date = new DateTime(2024, 12, 25),
                    Description = "Antalya Beach Resort 5*\nТурція, Анталія",
                    HotelImage = "/Antalya Beach Resort 5.png",
                    Period = "Період: 7 ночей",
                    TicketAvailability = "Наявність квитків: ✔",
                    Price = "27 800 UAH",
                    Departure = "Warsaw",
                    HotelName = "Antalya Beach Resort 5*"
                },
                new Ticket {
                    Destination = "Split",
                    Date = new DateTime(2025, 3, 1),
                    Description = "Split Sunset Hotel 4*\nХорватія, Спліт",
                    HotelImage = "/Split Sunset Hotel 4.png",
                    Period = "Період: 7 ночей",
                    TicketAvailability = "Наявність квитків: ✔",
                    Price = "28 600 UAH",
                    Departure = "London",
                    HotelName = "Split Sunset Hotel"
                }
            };
            FiltertedTickets = new ObservableCollection<Ticket>(Tickets);
            PossibleDestinations = new ObservableCollection<string>(Tickets.Select(t => t.Destination).Distinct());
            PossibleDepartures = new ObservableCollection<string>(Tickets.Select(t => t.Departure).Distinct());
            PossiblePeriod = new ObservableCollection<string>(Tickets.Select(t => t.Period).Distinct());
            ApplyFiltersCommand = new RelayCommand(ApplyFilters);
            BuyCommand = new RelayCommand(Buy);
            LikeCommand = new RelayCommand(LikeTicket);
            RemoveFromLikedCommand = new RelayCommand(RemoveFromLiked);
            ResetFiltersCommand = new RelayCommand(ResetFilters);
        }
        public void ApplyFilters()
        {
            var filtered = Tickets.Where(v =>
                (!StartDateFilter.HasValue || v.Date >= StartDateFilter.Value) &&
                (string.IsNullOrEmpty(PeriodFilter) || v.Period.Equals(PeriodFilter, StringComparison.OrdinalIgnoreCase)) &&
                (string.IsNullOrEmpty(DestinationFilter) || v.Destination.Equals(DestinationFilter, StringComparison.OrdinalIgnoreCase)) &&
                (string.IsNullOrEmpty(DepartureFilter) || v.Departure.Equals(DepartureFilter, StringComparison.OrdinalIgnoreCase))).ToList();

            FiltertedTickets.Clear();
            foreach (var ticket in filtered)
            {
                FiltertedTickets.Add(ticket);
            }
        }
        public void ResetFilters()
        {
            StartDateFilter = null;
            DestinationFilter = null;
            DepartureFilter = null;
            FiltertedTickets = new ObservableCollection<Ticket>(Tickets);
        }
        private void Buy(object parameter)
        {
            if (CustomerService.IsUserLoggedIn == true)
            {
                if (Application.Current.MainWindow is MainWindow mainWindow)
                {
                    mainWindow.MyFrame.Navigate(new Uri("buying.xaml", UriKind.Relative));
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
        private void LikeTicket(object parameter)
        {
            if (CustomerService.IsUserLoggedIn)
            {
                if (parameter is Ticket ticket && CustomerService.CurrentCustomer != null)
                {
                    if (!LikedTickets.Any(t => t.HotelName == ticket.HotelName && t.Date == ticket.Date))
                    {
                        CustomerService.Instance.UpdateCurrentCustomerLikedTickets(ticket, true);
                        LikedTickets.Add(ticket);
                        MessageBox.Show("Квиток додано до обраного!", "Добре", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("Цей квиток вже є в обраному!", "Інформація", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
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
        private void RemoveFromLiked(object parameter)
        {
            if (CustomerService.IsUserLoggedIn && CustomerService.CurrentCustomer != null)
            {
                if (parameter is Ticket ticket)
                {
                    CustomerService.Instance.UpdateCurrentCustomerLikedTickets(ticket, false);
                    LikedTickets.Remove(ticket);
                    MessageBox.Show("Квиток видалено", "Добре", MessageBoxButton.OK, MessageBoxImage.Information);
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