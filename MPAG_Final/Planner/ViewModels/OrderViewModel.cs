using MPAG_Final.Planner.Models;
using MPAG_Final.Services;
using MPAG_Final.SharedModels;
using MPAG_Final.SharedViewModels;
using MPAG_Final.Utilities;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Windows.Input;

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
        private ICarrierDataService _carrierService;
        private IContractDataService _contractService;

        // for accessing the contracts view model
        private ContractsViewModel _contractsVM;
        public ContractsViewModel ContractsVM
        {
            get { return _contractsVM; }
            set { OnPropertyChanged(ref _contractsVM, value); }
        }
        // for accessing the contracts view model_plannerRole
        private PlannerRole _plannerRoleVM;
        public PlannerRole PlannerRoleVM
        {
            get { return _plannerRoleVM; }
            set { OnPropertyChanged(ref _plannerRoleVM, value); }
        }

        private CarriersViewModel _carriersVM;
        public CarriersViewModel CarriersVM
        {
            get { return _carriersVM; }
            set { OnPropertyChanged(ref _carriersVM, value); }
        }

        public ICommand RunTestCommand { get; private set; }


        /// <summary>
        ///     Constructor that instantiates a new instance of the OrderViewModel class
        /// </summary>
        public OrderViewModel(PlannerRole planner)
        {
            var carrierMarketPlace = new MockCarrierMarketplace(); //mock service for the testing of the ui
            var contractMarketPlace = new MockContractMarketplace(); //mock service for the testing of the ui

            PlannerRoleVM = planner;

            _carrierService = carrierMarketPlace;
            _contractService = contractMarketPlace;

            RunTestCommand = new RelayCommand(RunTest);

            ContractsVM = new ContractsViewModel();
            CarriersVM = new CarriersViewModel(carrierMarketPlace);
            LoadCarriers();
            LoadContracts();

            int i = 0;          
        }

        private void LoadContracts()
        {
            ContractsVM.LoadContracts(_contractService.GetContracts());
        }

        private void LoadCarriers()
        {
            CarriersVM.LoadCarriers(_carrierService.GetCarriers());
        }

        private void RunTest()
        {
            PlannerRoleVM.testFunction();
        }
    }
}
