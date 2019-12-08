using MPAG_Final.Utilities;

namespace MPAG_Final.SharedModels
{
    /**
    * \brief   The Carrier class represents the companies that will serve the Customer's orders. 
    * \details The Carrier will represent the trucking companies. Each carrier will have multiple Depots, spread across multiple cities
    *          to serve orders from different places. Each Carrier will have an unique identifier, name and contact information, trip rates
    *          based on the order type and reefer charge.
    */
    public class Carrier
    {
        /* ---------- Attributes --------------- */
        // Contact information
        public int carrierId { get; set; }
        public string carrierName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        // Business Information
        public Dictionary<string, Depot> CityDepots = null;             // Key can be city ID???
        private double FTLRate;
        private double LTLRate;
        private double ReeferCharge;
        public const int MaxLot = 26;

        /* ---------- Constructors --------------- */
        // Temporary Constructor: the Carrier should only be created by the Admin Class -> Implement interface AdminRoles:CreateCarrier
        /// <summary>
        /// Constructor for the Carrier company. Requires their name, rates, reefer charge and a dictionary (key: name of the city, value: Depot object)
        /// </summary>
        /// <param name="name"> <b>string</b> - Carrier's comapny name</param>
        /// <param name="rateFTL"> <b>double</b> - Charging rate for the FTL type trips </param>
        /// <param name="rateLTL"> <b>double</b> - Charging rate for the LTL type trips</param>
        /// <param name="charge"> <b>double</b> - Reefer charge of the carrier</param>
        /// <param name="initialDepots"><b>Dictionary<string, Depot></b> - Initial city-depot dictionary of the carrier</param>
        public Carrier(string name, double rateFTL, double rateLTL, double charge, Dictionary<string, Depot> initialDepots)
        {
            Random rng = new Random();            // DEBUG: This is a placeholder for the CarrierId that will
            carrierId = rng.Next(10000, 99000);   // be grabbed from the database baased on the Id of the last order

            //-----------------------PART of the ADMIN ROLE------------------------------------------------//
            carrierName = name;
            FTLRate = rateFTL;
            LTLRate = rateLTL;
            ReeferCharge = charge;

            CityDepots = initialDepots;   // The admin will list the availible cities for that carrier
        }

        /// <summary>
        /// Default Contructor for Carrier Class - Instantiate to get database information
        /// </summary>
        /// 
        public Carrier()
        {
        }

        /* ---------- Methods---------------*/
        /// <summary>
        /// Used to change the Carrier's FTL rate
        /// </summary>
        /// <param name="someCarrier"><b>Carrier</b> - The Carrier that will have its LTL Rate changed</param>
        /// <param name="newFTLRate"><b>double</b> - The new FTL rate </param>
        public void ChangeFTLRate(Carrier someCarrier, double newFTLRate)
        {
            if (newFTLRate > 0.00)
            {
                someCarrier.FTLRate = newFTLRate;
            }
        }

        /// <summary>
        /// Used to change the Carrier's LTL rate
        /// </summary>
        /// <param name="someCarrier"><b>Carrier</b> - The Carrier that will have its LTL Rate changed</param>
        /// <param name="newLTLRate"><b>double</b> - The new LTL rate </param>
        public void ChangeLTLRate(Carrier someCarrier, double newLTLRate)
        {
            if (newLTLRate > 0.00)
            {
                someCarrier.LTLRate = newLTLRate;
            }
        }


    }
}
