using MPAG_Final.Services;
using MPAG_Final.SharedModels;
using MPAG_Final.SharedViewModels;
using MPAG_Final.Utilities;

namespace MPAG_Final.Buyer.ViewModels
{
    public class BuyerLandingViewModel : ObservableObject
    {
        // mock contract service for testing UI
        private IContractDataService _service;
        
        // for accessing the contracts view model
        private ContractsViewModel _contractsVM;
        public ContractsViewModel ContractsVM
        {
            get { return _contractsVM; }
            set { OnPropertyChanged(ref _contractsVM, value); }
        }

        /// <summary>
        /// Constructor that instantiates a new instance of the OrderViewModel class
        /// </summary>
        public BuyerLandingViewModel()
        {

        }

        public async void RunDatabase()
        {
            var contractMarketPlace = new MockContractMarketplace(); //mock service for the testing of the ui

            ContractsVM = new ContractsViewModel(contractMarketPlace);
            _service = contractMarketPlace;

            LoadContracts();
        }
        //function for loading contracts; references ContractViewModel LoadContracts function
        private void LoadContracts()
        {
            ContractsVM.LoadContracts(_service.GetContracts());
        }



    }
}
