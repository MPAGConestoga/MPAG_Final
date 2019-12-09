using MPAG_Final.Planner.Models;
using MPAG_Final.Services;
using MPAG_Final.SharedModels;
using MPAG_Final.SharedViewModels;
using MPAG_Final.Utilities;
using System;
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
        public ICommand RemoveOrderCommand { get; private set; }
        // For accessing planner methods 

        public ICommand ResetCommand { get; private set; }
        private ICarrierDataService _carrierService;
        private IContractDataService _contractService;


        private string _message;
        public string Message
        {
            //mock service for the testing of the ui

            get { return _message; }
            set
            {
                _message = value;
                OnPropertyChanged("Message");



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


        public ObservableCollection<Order> LTLOrdersMaster { get; set; }



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

        private Order _selectedActiveOrder;
        public Order SelectedActiveOrder
        {
            get { return _selectedActiveOrder; }
            set { OnPropertyChanged(ref _selectedActiveOrder, value); }
        }


        private ObservableCollection<Order> _activeOrders;
        public ObservableCollection<Order> ActiveOrders
        {

            get { return _activeOrders; }
            set
            {
                _activeOrders = value;
                OnPropertyChanged("ActiveOrders");
            }
        }


        /// <summary>
        ///     Constructor that instantiates a new instance of the OrderViewModel class
        /// </summary>
        public OrderViewModel(PlannerRole planner)
        {
            PlannerRoleVM = planner;
            RelevantCarriers = new ObservableCollection<Carrier>();
            //ContractsVM = new ContractsViewModel();
            //CarriersVM = new CarriersViewModel(carrierMarketPlace);
            //LoadCarriers();
            //LoadContracts();
            // DEBUG: Include FTL Order 

            LTLOrders = new ObservableCollection<Order>(new TMSDAL().GetOrdersByJobType(0));
            LTLOrdersMaster = new ObservableCollection<Order>(new TMSDAL().GetOrdersByJobType(0));
            
            SelectedOrders = new ObservableCollection<Order>();
            CheckOrderCommand = new SelectOrder(this);
            ResetCommand = new RelayCommand(ResetFilters);
            RemoveOrderCommand = new RemoveOrder(this);

           // LTLOrders = new ObservableCollection<Order>(new SampleData().SampleLTLOrders());
            SelectedOrders = new ObservableCollection<Order>();
            CheckOrderCommand = new SelectOrder(this);
            ActiveOrders = new ObservableCollection<Order>()
            {
                new Order(true, 10, "Toronto", "Windsor", false),
                new Order(true, 4, "Waterloo", "London", false )
            };

        }


        public void ResetFilters()
        {
            LTLOrders.Clear();
            LTLOrdersMaster.Clear();
            RelevantCarriers.Clear();
            SelectedOrders.Clear();
            FirstOrderSelected = null;

            var list = new TMSDAL().GetOrdersByJobType(0);

            foreach(Order el in list)
            {
                LTLOrders.Add(el);
                LTLOrdersMaster.Add(el);
            }
        }
        public void OrderChecked(object parameter)
        {
            Message = "";
            TMSDAL DAL = new TMSDAL();
            int ID = Convert.ToInt32(parameter);
            Order selectedOrder = DAL.GetOrderByID(ID);
            if (SelectedOrders.Count == 0)
            {
                var list = new ObservableCollection<Carrier>(DAL.GetCarriersByCityID(selectedOrder.origin, selectedOrder.destination));
                foreach (Carrier el in list)
                {
                    el.TargetDepot = DAL.GetCityDepotByCarrierAndCity(el.carrierId, selectedOrder.origin);
                    RelevantCarriers.Add(el);

                }
            }

                // Populate LTL order 
                //LTLOrders = new ObservableCollection<Order>(new SampleData().FilterLTLs(selectedOrder.origin, selectedOrder.vanType));
                LTLOrders.Remove(selectedOrder);
                OnPropertyChanged("LTLOrders");

            if (RelevantCarriers.Count != 0)
            {
                if (FirstOrderSelected == null)
                {
                    FirstOrderSelected = selectedOrder;

                    // Populate LTL order 
                    //LTLOrders = new ObservableCollection<Order>(new SampleData().FilterLTLs(selectedOrder.origin, selectedOrder.vanType));
                    //LTLOrders.Remove(selectedOrder);
                    foreach (Order el in LTLOrdersMaster)
                    {
                        if (el.OrderID == selectedOrder.OrderID)
                        {
                            LTLOrdersMaster.Remove(el);
                            break;
                        }
                    }
                    //remove from list so it doesn't show in the view

                    LTLOrders.Clear();
                    OnPropertyChanged("LTLOrders");

                    // Send selected order to bundled orders
                    SelectedOrders.Add(selectedOrder);
                }
                else
                {
                    SelectedOrders.Add(selectedOrder);
                    foreach (Order el in LTLOrdersMaster)
                    {
                        if (el.OrderID == selectedOrder.OrderID)
                        {
                            LTLOrdersMaster.Remove(el);
                            break;
                        }
                        LTLOrders.Clear();

                    }
                }
                //foreach (Order el in LTLOrdersMaster)
                //{
                //    if (el.OrderID ==selectedOrder.OrderID)
                //    {
                //        LTLOrdersMaster.Remove(el);
                //        break;
                //    }
                //}
                ////remove from list so it doesn't show in the view

               LTLOrders.Clear();
                int direction = CalculateDirection(selectedOrder.origin, selectedOrder.destination);
                foreach (Order el in LTLOrdersMaster)
                {
                    if((el.origin==selectedOrder.origin) && (el.vanType == selectedOrder.vanType) && (CalculateDirection(el.origin,el.destination) == direction))
                    {
                        LTLOrders.Add(el);
                    }
                }
                   
            }
            else
            {
                Message = "No Carrier has the specified origin and destination. Deleting Order.";
                new TMSDAL().DeleteOrder(selectedOrder.OrderID);
                foreach (Order el in LTLOrdersMaster)
                {
                    if (el.OrderID == selectedOrder.OrderID)
                    {
                        LTLOrdersMaster.Remove(el);
                        break;
                    }
                }
                foreach (Order el in LTLOrders)
                {
                    if (el.OrderID == selectedOrder.OrderID)
                    {
                        LTLOrders.Remove(el);
                        break;
                    }
                }
            }

            // OnPropertyChanged("SelectedOrders");

            // // Populate Relevant Carriers list

            //OnPropertyChanged("RelevantCarriers");

        }

            // Populate Relevant Carriers list
            //RelevantCarriers = new ObservableCollection<Carrier>(new SampleData().GetRelevantCarrier(selectedOrder.origin, selectedOrder.destination));


        public void RemoveOrder(object parameter)
        {
            int ID = Convert.ToInt32(parameter);
            Order selectedOrder = new TMSDAL().GetOrderByID(ID);
            foreach (Order el in SelectedOrders)
            {
                if (el.OrderID == selectedOrder.OrderID)
                {
                    SelectedOrders.Remove(el);
                    LTLOrders.Add(el);
                    break;
                }
               
                
                
            }
            if (SelectedOrders.Count == 0)
            {
                FirstOrderSelected = null;
                var list = new TMSDAL().GetOrdersByJobType(0);
                LTLOrdersMaster.Clear();
                LTLOrders.Clear();
                foreach (Order el in list)
                {
                    LTLOrders.Add(el);
                    LTLOrdersMaster.Add(el);
                }
                RelevantCarriers.Clear();
            }
        }

        public int CalculateDirection(int origin, int destination)
        {
            TMSDAL DAL = new TMSDAL();
            int originX = DAL.GetXValue(origin);
            int destinationX = DAL.GetXValue(destination);
            if (originX < destinationX)
            {
                return 2; //going east
            }
            else
            {
                return 0; //going west
            }
        }

        public void ActivateOrders()
        {
            int orderCounter = 0;
            int carrierCounter = 0;

            // Only Pallets 
            while (orderCounter < SelectedOrders.Count)
            {
                // Convert to FTL truck if possible
                if (SelectedOrders[orderCounter].quantity % Carrier.MaxLot == 0 && SelectedCarriers[carrierCounter].TargetDepot.availibleFTL > 0)
                {
                    int howManyFTL = SelectedCarriers[carrierCounter].TargetDepot.availibleFTL;
                    SelectedOrders[orderCounter].quantity -= (howManyFTL * Carrier.MaxLot);
                }
                // Remove from pallets 
                else
                {
                    int howMuchToRemoved = SelectedCarriers[carrierCounter].TargetDepot.avalibleLTL;

                    SelectedOrders[orderCounter].quantity -= howMuchToRemoved;
                    SelectedCarriers[carrierCounter].TargetDepot.avalibleLTL -= howMuchToRemoved;
                }

                // Add trips
                PlannerRoleVM.AddTrip(SelectedCarriers[carrierCounter], SelectedOrders[orderCounter]);
                if (SelectedOrders[orderCounter].quantity == 0)
                {
                    orderCounter++;
                }

                // Move to next carrier
                carrierCounter++;
                SelectedOrders[orderCounter].status = 2;
                ActiveOrders.Add(SelectedOrders[orderCounter]);
            }


        }
    }
}
