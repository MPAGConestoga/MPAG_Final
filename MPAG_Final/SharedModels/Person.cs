using MPAG_Final.Logging;
using MPAG_Final.Utilities;
using System;
using System.Net.Mail;

namespace MPAG_Final.SharedModels
{
    /** 
     * \Class Person
     * \brief The Person class used to hold identifying information about an individual.
     * \details The Person class is used for holding information about a customer or employee. The Customer
     * and Employee class both inherit from this class. 
     * \see Employee, Customer
     *
     */
    public class Person
    {
        //--------- Attributes ---------//
        public int personID { get; set; }           // Key
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string phoneNum { get; set; }
        public string email { get; set; }
        public Address personAddress { get; set; }


        /// <summary>
        /// This constructor is used to instantiate a Person with the specified field values
        /// </summary>
        /// <param name="firstName"> <b>string</b> - Customer first name</param>
        /// <param name="lastName"> <b>string</b> - Customer last name</param>
        /// <param name="email"> <b>string</b> - Customer email</param>
        /// <param name="phoneNum"> <b>string</b> - Customer phone number</param>
        /// <param name="streetAddress"> <b>string</b> - Customer street address</param>
        /// <param name="city"> <b>string</b> - Customer's city</param>
        /// <param name="province"> <b>string</b> - Customer's province</param>
        /// <param name="postalCode"> <b>string</b> - Customer's postal code</param>
        public Person(string firstName, string lastName, string email, string phoneNum,
            string streetAddress, string city, string province, string postalCode)
        {
            this.personID = 1;
            this.firstName = firstName;
            this.lastName = lastName;
            Address temp = new Address(streetAddress, city, province, postalCode);
            personAddress = temp;
            this.phoneNum = phoneNum;

            if (ValidateEmail(email))
            {
                this.email = email;
            }
        }

        /// <summary>
        /// The default constructor for the person class
        /// </summary>
        public Person()
        {
        }

        /// <summary>
        /// This method is used to validate that an email is in the correct format
        /// </summary>
        /// <param name="email"> <b>string</b> - Customer email</param>
        /// \see Person::ChangeEmail()
        private static bool ValidateEmail(string email)
        {
            // Reference: https://stackoverflow.com/questions/5342375/regex-email-validation

            try
            {
                MailAddress verify = new MailAddress(email);
                return true;
            }
            catch (FormatException)
            {
                LogType.ErrorType(LogType.LoggingType.buyer, "Email not entered");
                return false;
            }
        }

        /// <summary>
        /// This method is used to change the email of a customer
        /// </summary>
        /// <param name="email"> <b>string</b> - Customer email</param>
        /// \see Person::ValidateEmail()
        public void ChangeEmail(string newEmail)
        {
            if (ValidateEmail(newEmail))
            {
                email = newEmail;
            }
        }

        /// <summary>
        /// This method is used to submit the person information to the database.
        /// The Data access layer class is instantiated and used to submit the data.
        /// </summary>
        ///
        /// \see TMSDAL::GetCustomerInformation()
        public void GetPersonInfo()
        {
            new TMSDAL().GetCustomerInformation(this);
        }
    }
}
