using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MPAG_Final.Services;
using MPAG_Final.SharedViewModels;
using MPAG_Final.Utilities;

namespace MPAG_Final.Planner.ViewModels
{
    /**
   * \Class OrderViewModel
   * \Brief View model for the handling of the order view
   * \Details The order view model loads and handles the information that will be 
   * displayed to the user in the planner role. Pending and active orders will be 
   * loaded, as well as the carriers to be attached to the orders. Will also 
   * instantiate instances of the child view models: PendingViewModel and ActiveViewModel.
   */
    public class OrderViewModel : ObservableObject
    {
        //private object _currentView;
        //public object CurrentView
        //{
        //    get { return _currentView; }
        //    set { OnPropertyChanged(ref _currentView, value); }
        //}

        private CarriersViewModel _carriersVM;
        public CarriersViewModel CarriersVM
        {
            get { return _carriersVM; }
            set { OnPropertyChanged(ref _carriersVM, value); }
        }

        /// <summary>
        ///     Constructor that instantiates a new instance of the OrderViewModel class
        /// </summary>
        public OrderViewModel()
        {

        }
    }
}
