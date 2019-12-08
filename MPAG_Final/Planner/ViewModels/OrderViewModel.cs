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
        // mock contract service for testing UI
        private ICarrierDataService _service;

        private object _currentView;
        public object CurrentView
        {
            get { return _currentView; }
            set { OnPropertyChanged(ref _currentView, value); }
        }

        private CarriersViewModel _carriersVM;
        public CarriersViewModel CarriersVM
        {
            get { return _carriersVM; }
            set { OnPropertyChanged(ref _carriersVM, value); }
        }

        private ActiveViewModel _activeOrdersVM;
        public ActiveViewModel ActiveOrdersVM 
        {
            get { return _activeOrdersVM; }
            set { OnPropertyChanged(ref _activeOrdersVM, value); }
        }


        public ICommand LoadPendingOrdersCommand { get; private set; }
        public ICommand LoadActiveOrdersCommand { get; private set; }
        public ICommand LoadCarriersCommand { get; private set; }

        /// <summary>
        ///     Constructor that instantiates a new instance of the OrderViewModel class
        /// </summary>
        public OrderViewModel()
        {
            var carrierMarketPlace = new MockCarrierMarketplace(); //mock service for the testing of the ui

            CarriersVM = new CarriersViewModel(carrierMarketPlace);
            _service = carrierMarketPlace;

            LoadCarriers();

        }

        //function for loading contracts; references ContractViewModel LoadContracts function
        private void LoadCarriers()
        {
            CarriersVM.LoadCarriers(_service.GetCarriers());
        }


    }
}
