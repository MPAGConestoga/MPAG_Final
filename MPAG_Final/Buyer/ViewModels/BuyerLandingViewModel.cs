using MPAG_Final.Services;
using MPAG_Final.SharedModels;
using MPAG_Final.SharedViewModels;
using MPAG_Final.Utilities;
using System.Windows.Input;

namespace MPAG_Final.Buyer.ViewModels
{
    /**
    * \Class BuyerLandingViewModel
    * \Brief View model for the handling the buyer user control page
    * \Details The Buyer Landing Viewmodel loads and handles the information that will be 
    * displayed to the user in the Buyer view for the buyer role. Will handle 
    * the generation from the contract market place, the choices for the contract and approval
    * for the contract
    */

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
        public ICommand PauseCommand { get; private set; }
        public ICommand EmptyCommand { get; private set; }

        /// <summary>
        ///         Constructor that instantiates a new instance of the OrderViewModel class
        /// </summary>
        public BuyerLandingViewModel()
        {
            ContractsVM = new ContractsViewModel();

            RunDatabaseCommand = new RelayCommand(RunDatabase);
            PauseCommand = new RelayCommand(PauseDatabase);
            EmptyCommand = new RelayCommand(EmptyList);

        }

       /// <summary>
       ///          Empty list for contract market place
       /// </summary>
        public void EmptyList()
        {
            ContractsVM.EmptyList();
        }

        /// <summary>
        ///         Run Database will start the thread for running the database in the back
        ///         ground to pool the information
        /// </summary>
        public void RunDatabase()
        {
            var contractMarketPlace = new MockContractMarketplace(); //mock service for the testing of the ui

            _service = contractMarketPlace;
            ContractsVM.DatabaseRun();
            LoadContracts();
        }
        //  Function to bind to xaml command

        /// <summary>
        ///         Pause the database for the user
        /// </summary>
        public void PauseDatabase()
        {
            ContractsVM.PauseDatabase();

        }

        //function for loading contracts; references ContractViewModel LoadContracts function
        /// <summary>
        ///         Load the contract from the contract marketplace 
        /// </summary>
        private void LoadContracts()
        {
            ContractsVM.LoadContracts(_service.GetContracts());
        }
    }
}
