using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MPAG_Final.Services;
using MPAG_Final.SharedViewModels;
using MPAG_Final.Utilities;


namespace MPAG_Final.Planner.ViewModels
{
    public class PlannerLandingViewModel : ObservableObject
    {
        private OrderViewModel _orderVM;
        public OrderViewModel OrderVM
        {
            get { return _orderVM; }
            set { OnPropertyChanged(ref _orderVM, value); }
        }

        private SummaryReportsViewModel _summaryVM;
        public SummaryReportsViewModel SummaryVM
        {
            get { return _summaryVM; }
            set { OnPropertyChanged(ref _summaryVM, value); }
        }

        private object _currentView;
        public object CurrentView
        {
            get { return _currentView; }
            set { OnPropertyChanged(ref _currentView, value); }
        }

        

        /// <summary>
        /// Constructor that instantiates a new instance of the OrderViewModel class
        /// </summary>
        public PlannerLandingViewModel()
        {
            var contractMarketPlace = new MockContractMarketplace(); //mock service for the testing of the ui


            OrderVM = new OrderViewModel();
            SummaryVM = new SummaryReportsViewModel();
            
            CurrentView = OrderVM;

        }
    }
}
