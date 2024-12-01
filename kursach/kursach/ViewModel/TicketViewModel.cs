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
        private string _departureFilter; private ObservableCollection<Ticket> _tickets;
        private ObservableCollection<Ticket> _filteredTickets;
        public ObservableCollection<string> PossibleDestinations { get; private set; }
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

        public string DepartureFilter
        {
            get => _departureFilter;
            set
            {
                _departureFilter = value;
                OnPropertyChanged();
            }
        }
        public ICommand ApplyFiltersCommand { get; }
        public ICommand BuyCommand { get; }

        public TicketViewModel()
        {
            Tickets = new ObservableCollection<Ticket>
            {
                new Ticket { 
                    Destination = "Egypt", 
                    Date = new DateTime(2024, 12, 6), 
                    Description = "Grand Blue Saint Maria Aqua Park 3*\nЄгипет, Hurghada",
                    HotelImage = "/egipt.jpg",
                    Period = "Період: Hurghada   Виліт: 06.12.2024   Ночей: 7",
                    TicketAvailability = "Наявність квитків: ✔",
                    Price = "28 236 UAH",
                    Departure = "Warsaw",
                    HotelName = "Grand Blue Saint Maria Aqua Park 3*"
                },
                new Ticket { 
                    Destination = "Paris", 
                    Date = new DateTime(2023, 8, 20), 
                    Description = "Le Grand Hotel Paris 4*\nФранція, Париж",
                    HotelImage = "/egipt.jpg",
                    Period = "Період: Paris   Виліт: 20.08.2023   Ночей: 5",
                    TicketAvailability = "Наявність квитків: ✔",
                    Price = "32 450 UAH",
                    Departure = "London",
                    HotelName = "Le Grand Hotel Paris"
                },
                new Ticket { 
                    Destination = "Rome", 
                    Date = new DateTime(2023, 6, 10), 
                    Description = "Roma Palace Hotel 4*\nІталія, Рим",
                    HotelImage = "/egipt.jpg",
                    Period = "Період: Rome   Виліт: 10.06.2023   Ночей: 6",
                    TicketAvailability = "Наявність квитків: ✔",
                    Price = "30 800 UAH",
                    Departure = "Paris",
                    HotelName = "Roma Palace Hotel"
                }
            };
            PossibleDestinations = new ObservableCollection<string>(Tickets.Select(t => t.Destination).Distinct());
            PossibleDepartures = new ObservableCollection<string>(Tickets.Select(t => t.Departure).Distinct());
            FiltertedTickets = new ObservableCollection<Ticket>(Tickets);
            ApplyFiltersCommand = new RelayCommand(ApplyFilters);
            BuyCommand = new RelayCommand(Buy);
        }

        public void ApplyFilters()
        {
            var filtered = Tickets.Where(v =>
                (!StartDateFilter.HasValue || v.Date >= StartDateFilter.Value) &&
                (string.IsNullOrEmpty(DestinationFilter) || v.Destination.Equals(DestinationFilter, StringComparison.OrdinalIgnoreCase)) &&
                (string.IsNullOrEmpty(DepartureFilter) || v.Departure.Equals(DepartureFilter, StringComparison.OrdinalIgnoreCase))).ToList();

            FiltertedTickets.Clear();
            foreach (var ticket in filtered)
            {
                FiltertedTickets.Add(ticket);
            }
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
                if(Application.Current.MainWindow is MainWindow mainWindow)
                {
                    mainWindow.MyFrame.Navigate(new Uri("login.xaml", UriKind.Relative));
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