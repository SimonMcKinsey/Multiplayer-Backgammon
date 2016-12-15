using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WPFClientDemo.InfraStructure;

namespace WPFClientDemo.ViewModels
{
    public class AppViewModel : ViewModelBase
    {
        // simplified properties
        private ViewModelBase currentView;



        public ViewModelBase CurrentView
        {
            get { return currentView; }
            set
            {
                currentView = value;
                OnPropertyChanged(); 
            }
        }

        public AppViewModel()
        {
            ViewModelBase loginViewModel = ViewsFactory.CreateView("GameWindowView");
            loginViewModel.OnViewChange += LoginViewModel_OnViewChange;
            CurrentView = loginViewModel;
        }

        private void LoginViewModel_OnViewChange(object sender, PropertyChangedEventArgs e)
        {
            ViewModelBase viewModel = ViewsFactory.CreateView(e.PropertyName);
            viewModel.OnViewChange += App_OnViewChange;
            CurrentView = viewModel;
      
        }

        private void App_OnViewChange(object sender, PropertyChangedEventArgs e)
        {
            ViewModelBase viewModel = ViewsFactory.CreateView(e.PropertyName);
            viewModel.OnViewChange += LoginViewModel_OnViewChange;
            CurrentView = viewModel;
        }
    }
}
