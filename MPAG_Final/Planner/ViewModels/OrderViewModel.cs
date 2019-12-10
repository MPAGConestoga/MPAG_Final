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
        TimeSpan maxDrivingTime = new TimeSpan(8, 0, 0);
        TimeSpan maxPathTime = new TimeSpan(12, 0, 0);

        // Current selected order information
        private Order FirstOrderSelected = null;
        private Order FTLOrderSelected = null;
        private Order CarrierSelectedFTL = null;
        public ICommand CheckOrderCommand { get; private set; }
        public ICommand CheckFTLOrderCommand { get; private set; }
        public ICommand RemoveOrderCommand { get; private set; }
        // For accessing planner methods 

        public ICommand ResetCommand { get; private set; }
        private ICarrierDataService _carrierService;
        private IContractDataService _contractService;

        private int _slider;
        public int Slider
        {
            //mock service for the testing of the ui

            get { return _slider; }
            set
            {
                _slider = value;
                OnPropertyChanged("Slider");
            }
        }


        private string _message;
        public string Message
        {
            //mock service for the testing of the ui
            get { return _message; }
            set
            {
                _message = value;
                OnPropertyChanged("Message");
            }
        }



        // for accessing the contracts view model
        private ContractsViewModel _contractsVM;
        public ContractsViewModel ContractsVM
        {
            get { return _contractsVM; }
            set { OnPropertyChanged(ref _contractsVM, value); }
        }
        // for accessing the contracts view model_plannerRole

        private PlannerRole _plannerRoleVM;

        /// <summary>
        ///     Property for the planer role view model
        /// </summary>
        public PlannerRole PlannerRoleVM
        {
            get { return _plannerRoleVM; }
            set { OnPropertyChanged(ref _plannerRoleVM, value); }
        }

        //-> Order's lists
        private ObservableCollection<Order> _ftlOrders;
        /// <summary>
        ///         Creates an order list for the FTL orders
        /// </summary>
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
        /// <summary>
        ///         Creates an order list for the LTL's
        /// </summary>
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
        /// <summary>
        ///         Relevant carriers' list for the use of the order
        /// </summary>
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
        /// <summary>
        ///     Sets the selected order from the grid view
        /// </summary>
        public ObservableCollection<Order> SelectedOrders
        {
            get { return _selectedOrders; }
            set
            {
                _selectedOrders = value;
                OnPropertyChanged("SelectedOrders");

            }
        }

        private int BundledOrigin = -1;
        private int BundledDestination = -1;
        TimeSpan TotalPath = TimeSpan.Zero;
        TimeSpan TotalDriving = TimeSpan.Zero;
        private int direction;


        private List<Carrier> _selectedCarriers = new List<Carrier>();
        /// <summary>
        ///     Carrier that has been chosen
        /// </summary>
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

        private ObservableCollection<Carrier> _relevantCarriersFTL;
        /// <summary>
        ///         Relevant carriers' list for the use of the order
        /// </summary>
        public ObservableCollection<Carrier> RelevantCarriersFTL
        {
            get { return _relevantCarriersFTL; }
            set
            {
                _relevantCarriersFTL = value;
                OnPropertyChanged("RelevantCarriersFTL");
            }
        }




        /// <summary>
        ///     Constructor that instantiates a new instance of the OrderViewModel class
        /// </summary>
        public OrderViewModel(PlannerRole planner)
        {
            PlannerRoleVM = planner;
            RelevantCarriers = new ObservableCollection<Carrier>();
            RelevantCarriersFTL = new ObservableCollection<Carrier>();

            FTLOrders = new ObservableCollection<Order>(new TMSDAL().GetOrdersByJobType(1));

            LTLOrders = new ObservableCollection<Order>(new TMSDAL().GetOrdersByJobType(0));
            LTLOrdersMaster = new ObservableCollection<Order>(new TMSDAL().GetOrdersByJobType(0));

            SelectedOrders = new ObservableCollection<Order>();
            CheckOrderCommand = new SelectOrder(this);
            CheckFTLOrderCommand = new SelectFTLOrder(this);
            ResetCommand = new RelayCommand(ResetFilters);
            RemoveOrderCommand = new RemoveOrder(this);

            ActiveOrders = new ObservableCollection<Order>()
            {
                new Order(true, 10, "Toronto", "Windsor", false),
                new Order(true, 4, "Waterloo", "London", false )
            };
            Slider = 50;
        }

        /// <summary>
        ///     Reset the filters in the order view
        /// </summary>
        public void ResetFilters()
        {
            LTLOrders.Clear();
            LTLOrdersMaster.Clear();
            RelevantCarriers.Clear();
            SelectedOrders.Clear();
            FirstOrderSelected = null;
            BundledOrigin = -1;

            var list = new TMSDAL().GetOrdersByJobType(0);

            foreach (Order el in list)
            {
                LTLOrders.Add(el);
                LTLOrdersMaster.Add(el);
            }
        }

        public void FTLOrderCheck(object parameter)
        {
            Message = "";
            TMSDAL DAL = new TMSDAL();
            int ID = Convert.ToInt32(parameter);
            Order selectedOrder = DAL.GetOrderByID(ID);

            if (RelevantCarriersFTL.Count == 0)
            {
                FTLOrderSelected = selectedOrder;
                var list = new ObservableCollection<Carrier>(DAL.GetCarriersByCityID(selectedOrder.origin, selectedOrder.destination));
                foreach (Carrier el in list)
                {
                    el.TargetDepot = DAL.GetCityDepotByCarrierAndCity(el.carrierId, selectedOrder.origin);
                    RelevantCarriersFTL.Add(el);
                }
            }




        }


        /// <summary>
        ///     Check the order type and how many have been selected
        /// </summary>
        /// <param name="parameter"><b>object</b> - order ID being evaluated</param>

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

            LTLOrders.Remove(selectedOrder);
            OnPropertyChanged("LTLOrders");

            if (RelevantCarriers.Count != 0)
            {
                direction = CalculateDirection(selectedOrder.origin, selectedOrder.destination);
                // First Order
                if (FirstOrderSelected == null)
                {
                    FirstOrderSelected = selectedOrder;
                    if (selectedOrder.origin < selectedOrder.destination)
                    {
                        BundledOrigin = selectedOrder.origin;
                        BundledDestination = selectedOrder.destination;
                    }
                    else
                    {
                        BundledOrigin = selectedOrder.destination;
                        BundledOrigin = selectedOrder.origin;
                    }

                    TimeSpan drivingTime = TimeSpan.Zero;
                    drivingTime = SetUpPathInfo(BundledOrigin - 1, BundledDestination);
                    TimeSpan totalPathTime = drivingTime + new TimeSpan(((BundledDestination - BundledOrigin) + 1) * 2, 0, 0);
                    if (drivingTime > maxDrivingTime || totalPathTime > maxPathTime)
                    {
                        Message = "Trip time exceeds limit - Remove some orders to continue";
                    }
                    else
                    {
                        TotalDriving = drivingTime;
                        TotalPath = totalPathTime;
                    }


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
                    // Other orders                
                    int lowerIndex = selectedOrder.origin;
                    int higherIndex = selectedOrder.destination;

                    if (direction == 0)
                    {
                        lowerIndex = selectedOrder.destination;
                        higherIndex = selectedOrder.origin;
                    }

                    // If order being added will add more time to the trip -> check if doesnt surprass the time allowed
                    if (higherIndex > BundledDestination)
                    {
                        TimeSpan newTime = SetUpPathInfo(BundledOrigin - 1, higherIndex);
                        TimeSpan newTotal = newTime + new TimeSpan(((BundledDestination - BundledOrigin) + 1) * 2, 0, 0);

                        if (newTime > maxDrivingTime || newTotal > maxPathTime)
                        {
                            Message = "Trip time exceeds limit - Remove some orders to continue";
                        }
                        else
                        {
                            TotalDriving = newTime;
                            TotalPath = newTotal;
                        }
                    }



                    SelectedOrders.Add(selectedOrder);

                    // Check 


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
                foreach (Order el in LTLOrdersMaster)
                {
                    if ((el.origin == selectedOrder.origin) && (el.vanType == selectedOrder.vanType) && (CalculateDirection(el.origin, el.destination) == direction))
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


        

            // Populate Relevant Carriers list
            //RelevantCarriers = new ObservableCollection<Carrier>(new SampleData().GetRelevantCarrier(selectedOrder.origin, selectedOrder.destination));

        /// <summary>
        ///     Remove order from list
        /// </summary>
        /// <param name="parameter"><b>object</b> - order to be removed</param>
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

        /// <summary>
        ///     Calculate the direction of the order travel
        /// </summary>
        /// <param name="origin"><b>int</b> -  </param>
        /// <param name="destination"></param>
        /// <returns></returns>
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

        private TimeSpan SetUpPathInfo(int start, int stop)
        {
            float hours = 0;
            int counter = start;

            while (counter < stop)
            {
                hours += (new TMSDAL().GetTimeAndDistance(counter++)).Item2;
            }

            int totalHours = (int)hours;
            int totalMinutes = (int)((hours - totalHours) * 60);

            return new TimeSpan(totalHours, totalMinutes, 0);
        }
    }
}
