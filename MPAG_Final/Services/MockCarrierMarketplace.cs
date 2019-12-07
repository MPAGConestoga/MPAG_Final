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
                    carrierId = 01,
                    carrierName = "Ted's Truck"
                },
                new Carrier
                {
                    carrierId = 02,
                    carrierName = "UPS"
                },
                new Carrier
                {
                    carrierId = 03,
                    carrierName = "Alan's Truck"
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
