using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Navigation;

namespace kursach.ViewModel
{
    internal class ViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        public ICommand NavigateCommand { get; }
        public ViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            NavigateCommand = new RelayCommand<string>(Navigate);
        }
        private void Navigate(string viewModelName)
        {
            _navigationService.NavigateTo(viewModelName);
        }
    }
}
