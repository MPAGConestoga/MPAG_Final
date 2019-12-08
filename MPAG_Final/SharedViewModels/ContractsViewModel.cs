using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MPAG_Final.Services;
using MPAG_Final.SharedModels;
using MPAG_Final.Utilities;
using System.Windows;
using System.Data.Linq;

namespace MPAG_Final.SharedViewModels
{    
    public class ContractsViewModel : ObservableObject
    {
        //property for the management of selected contracts in the views that utilize lists
        //buyer page binds to them via the BuyerLandingView
        private Contract _selectedContract;
        public Contract SelectedContract
        {
            get { return _selectedContract; }
            set { OnPropertyChanged(ref _selectedContract, value); }
        }

        // Contracts are stored here in a WPF friendly list
        public ObservableCollection<Contract> Contracts { get; private set; }
        public ICommand UpdateCommand { get; private set; }

        //mock data service for testing UI
        private IContractDataService _contractDataService;

        //ContractsViewModel contructor
        public ContractsViewModel(IContractDataService contractDataService)
        {
            _contractDataService = contractDataService;
            UpdateCommand = new RelayCommand(Update);

        }

        private void Update()
        {
            //add this later
        }


        //command for the loading of contracts
        public void LoadContracts(IList<Contract> contracts)
        {
            Contracts = new ObservableCollection<Contract>(contracts);
            OnPropertyChanged("Contracts");
        }
    }

    
}
