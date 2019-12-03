using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MPAG_Final.Buyer.Models;
using MPAG_Final.Utilities;

namespace MPAG_Final.Buyer.ViewModels
{
    public class ContractViewModel : ObservableObject
    {
        private Contract _selectedContract;
        public Contract SelectedContract
        {
            get { return _selectedContract; }
            set { OnPropertyChanged(ref _selectedContract, value); }
        }

        private bool _isEditMode;
        public bool IsEditMode
        {
            get { return _isEditMode; }
            set
            {
                OnPropertyChanged(ref _isEditMode, value);
                OnPropertyChanged("IsDisplayMode");
            }
        }

        public bool IsDisplayMode
        {
            get { return !_isEditMode; }
        }

        public ObservableCollection<Contract> Contracts { get; private set; }
        public ICommand EditCommand { get; private set; }

        //public ContractsViewModel()
        //{
        //    EditCommand = new RelayCommand(Edit, CanEdit);
        //}

        private bool CanEdit()
        {
            if (SelectedContract == null)
                return false;

            return !IsEditMode;
        }

        private void Edit()
        {
            IsEditMode = true;
        }

        public void LoadContracts(IEnumerable<Contract> contracts)
        {
            Contracts = new ObservableCollection<Contract>(contracts);
            OnPropertyChanged("Contracts");
        }

        public enum VanType
        {
            Dry,
            Reefer
        }

        public enum JobType
        {
            FTL,
            LTL
        }

        public enum City
        {
            Windsor,
            London,
            Hamilton,
            Toronto,
            Oshawa,
            Belleville,
            Kingston,
            Ottawa
        }


    }
}
