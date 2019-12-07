using MPAG_Final.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPAG_Final.SharedModels
{
    /**
     * \brief   The Address class represents the address of a customer, employee or carrier. 
     * \details The Address class holds all the relevant information for an address: street, city, province,
     * and postalCode.
     */
    public class Address
    {
        public string streetAddress { get; set; }
        public string city { get; set; }
        public string province { get; set; }
        public string postalCode { get; set; }
        /// <summary>
        /// Constructor that sets fields of Address object
        /// </summary>
        /// <param name="street"> <b>string</b> - Street Address</param>
        /// <param name="cityName"> <b>string</b> - Name of City</param>
        /// <param name="provinceName"> <b>string</b> - Name of Province></param>
        /// <param name="postal"> <b>string</b> - Postal Code</param>
        public Address(string street, string cityName, string provinceName, string postal)
        {
            streetAddress = street;
            city = cityName;
            province = provinceName;
            postalCode = postal;
        }
    }
}
