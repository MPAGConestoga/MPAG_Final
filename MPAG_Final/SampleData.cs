using MPAG_Final.SharedModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPAG_Final
{
    public class SampleData
    {

    //    // Orders
    //    // valid cities "Toronto", "Waterloo", "Windsor", "London" 
    //    public List<Order> SampleLTLOrders()
    //    {
    //        var sampleData = new List<Order>()
    //        {
    //            new Order(true, 10, "Toronto", "Waterloo", true),
    //            new Order(true, 200, "Toronto", "Windsor", true),
    //            new Order(true, 33, "Toronto", "Waterloo", true),
    //            new Order(true, 26, "Toronto", "London", false),
    //            new Order(true, 52, "Windsor", "London", false),
    //            new Order(true, 5, "Waterloo", "Windsor", false),
    //            new Order(true, 20, "Toronto", "Waterloo", false),

    //        };

    //        return sampleData;
    //    }

    //    public List<Order> SampleFTLOrders()
    //    {
    //        var sampleData = new List<Order>()
    //        {
    //            new Order(false, 0,  "Windsor", "London", true),
    //            new Order(false, 0,  "Waterloo", "Toronto", true),
    //            new Order(false, 0,  "Toronto", "London", true),
    //            new Order(false, 0,  "Windsor", "London", false),
    //            new Order(false, 0,  "Waterloo", "Windsor", false),
    //            new Order(false, 0,  "Toronto", "London", true),
    //        };

    //        return sampleData;
    //    }

    //    public List<Order> FilterLTLs(int city, bool vanType)
    //    {
    //        var list = SampleLTLOrders();
    //        var filteredList = new List<Order>();

    //        foreach(var order in list)
    //        {
    //            if(order.vanType == vanType && order.origin == city)
    //            {
    //                filteredList.Add(order);
    //            }
    //        }

    //        return filteredList;
    //    }

    //    public List<Carrier> GetRelevantCarrier(string origin, string destination)
    //    {
    //        //--------------------------STUBBED OUT----------------------------//
    //        Dictionary<string, Depot> depotCarrier1 = new Dictionary<string, Depot>
    //        {
    //                {"Toronto",  new Depot("Toronto", 10, 500)},
    //                {"Waterloo", new Depot("Windsor", 5, 200)},
    //                {"Quebec",   new Depot("London", 20, 300) },
    //                {"Ottawa",   new Depot("Waterloo", 60, 400) }
    //        };

    //        // CARRIERS
    //        Carrier carrier1 = new Carrier("Deliver", 0.5, 0.5, 23, depotCarrier1);
    //        Carrier carrier2 = new Carrier("Try", 0.5, 0.5, 23, depotCarrier1);
    //        Carrier carrier3 = new Carrier("WeedELIVER", 0.5, 0.5, 23, depotCarrier1);
    //        Carrier carrier4 = new Carrier("OutOfIdeas", 0.5, 0.5, 23, depotCarrier1);
    //        Carrier carrier5 = new Carrier("Why", 0.5, 0.5, 23, depotCarrier1);

    //        List<Carrier> allCarriers = new List<Carrier>
    //        {
    //            carrier1, carrier2, carrier3, carrier4, carrier5
    //        };

    //        List<Carrier> returnCarriers = new List<Carrier>();

    //        foreach (Carrier carrier in allCarriers)
    //        {
    //            if (carrier.DepotsLocation.ContainsKey(origin) && carrier.DepotsLocation.ContainsKey(origin))
    //            {
    //                returnCarriers.Add(carrier);
    //                carrier.TargetDepot = carrier.DepotsLocation[origin];
    //            }
    //        }

    //        return returnCarriers;
    //    }

        // Orders
        // valid cities "Toronto", "Waterloo", "Windsor", "London" 
        //public List<Order> SampleLTLOrders()
        //{
        //    var sampleData = new List<Order>()
        //    {
        //        new Order(true, 10, "Toronto", "Waterloo", true),
        //        new Order(true, 200, "Toronto", "Windsor", true),
        //        new Order(true, 33, "Toronto", "Waterloo", true),
        //        new Order(true, 26, "Toronto", "London", false),
        //        new Order(true, 52, "Windsor", "London", false),
        //        new Order(true, 5, "Waterloo", "Windsor", false),
        //        new Order(true, 20, "Toronto", "Waterloo", false),

        //    };

        //    return sampleData;
        //}

        //public List<Order> SampleFTLOrders()
        //{
        //    var sampleData = new List<Order>()
        //    {
        //        new Order(false, 0,  "Windsor", "London", true),
        //        new Order(false, 0,  "Waterloo", "Toronto", true),
        //        new Order(false, 0,  "Toronto", "London", true),
        //        new Order(false, 0,  "Windsor", "London", false),
        //        new Order(false, 0,  "Waterloo", "Windsor", false),
        //        new Order(false, 0,  "Toronto", "London", true),
        //    };

        //    return sampleData;
        //}

        //public List<Order> FilterLTLs(string city, bool vanType)
        //{
        //    var list = SampleLTLOrders();
        //    var filteredList = new List<Order>();

        //    foreach(var order in list)
        //    {
        //        if(order.vanType == vanType && order.origin == city)
        //        {
        //            filteredList.Add(order);
        //        }
        //    }

        //    return filteredList;
        //}

        //public List<Carrier> GetRelevantCarrier(string origin, string destination)
        //{
        //    //--------------------------STUBBED OUT----------------------------//
        //    Dictionary<string, Depot> depotCarrier1 = new Dictionary<string, Depot>
        //    {
        //            {"Toronto",  new Depot("Toronto", 10, 500)},
        //            {"Waterloo", new Depot("Windsor", 5, 200)},
        //            {"Quebec",   new Depot("London", 20, 300) },
        //            {"Ottawa",   new Depot("Waterloo", 60, 400) }
        //    };

        //    // CARRIERS
        //    Carrier carrier1 = new Carrier("Deliver", 0.5, 0.5, 23, depotCarrier1);
        //    Carrier carrier2 = new Carrier("Try", 0.5, 0.5, 23, depotCarrier1);
        //    Carrier carrier3 = new Carrier("WeedELIVER", 0.5, 0.5, 23, depotCarrier1);
        //    Carrier carrier4 = new Carrier("OutOfIdeas", 0.5, 0.5, 23, depotCarrier1);
        //    Carrier carrier5 = new Carrier("Why", 0.5, 0.5, 23, depotCarrier1);

        //    List<Carrier> allCarriers = new List<Carrier>
        //    {
        //        carrier1, carrier2, carrier3, carrier4, carrier5
        //    };

        //    List<Carrier> returnCarriers = new List<Carrier>();

        //    foreach (Carrier carrier in allCarriers)
        //    {
        //        if (carrier.DepotsLocation.ContainsKey(origin) && carrier.DepotsLocation.ContainsKey(origin))
        //        {
        //            returnCarriers.Add(carrier);
        //            carrier.TargetDepot = carrier.DepotsLocation[origin];
        //        }
        //    }

        //    return returnCarriers;
        //}


    }
}
