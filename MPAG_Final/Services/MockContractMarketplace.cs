using MPAG_Final.SharedModels;
using MPAG_Final.SharedViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPAG_Final.Services
{
    public class MockContractMarketplace : IContractDataService
    {
        private IEnumerable<Contract> _contracts;

        public MockContractMarketplace()
        {
            _contracts = new List<Contract>()
            {
                new Contract
                {
                    OrderID = 00,
                    Customer = "Jev's Robotics",
                    JobType = JobType.LTL,
                    Origin = City.Hamilton,
                    Destination = City.London,
                    VanType = VanType.Dry
                },
                new Contract
                {
                    OrderID = 00,
                    Customer = "Tim's Ice Makers",
                    JobType = JobType.FTL,
                    Origin = City.Kingston,
                    Destination = City.Oshawa,
                    VanType = VanType.Reefer
                },
                new Contract
                {
                    OrderID = 00,
                    Customer = "Cats",
                    JobType = JobType.FTL,
                    Origin = City.Toronto,
                    Destination = City.Ottawa,
                    VanType = VanType.Dry
                }
            };
        }

        public IEnumerable<Contract> GetContracts()
        {
            return _contracts;
        }

        public void Save(IEnumerable<Contract> contracts)
        {
            _contracts = contracts;
        }

    }
}
