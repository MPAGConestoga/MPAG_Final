using MPAG_Final.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace MPAG_Final.Planner.Models
{
    /**
    * \Class PendingOrders
    * \Brief Model for storage of pending order data
    * \Details The pendingOrders model contains the getters and setters for the
    * data displayed for pending orders.
    */
    public class ActiveOrders : ObservableObject
    {
        string connectionString = ConfigurationManager.ConnectionStrings["contractmarketplace"].ConnectionString;
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

        private int attachedCarriers;
        public int AttachedCarriers
        {
            get { return attachedCarriers; }
            set { OnPropertyChanged(ref attachedCarriers, value); }
        }
    }
}
