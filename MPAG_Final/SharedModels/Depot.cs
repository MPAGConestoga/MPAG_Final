using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPAG_OrderAndTrip
{
    /**
    * \brief   The Depot represent one location where the Carrier has a delivery station.
    * \details The Depot will represent the a city where a Carrier has a delivery station, this means one carrier company will be able to make deliveries
    *          that has the origin in one of its depot locations. Each Depot will contain the availability of trucks (FTL and LTL) and a collection of trips
    *          that had the origin in that depot city.
    */
    public class Depot
    {
        public int DepotID { get; set; }
        string location;
        public int availibleFTL { get; set; }
        public int avalibleLTL { get; set; }
        public List<Trip> Trips;

        /// <summary>
        /// Constructor for the Depot. Requires 
        /// </summary>
        /// <param name="city"> <b>string</b> - City location of the carrier's depot </param>
        /// <param name="initialFTLs"> <b>int</b> - Initial quantity of FTL trucks in that depot </param>
        /// <param name="initialLTLs"> <b>int</b> - Initial quantity of LTL trucks in that depot </param>
        public Depot(string city, int initialFTLs, int initialLTLs)
        {
            location = city;
            availibleFTL = initialFTLs;
            avalibleLTL = initialLTLs;
        }
    }
}
