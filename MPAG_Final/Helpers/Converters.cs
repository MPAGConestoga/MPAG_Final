using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace MPAG_Final.Helpers
{
        /**
     * \Class BoolToVisbilityConverter
     * \brief   Value converter for visibility of items
     * \details The BoolToVisibilityConverter is in charge of setting xaml controls to a visible 
     *          or invisible state
     */

    public class BoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var boolValue = (bool)value;

            if (boolValue)
                return Visibility.Visible;

            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    /**
     * \Class NullToVisibilityConverter
     * \brief Interaction with XAML  
     * \details This control sets the XAML to null if the converter is requested
     */

    //if null, then make visibile
    public class NullToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return Visibility.Visible;

            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    /**
     * \Class AddContractConverter
     * \brief   Save blocker
     * \details This controls if the save button is enabled only if the required values are completely 
     *          filled appropriately
     */
    public class AddContractConverter : IMultiValueConverter
    {
        // Method Name: Convert
        // Description: This method is used to check if all the order fields
        //              are entered by the user.
        // Parameters:
        // 
        // Returns: true if all entered, false if not
        //

        /// <summary>
        ///         Converts to check there is a value input into the buyer page
        /// </summary>
        /// <param name="values"><b>object[]</b> - check if there is a valid value input</param>
        /// <param name="targetType"><b>Type</b> - Data type that is input</param>
        /// <param name="parameter"><b>object</b> - name of the input </param>
        /// <param name="culture"><b>CultureInfo</b> - The region of the input type</param>
        /// <returns></returns>
        public object Convert(object[] values, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            bool retValue = true;
            string job = (values[0]).ToString();
            string van = (values[1]).ToString();
            string origin = (values[2]).ToString();
            string destination = (values[3]).ToString();
            string check = (values[4]).ToString();
            string count = (values[6]).ToString();
            int number;
            bool val = Int32.TryParse(count, out number);
            ((System.Windows.Controls.TextBlock)values[5]).Text = "";

            if (!val)
            {
                ((System.Windows.Controls.TextBlock)values[5]).Text = "The Quantity must be a whole number";
                retValue = false;
            }
            if ((val) && (number < 0))
            {
                ((System.Windows.Controls.TextBlock)values[5]).Text = "The Quantity cannot be a negative number";
                retValue = false;
            }
            if ((val) && (number <= 0) && (job=="LTL"))
            {
                ((System.Windows.Controls.TextBlock)values[5]).Text = "The Quantity cannot be zero for an LTL";
                retValue = false;
            }
            if (origin == destination)
            {
                ((System.Windows.Controls.TextBlock)values[5]).Text = "The Origin and Destination must be different";
                retValue = false;
            }
            if ((job == "NULL") || (van == "NULL") ||(origin == "NULL") || (destination == "NULL") || (check == ""))
            {
                ((System.Windows.Controls.TextBlock)values[5]).Text = "Please Select A Contract";
                retValue = false;
            }
            

            return retValue;
        }

        // Must be defined, but is not implemented
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter,
        System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    /**
     * \Class BtnParamConverter
     * \brief   Save blocker
     * \details This controls if the save button is enabled only if the required values are completely 
     *          filled appropriately
     */
    class BtnParamConverter : IMultiValueConverter
    {
        // Returns a copy of the parameters
        /// <summary>
        ///     Returns a copy of the parameters
        /// </summary>
        /// <param name="values"><b>object[]</b> - Value of the paramater</param>
        /// <param name="targetType"><b>Type</b> - Data type </param>
        /// <param name="parameter"><b>object</b> - Name of the paramater </param>
        /// <param name="culture"><b>CultureInfo</b> - If there is a required region for the language or date of the data type</param>
        /// <returns></returns>
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return values.Clone();
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}