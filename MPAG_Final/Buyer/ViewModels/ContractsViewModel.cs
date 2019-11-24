using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPAG_Final.Buyer.Models;
using MPAG_Final.Utilities;


namespace MPAG_Final.Buyer.ViewModels
{
    public class ContractsViewModel : ObservableObject
    {
        private Contract _selectedContract;
        public Contract SelectedContract
        {

            get { return _selectedContract; }
            set { OnPropertyChanged(ref _selectedContract, value); }
        }

        public ObservableCollection<Contract> Contracts { get; private set; }

        public ContractsViewModel()
        {

        }

        public void LoadContracts(IEnumerable<Contract> contracts)
        {
            Contracts = new ObservableCollection<Contract>(contracts);
            OnPropertyChanged("Contracts");
        }

    }
}
