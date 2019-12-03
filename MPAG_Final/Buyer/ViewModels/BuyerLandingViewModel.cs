using MPAG_Final.Services;
using MPAG_Final.SharedViewModels;
using MPAG_Final.Utilities;



namespace MPAG_Final.Buyer.ViewModels
{
    public class BuyerLandingViewModel : ObservableObject
    {
        private IContractDataService _service;
        private ContractsViewModel _contractsVM;
        public ContractsViewModel ContractsVM
        {
            get { return _contractsVM; }
            set { OnPropertyChanged(ref _contractsVM, value); }
        }

        

        /// <summary>
        /// Constructor that instantiates a new instance of the OrderViewModel class
        /// </summary>
        public BuyerLandingViewModel(IContractDataService service)
        {
            ContractsVM = new ContractsViewModel();
            _service = service;

            LoadContracts();
            
        }

        private void LoadContracts()
        {
            ContractsVM.LoadContracts(_service.GetContracts());
        }


    }
}
