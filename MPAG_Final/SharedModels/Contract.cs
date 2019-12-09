
using MPAG_Final.SharedViewModels;
using MPAG_Final.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPAG_Final.SharedModels
{
    /**
     * \Class   Contract
     * \brief   Used for creating contracts
     * \details Creates type of contract was selected by the buyer
     */

    public class Contract : ObservableObject
    {
        private int orderID;
        public int OrderID
        {
            get { return orderID; }
            set { OnPropertyChanged(ref orderID, value); }
        }
        private int quantity;
        public int Quantity
        {
            get { return quantity; }
            set { OnPropertyChanged(ref quantity, value); }
        }
        private string customer;
        public string Customer
        {
            get { return customer; }
            set { OnPropertyChanged(ref customer, value); }
        }

        private int customerID;
        public int CustomerID
        {
            get { return customerID; }
            set { OnPropertyChanged(ref customerID, value); }
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
        private int originID;
        public int OriginID
        {
            get { return originID; }
            set { OnPropertyChanged(ref originID, value); }
        }

        private int destinationID;
        public int DestinationID
        {
            get { return destinationID; }
            set { OnPropertyChanged(ref destinationID, value); }
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

    //public enum City
    //{
    //    Windsor,
    //    London,
    //    Hamilton,
    //    Toronto,
    //    Oshawa,
    //    Belleville,
    //    Kingston,
    //    Ottawa
    //}
}
