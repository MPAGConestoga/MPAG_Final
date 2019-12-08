using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
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
        private PendingViewModel _pendingOrdersVM;
        public PendingViewModel PendingOrdersVM
        {
            get { return _pendingOrdersVM; }
            set { OnPropertyChanged(ref _pendingOrdersVM, value); }
        }

        private ActiveViewModel _activeOrdersVM;
        public ActiveViewModel ActiveOrdersVM
        {
            get { return _activeOrdersVM; }
            set { OnPropertyChanged(ref _activeOrdersVM, value); }
        }

        private object _currentView;
        public object CurrentView
        {
            get { return _currentView; }
            set { OnPropertyChanged(ref _currentView, value); }
        }

        public ICommand LoadPendingOrdersCommand { get; private set; }
        public ICommand LoadActiveOrdersCommand { get; private set; }
        public ICommand LoadCarriersCommand { get; private set; }

        /// <summary>
        /// Constructor that instantiates a new instance of the OrderViewModel class
        /// </summary>
        public OrderViewModel()
        {
            PendingOrdersVM = new PendingViewModel();
            ActiveOrdersVM = new ActiveViewModel();

            PendingOrdersVM = new ViewModels.PendingViewModel();
            CurrentView = PendingOrdersVM;

            //these may have to be moved to pending and active viewmodels instead
            LoadPendingOrdersCommand = new RelayCommand(LoadPendingOrders);
            LoadActiveOrdersCommand = new RelayCommand(LoadActiveOrders);
            LoadCarriersCommand = new RelayCommand(LoadCarriers);

        }

        /// <summary>
        /// Method for the display of pending orders
        /// </summary>
        private void LoadPendingOrders()
        {

        }

        /// <summary>
        /// Method for the display of active orders
        /// </summary>
        private void LoadActiveOrders()
        {

        }

        /// <summary>
        /// Method for the display of carriers 
        /// </summary>
        private void LoadCarriers()
        {

        }

    }
}
