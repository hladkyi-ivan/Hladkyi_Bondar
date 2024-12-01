using kursach.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

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
        public TicketViewModel()
        {
            Tickets = new ObservableCollection<Ticket>
            {
                new Ticket { Destination = "Egypt", Date = new DateTime(2024, 12, 6), HotelName = "Hotel Egypt", Departure = "New York" },
                new Ticket { Destination = "Paris", Date = new DateTime(2023, 8, 20), HotelName = "Hotel Paris", Departure = "London" },
                new Ticket { Destination = "Rome", Date = new DateTime(2023, 6, 10), HotelName = "Hotel Rome", Departure = "Paris" }
            };
            PossibleDestinations = new ObservableCollection<string>(Tickets.Select(t => t.Destination).Distinct());
            PossibleDepartures = new ObservableCollection<string>(Tickets.Select(t => t.Departure).Distinct());
            FiltertedTickets = new ObservableCollection<Ticket>(Tickets);
            ApplyFiltersCommand = new RelayCommand(ApplyFilters);
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
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
