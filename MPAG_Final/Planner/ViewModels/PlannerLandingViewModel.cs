using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
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

            OrderVM = new OrderViewModel();
            CurrentView = OrderVM;

        }
    }
}
