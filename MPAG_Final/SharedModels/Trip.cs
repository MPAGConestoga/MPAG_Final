using MPAG_Final.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPAG_Final.SharedModels
{
    /**
      * \brief   The Trip class represent the one trip associated with an order.
      * \details The Trip links the order to the depots, which in turn links one order to all the carriers that are completing that order.
      *          It contains the last stop, the total of hours and kilometers required to completed a order.
      */
    public class Trip
    {
        public string lastStop { get; set; }
        private int _addedHours;
        int addedHours
        {
            get { return _addedHours; }
            set
            {
                _addedHours += value;
            }
        }

        private double _addedKilometers;
        public double addedKilometers
        {
            get { return _addedKilometers; }
            set
            {
                _addedKilometers += value;
            }
        }

        /// <summary>
        /// Constructor for the Trip class. It requires the initial last stop (where the cargo will be unloaded), and the cumuluative hours and kilometers to finish.
        /// </summary>
        /// <param name="initialStop"> <b>string</b> - The city where the cargo will be unloaded </param>
        /// <param name="initialHours"> <b>int</b> - represent the initial hours required to do a trip </param>
        /// <param name="initialKm"> <b>double</b> - represent the initial kilometers required to complete an order </param>
        public Trip(string initialStop, int initialHours, double initialKm)         // Km and hours should be self generated        
        {
            lastStop = initialStop;
            _addedHours = initialHours;
            _addedKilometers = initialKm;
        }
    }
}
