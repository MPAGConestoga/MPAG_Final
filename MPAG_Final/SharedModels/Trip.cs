using MPAG_Final.SharedModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPAG_OrderAndTrip
{
    /**
     * \brief   The Trip class represent the one trip associated with an order.
     * \details The Trip links the order to the depots, which in turn links one order to all the carriers that are completing that order.
     *          It contains the last stop, the total of hours and kilometers required to completed a order.
     */
    public class Trip
    {
        // Represents the link between Order and Carrier (through Depot)
        public int OrderID { get; set; }
        public int DepotID { get; set; }

        // Trip Information 
        public DateTime StartedAt { get; set; }
        public DateTime EndsAt { get; set; }
        private double _kilometersPerHour;
        public double KilometersPerHour
        {
            get { return _kilometersPerHour; }
            set
            {
                _kilometersPerHour = value;
            }
        }

        /// <summary>
        /// Constructor for the Trip class. It requires the initial last stop (where the cargo will be unloaded), and the cumuluative hours and kilometers to finish.
        /// </summary>
        /// <param name="initialStop"> <b>string</b> - The city where the cargo will be unloaded </param>
        /// <param name="initialHours"> <b>int</b> - represent the initial hours required to do a trip </param>
        /// <param name="initialKm"> <b>double</b> - represent the initial kilometers required to complete an order </param>
        public Trip(Order selectedOrder, Carrier selectedCarrier, double initialKmH = 60.0)         // Km and hours should be self generated        
        {
            StartedAt = DateTime.Now;

            OrderID = selectedOrder.OrderID;
            DepotID = selectedCarrier.DepotsLocation[selectedOrder.origin].DepotID;

            KilometersPerHour = initialKmH;

            //--> DEBUG: Get origin and destination to get total kilometers for a trip
            double positionDelta = 100;

            // Determine initial ETA
            TimeSpan timeInterval = TimeSpan.FromHours(positionDelta / KilometersPerHour);
            EndsAt = StartedAt + timeInterval;

            // Algorithm for determining the ETA by taking the origin and destination-------//
            // Get start city and end city -> Determine total Amount of kilometers
            // Use the average speed to determine the amount of hours will take 
            EndsAt = StartedAt.AddHours(10);
            //------------------- END OF DEBUG ---------------------------------------------//
        }
    }
}
