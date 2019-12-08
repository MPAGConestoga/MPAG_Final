using MPAG_Final.Services;
using MPAG_Final.SharedModels;
using MPAG_Final.SharedViewModels;
using MPAG_Final.Utilities;
using System.Windows.Input;

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

        //public ICommand /add the name of the command to bind to in xaml here/ { get; private set;}
        public ICommand RunDatabaseCommand { get; private set; }

        /// <summary>
        /// Constructor that instantiates a new instance of the OrderViewModel class
        /// </summary>
        public BuyerLandingViewModel()
        {
            ContractsVM = new ContractsViewModel(/*contractMarketPlace*/);

            RunDatabaseCommand = new RelayCommand(RunDatabase);

           //   /xaml command name/ = new RelayCommand(function from below)
        }

        //  Function to bind to xaml command
        public void RunDatabase()
        {
            var contractMarketPlace = new MockContractMarketplace(); //mock service for the testing of the ui

            _service = contractMarketPlace;
            ContractsVM.DatabaseRun();
            LoadContracts();
        }
        //function for loading contracts; references ContractViewModel LoadContracts function
        private void LoadContracts()
        {
            ContractsVM.LoadContracts(_service.GetContracts());
        }



    }
}
