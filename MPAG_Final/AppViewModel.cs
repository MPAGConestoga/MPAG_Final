using MPAG_Final.Services;
using MPAG_Final.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MPAG_Final
{
    /**
    * \Class OrderViewModel
    * \Brief View model for the handling of the main window view
    * \Details The app view model loads and handles the information that will be 
    * displayed to the user on the landing page for the planner role. Instantiates
    * a child instance of the order view model which will be displayed in the window beneath
    * the upper bar of the app.
    */
    public class AppViewModel : ObservableObject
    {
        private object _currentView;
        public object CurrentView
        {
            get { return _currentView; }
            set { OnPropertyChanged(ref _currentView, value); }
        }

        private Planner.ViewModels.PlannerLandingViewModel _plannerVM;
        public Planner.ViewModels.PlannerLandingViewModel PlannerVM
        {
            get { return _plannerVM; }
            set { OnPropertyChanged(ref _plannerVM, value); }
        }

        private Buyer.ViewModels.BuyerLandingViewModel _buyerVM;
        public Buyer.ViewModels.BuyerLandingViewModel BuyerVM
        {
            get { return _buyerVM; }
            set { OnPropertyChanged(ref _buyerVM, value); }
        }

        public ICommand LoadBuyerCommand { get; private set; }
        public ICommand LoadPlannerCommand { get; private set; }

        public AppViewModel()
        {
            BuyerVM = new Buyer.ViewModels.BuyerLandingViewModel();
            PlannerVM = new Planner.ViewModels.PlannerLandingViewModel();

            LoadBuyerCommand = new RelayCommand(LoadBuyer);
            LoadPlannerCommand = new RelayCommand(LoadPlanner);

        }

        private void LoadBuyer()
        {
            CurrentView = BuyerVM;

        }

        private void LoadPlanner()
        {
            CurrentView = PlannerVM;
        }
    }
}
