﻿using kursach.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using kursach.View;
namespace kursach.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public CustomerViewModel CustomerViewModel { get; set; }
        public TicketViewModel TicketViewModel { get; set; }
        public PaymentViewModel PaymentViewModel { get; set; }

        public MainViewModel()
        {
            CustomerViewModel = new CustomerViewModel();
            TicketViewModel = new TicketViewModel();
            PaymentViewModel = new PaymentViewModel();
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
