using MPAG_Final.SharedViewModels;
using MPAG_Final.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPAG_Final.SharedModels
{
    public class Carrier : ObservableObject
    {
        private int carrierID;
        public int CarrierID
        {
            get { return carrierID; }
            set { OnPropertyChanged(ref carrierID, value); }
        }

        private string carrierName;
        public string CarrierName
        {
            get { return carrierName; }
            set { OnPropertyChanged(ref carrierName, value); }
        }

        private int availableTrucks;
        public int AvailableTrucks
        {
            get { return availableTrucks; }
            set { OnPropertyChanged(ref availableTrucks, value); }
        }

        
    }
}
