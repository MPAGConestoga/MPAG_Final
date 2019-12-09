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

        public double baseSpeed = 60;
        TimeSpan timeAdvanced = TimeSpan.Zero;

        // Trip Information 
        public DateTime StartedAt { get; set; }
        public DateTime CurrentTime
        {
            get { return DateTime.Now + timeAdvanced; }
        }

        public DateTime EndsAt { get; set; }
   

        public double positionDelta { get; set; }

        /// <summary>
        /// Constructor for the Trip class. It requires the initial last stop (where the cargo will be unloaded), and the cumuluative hours and kilometers to finish.
        /// </summary>
        /// <param name="initialStop"> <b>string</b> - The city where the cargo will be unloaded </param>
        /// <param name="initialHours"> <b>int</b> - represent the initial hours required to do a trip </param>
        /// <param name="initialKm"> <b>double</b> - represent the initial kilometers required to complete an order </param>
        public Trip(Order selectedOrder, Carrier selectedCarrier)         // Km and hours should be self generated        
        {
            StartedAt = DateTime.Now;
            CurrentTime = DateTime.Now;

            OrderID = selectedOrder.OrderID;
            DepotID = selectedCarrier.DepotsLocation[selectedOrder.origin].DepotID;

            //--> DEBUG: Get origin and destination to get total kilometers for a trip
            positionDelta = 100;

            // Determine initial ETA
            TimeSpan timeInterval = TimeSpan.FromHours(positionDelta / baseSpeed);
            EndsAt = StartedAt + timeInterval;
        }

        public void SpeedUp(int increment)
        {
            CurrentTime.AddDays(increment);
            TimeSpan secondsIncrement = TimeSpan.FromDays(increment);
            timeAdvanced.Add(secondsIncrement);
        }
    }
}
