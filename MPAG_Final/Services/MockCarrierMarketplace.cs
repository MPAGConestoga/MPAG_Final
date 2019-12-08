using MPAG_Final.SharedModels;
using MPAG_Final.SharedViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPAG_Final.Services
{
    public class MockCarrierMarketplace : ICarrierDataService
    {
        private IEnumerable<Carrier> _carriers;

        public MockCarrierMarketplace()
        {
            _carriers = new List<Carrier>()
            {
                new Carrier
                {
                    CarrierID = 01,
                    CarrierName = "Ted's Truck",
                    AvailableTrucks = 12
                },
                new Carrier
                {
                    CarrierID = 02,
                    CarrierName = "UPS",
                    AvailableTrucks = 2
                },
                new Carrier
                {
                    CarrierID = 03,
                    CarrierName = "Alan's Truck",
                    AvailableTrucks = 33
                }
            };
        }

        public IEnumerable<Carrier> GetCarriers()
        {
            return _carriers;
        }

        public void Save(IEnumerable<Carrier> carriers)
        {
            _carriers = carriers;
        }
    }
}
