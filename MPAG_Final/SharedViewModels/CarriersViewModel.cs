using MPAG_Final.Services;
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
        private Carrier _selectedCarrier;
        public Carrier SelectedCarrier
        {
            get { return _selectedCarrier; }
            set { OnPropertyChanged(ref _selectedCarrier, value); }
        }

        public ObservableCollection<Carrier> Carriers { get; private set; }
        public ICommand UpdateCommand { get; private set; }


        //mock data service for testing UI
        private ICarrierDataService _carrierDataService;

        public CarriersViewModel(ICarrierDataService carrierDataService)
        {
            _carrierDataService = carrierDataService;
            UpdateCommand = new RelayCommand(Update);
        }

        private void Update()
        {
            //add this later
        }

        public void LoadCarriers(IEnumerable<Carrier> carrier)
        {
            Carriers = new ObservableCollection<Carrier>(carrier);
            OnPropertyChanged("Carriers");
        }
    }
}
