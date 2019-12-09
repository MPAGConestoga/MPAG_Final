using MPAG_Final.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPAG_Final.Planner.Models
{
    /**
    * \Class PendingOrders
    * \Brief Model for storage of pending order data
    * \Details The pendingOrders model contains the getters and setters for the
    * data displayed for pending orders.
    */
    public class PendingModel : ObservableObject
    {
        private int orderID;
        public int OrderID
        {
            get { return orderID; }
            set { OnPropertyChanged(ref orderID, value); }
        }

        private string customer;
        public string Customer
        {
            get { return customer; }
            set { OnPropertyChanged(ref customer, value); }
        }

        private JobType jobType;
        public JobType JobType
        {
            get { return jobType; }
            set { OnPropertyChanged(ref jobType, value); }
        }

        private string origin;
        public string Origin
        {
            get { return origin; }
            set { OnPropertyChanged(ref origin, value); }
        }

        private string destination;
        public string Destination
        {
            get { return destination; }
            set { OnPropertyChanged(ref destination, value); }
        }

        private VanType vanType;
        public VanType VanType
        {
            get { return vanType; }
            set { OnPropertyChanged(ref vanType, value); }
        }
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

}
