using MPAG_Final.Planner.Models;
using MPAG_Final.Services;
using MPAG_Final.SharedModels;
using MPAG_Final.SharedViewModels;
using MPAG_Final.Utilities;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Windows.Input;

namespace MPAG_Final.Planner.ViewModels
{
    /**
   * \Class OrderViewModel
   * \Brief View model for the handling of the order view
   * \Details The order view model loads and handles the information that will be 
   * displayed to the user in the planner role. Pending and active orders will be 
   * loaded, as well as the carriers to be attached to the orders. Will also 
   * instantiate instances of the child view models: PendingViewModel and ActiveViewModel.
   */
    public class OrderViewModel : ObservableObject
    {

        // Current selected order information
        private Order FirstOrderSelected = null;
        public ICommand CheckOrderCommand { get; private set; }
        // For accessing planner methods 

        private ICarrierDataService _carrierService;
        private IContractDataService _contractService;

        // for accessing the contracts view model
        private ContractsViewModel _contractsVM;
        public ContractsViewModel ContractsVM
        {
            get { return _contractsVM; }
            set { OnPropertyChanged(ref _contractsVM, value); }
        }
        // for accessing the contracts view model_plannerRole

        private PlannerRole _plannerRoleVM;
        public PlannerRole PlannerRoleVM
        {
            get { return _plannerRoleVM; }
            set { OnPropertyChanged(ref _plannerRoleVM, value); }
        }

        //-> Order's lists
        private ObservableCollection<Order> _ftlOrders;
        public ObservableCollection<Order> FTLOrders
        {
            get { return _ftlOrders; }
            set
            {
                _ftlOrders = value;
                OnPropertyChanged("FTLOrders");

            }
        }

        private ObservableCollection<Order> _ltlOrders;
        public ObservableCollection<Order> LTLOrders
        {

            get { return _ltlOrders; }
            set
            {
                _ltlOrders = value;
                OnPropertyChanged("LTLOrders");


            }
        }



            //ContractsVM = new ContractsViewModel();
            //CarriersVM = new CarriersViewModel(carrierMarketPlace);
            //LoadCarriers();
            //LoadContracts();
        //-> Relevant Carriers
        private ObservableCollection<Carrier> _relevantCarriers;
        public ObservableCollection<Carrier> RelevantCarriers
        {
            get { return _relevantCarriers; }
            set
            {
                _relevantCarriers = value;
                OnPropertyChanged("RelevantCarriers");
            }
        }

            //ContractsVM = new ContractsViewModel();
            //CarriersVM = new CarriersViewModel(carrierMarketPlace);
            //LoadCarriers();
            //LoadContracts();
        //-> Selected Propreties
        private ObservableCollection<Order> _selectedOrders;
        public ObservableCollection<Order> SelectedOrders
        {
            get { return _selectedOrders; }
            set
            {
                _selectedOrders = value;
                OnPropertyChanged("SelectedOrders");

            }
        }


        private List<Carrier> _selectedCarriers = new List<Carrier>();
        public List<Carrier> SelectedCarriers
        {
            //ContractsVM.LoadContracts(_contractService.GetContracts());
            get { return _selectedCarriers; }
            set
            {
                _selectedCarriers = value;
                OnPropertyChanged("SelectedCarriers");

            }
        }




        /// <summary>
        ///     Constructor that instantiates a new instance of the OrderViewModel class
        /// </summary>
        public OrderViewModel(PlannerRole planner)
        {
            PlannerRoleVM = planner;

            // DEBUG: Include FTL Order 
           // LTLOrders = new ObservableCollection<Order>(new SampleData().SampleLTLOrders());
            SelectedOrders = new ObservableCollection<Order>();
            CheckOrderCommand = new SelectOrder(this);
        }

        public void OrderChecked(object parameter)
        {
            var list = (object[])parameter;
            Order selectedOrder = (Order)list[0];
            if (FirstOrderSelected == null)
            {
                FirstOrderSelected = selectedOrder;

                // Populate LTL order 
                //LTLOrders = new ObservableCollection<Order>(new SampleData().FilterLTLs(selectedOrder.origin, selectedOrder.vanType));
                LTLOrders.Remove(selectedOrder);
                OnPropertyChanged("LTLOrders");

                // Send selected order to bundled orders
                SelectedOrders.Add(selectedOrder);
            }
            else
            {
                SelectedOrders.Add(selectedOrder);
                LTLOrders.Remove(selectedOrder);
            }

            OnPropertyChanged("SelectedOrders");

            // Populate Relevant Carriers list
            //RelevantCarriers = new ObservableCollection<Carrier>(new SampleData().GetRelevantCarrier(selectedOrder.origin, selectedOrder.destination));

            OnPropertyChanged("RelevantCarriers");
        }
    }
}
