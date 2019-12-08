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

    public class AddContractConverter : IMultiValueConverter
    {
        // Method Name: Convert
        // Description: This method is used to check if all the order fields
        //              are entered by the user.
        // Parameters:
        // 
        // Returns: true if all entered, false if not
        //
        public object Convert(object[] values, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            bool retValue = true;
            string job = (values[0]).ToString();
            string van = (values[1]).ToString();
            string origin = (values[2]).ToString();
            string destination = (values[3]).ToString();
            string check = (values[4]).ToString();
            
            ((System.Windows.Controls.TextBlock)values[5]).Text = "";
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

    class BtnParamConverter : IMultiValueConverter
    {
        // Returns a copy of the parameters
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