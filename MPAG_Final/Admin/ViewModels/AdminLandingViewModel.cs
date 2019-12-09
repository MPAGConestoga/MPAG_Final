using MPAG_Final.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPAG_Final.Admin.ViewModels
{
    public class AdminLandingViewModel : ObservableObject
    {

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

        private String selectedDate;
        public String SelectedDate
        {
            get { return selectedDate; }
            set { OnPropertyChanged(ref selectedDate, value); }
        }

    }
}
