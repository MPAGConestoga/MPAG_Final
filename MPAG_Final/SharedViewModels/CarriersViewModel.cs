using MPAG_Final.SharedModels;
using MPAG_Final.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MPAG_Final.SharedViewModels
{
    public class CarriersViewModel : ObservableObject
    {
        private Carrier _selectedContract;
        public Carrier SelectedContract
        {
            get { return _selectedContract; }
            set { OnPropertyChanged(ref _selectedContract, value); }
        }

        public ObservableCollection<Carrier> Carriers { get; private set; }

        public CarriersViewModel()
        {
        }

        public void LoadCarriers(IEnumerable<Carrier> carrier)
        {
            Carriers = new ObservableCollection<Carrier>(carrier);
            OnPropertyChanged("Carriers");
        }
    }
}
