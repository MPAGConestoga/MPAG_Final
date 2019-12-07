using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPAG_Final.SharedModels
{
    /**
    * \brief   The Employee class represents an OSHT employee.
    * \details The Employee class contains contact information about the OSHT employee. It encapsulates role, name, email, phone number and address
    */
    public class Employee : Person
    {
        //--------- Attributes ---------//
        private int employeeID;         // Key
        //private string password;
        private int roleID;

        private static Dictionary<int, string> validRoles = new Dictionary<int, string>()
        {
            { 0, "Buyer" },
            { 1, "Planner" },
            { 2, "Admin"}
        };

        /// <summary>
        /// Constructor for the Employee. It requires their role name ,first, last name, email, phone number and address
        /// </summary>
        /// <param name="role"> <b>string</b> - Name of the role </param>
        /// <param name="firstName"> <b>string</b> - Employee's first name</param>
        /// <param name="lastName"> <b>string</b> - Employee's last name</param>
        /// <param name="email"> <b>string</b> - Employee's email</param>
        /// <param name="phoneNum"> <b>string</b> - Employee's phone number</param>
        /// <param name="streetAddress"> <b>string</b> - Part of the Address object that will be generated</param>
        /// <param name="city"> <b>string</b> - Part of the Address object that will be generated</param>
        /// <param name="province"> <b>string</b> - Part of the Address object that will be generated</param>
        /// <param name="postalCode"> <b>string</b> - Part of the Address object that will be generated</param>
        public Employee
            (string role, string firstName, string lastName, string email, string phoneNum,
            string streetAddress, string city, string province, string postalCode) :
            base(firstName, lastName, email, phoneNum, streetAddress, city, province, postalCode)
        {
            int possibleRole = ValidateRole(role);
            if (possibleRole != -1)
            {
                this.roleID = possibleRole;
            }
        }

        /// <summary>
        /// Validates the role to make sure it is one of the OSHT employee types
        /// </summary>
        /// <param name="newRoleID"> <b>string</b> - Name of the role that is being validated </param>
        /// <returns></returns>
        private static int ValidateRole(string newRoleID)
        {
            int isValid = -1;

            foreach (var role in validRoles)
            {
                if (newRoleID == role.Value)
                {
                    isValid = role.Key;
                    break;
                }
            }

            return isValid;
        }
    }
}
