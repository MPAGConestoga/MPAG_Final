using MPAG_Final.Utilities;
using MPAG_Final.Services;
using MPAG_Final.SharedViewModels;

namespace MPAG_Final.Planner.ViewModels
{
    /**
   * \Class PendingViewModel
   * \Brief View model for the handling of pending orders
   * \Details The pending view model loads and handles the information that will be 
   * displayed to the user in the pending order list in the planner role. Will handle 
   * the generation and storage of new pending orders for the pending order view.
   */
    public class PendingViewModel : ObservableObject
    {
        private IContractDataService _contractService;
        private ICarrierDataService _carrierService;

        private ContractsViewModel _contractsVM;
        public ContractsViewModel ContractsVM
        {
            get { return _contractsVM; }
            set { OnPropertyChanged(ref _contractsVM, value); }
        }

        private CarriersViewModel _carriersVM;
        public CarriersViewModel CarriersVM
        {
            get { return _carriersVM; }
            set { OnPropertyChanged(ref _carriersVM, value); }
        }

        /// <summary>
        /// Constructor for PendingViewModel
        /// </summary>
        public PendingViewModel(IContractDataService contractService, ICarrierDataService carrierService)
        {
            ContractsVM = new ContractsViewModel(contractService);
            _contractService = contractService;
            _carrierService = carrierService;

            LoadContracts();
        }

        private void LoadContracts()
        {
            ContractsVM.LoadContracts(_contractService.GetContracts());
        }
        private void LoadCarriers()
        {
            CarriersVM.LoadCarriers(_carrierService.GetCarriers());
        }
    }
}
