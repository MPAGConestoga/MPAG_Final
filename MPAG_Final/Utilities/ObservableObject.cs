using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MPAG_Final.Utilities
{
    /**
   * \Class ObservableObject
   * \Brief Base class for MVVM view models
   * \Details Interface allowing update messages to be sent to the view
   * files for their respective view models. Inherits from the INotifyPropertyChanged
   * interface.
   */
    public class ObservableObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises an event to alert client to changed property value
        /// </summary>
        /// <param name="property"></param> <b>string</b> - name of property which has been changed
        // referenced from https://www.youtube.com/watch?v=CQYvjlDoJ08&t=21s
        protected virtual void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        /// <summary>
        /// Raises an event to alert client to changed property value. Compares new value to old
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="backing"></param> <b>ref T backingField</b> - field to change
        /// <param name="value"></param> <b>T value</b> - new value to apply to changed field
        /// <param name="propertyName"></param> <b>string</b> - name of property which has been changed
        /// <returns>bool true - if values </returns>
        // referenced from https://www.youtube.com/watch?v=CQYvjlDoJ08&t=21s
        protected virtual bool OnPropertyChanged<T>(ref T backing, T value, [CallerMemberName] string propertyName = "")
        {
            if (EqualityComparer<T>.Default.Equals(backing, value))
                return false;

            backing = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
