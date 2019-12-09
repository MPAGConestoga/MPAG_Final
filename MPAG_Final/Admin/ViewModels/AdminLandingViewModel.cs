using MPAG_Final.SharedModels;
using MPAG_Final.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPAG_Final.Admin.ViewModels
{
    /**
    * \Class AdminLandingViewModel
    * \Brief View model for the handling of Administrative model
    * \Details The Admin view model loads and handles the information that will be 
    * displayed to the user for logging information and changing the database IP and root
    */
    public class AdminLandingViewModel : ObservableObject
    {
        /// <summary>
        ///      Retrieves the date of today from the calendar view.
        ///      If the calendar has been changed the date property is changed.
        /// </summary>
        private Nullable<DateTime> myDateTimeProperty;
        public Nullable<DateTime> MyDateTimeProperty
        {
            get
            {
                if (myDateTimeProperty == null)
                {
                    myDateTimeProperty = DateTime.Today;
                    DateTime dateBuffer = (DateTime)myDateTimeProperty;
                    SelectedDate = dateBuffer.ToString("yyyy-MM-dd");
                }
                return myDateTimeProperty;
            }
            set
            {
                myDateTimeProperty = value;
                DateTime dateBuffer = (DateTime)myDateTimeProperty;
                SelectedDate = dateBuffer.ToString("yyyy-MM-dd");
                OnPropertyChanged("MyDateTimeProperty");
            }
        }
        public ObservableCollection<Carrier> AllCarriers { get; set; }


        /// <summary>
        ///     The selected dat is converted into a string to be readied 
        ///     and prepared to find the file that is required to be accessed
        /// </summary>
        private String selectedDate;
        public String SelectedDate
        {
            get { return selectedDate; }
            set { OnPropertyChanged(ref selectedDate, value); }
        }
        public AdminLandingViewModel()
        {
            AllCarriers = new ObservableCollection<Carrier>(new TMSDAL().GetAllCarriers());      
        }

        public void UpdateCarrierRates()
        {
            foreach (Carrier el in AllCarriers)
            {
                new TMSDAL().UpdateCarrierRates(el);
            }
            AllCarriers.Clear();
            var list = new TMSDAL().GetAllCarriers();
            foreach(Carrier el in list)
            {
                AllCarriers.Add(el);
            }
        }
    }
}
