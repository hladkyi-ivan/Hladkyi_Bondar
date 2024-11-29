using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace kursach.ViewModel
{
    internal class NavigationService : INavigationService
    {
        private readonly Dictionary<string, Func<ViewModelBase>> _viewModelFactories;

        public Action<ViewModelBase> OnViewModelChanged { get; set; }

        public NavigationService()
        {
            _viewModelFactories = new Dictionary<string, Func<ViewModelBase>>();
        }

        public void Configure(string viewModelName, Func<ViewModelBase> viewModelFactory)
        {
            if (!_viewModelFactories.ContainsKey(viewModelName))
                _viewModelFactories[viewModelName] = viewModelFactory;
        }

        public void NavigateTo(string viewModelName)
        {
            if (_viewModelFactories.ContainsKey(viewModelName))
            {
                var viewModel = _viewModelFactories[viewModelName]();
                OnViewModelChanged?.Invoke(viewModel);
            }
            else
            {
                throw new Exception($"ViewModel '{viewModelName}' is not configured.");
            }
        }
    }
}
