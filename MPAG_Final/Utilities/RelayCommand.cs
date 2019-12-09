using MPAG_Final.Planner.ViewModels;
using MPAG_Final.SharedViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MPAG_Final.Utilities
{
    /**
  * \Class RelayCommand
  * \Brief Base class for MVVM command binding
  * \Details Interface allowing commands to be bound to buttons. Inherits from the
  * ICommand interface. Instances command objects through which the view can call
  * methods.
  */
    public class RelayCommand<T> : ICommand
    {
        private readonly Action<T> _execute = null;
        private readonly Func<T, bool> _canExecute = null;

        /// <summary>
        /// N/A
        /// </summary>
        /// <param name="execute"></param> <b>Action</b> - N/A
        /// <param name="canExecute"></param> <b>Func</b> - N/A
        public RelayCommand(Action<T> execute, Func<T, bool> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute ?? (_ => true);
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public bool CanExecute(object parameter) => _canExecute((T)parameter);

        public void Execute(object parameter) => _execute((T)parameter);
    }

    /**
    * \Class RelayCommands
    * \Brief Base class for MVVM command binding
    * \Details Interface allowing commands to be bound to buttons. Inherits from the
    * ICommand interface. Instances command objects through which the view can call
    * methods.
    */
    public class RelayCommand : RelayCommand<object>
    {
        /// <summary>
        /// N/A
        /// </summary>
        /// <param name="execute"></param> <b>Action</b> - N/A
        public RelayCommand(Action execute)
            : base(_ => execute()) { }

        /// <summary>
        /// N/A
        /// </summary>
        /// <param name="execute"></param> <b>Action</b> - N/A
        /// <param name="canExecute"></param> <b>Func</b> - N/A
        public RelayCommand(Action execute, Func<bool> canExecute)
            : base(_ => execute(), _ => canExecute()) { }
    }

    public class AddContract : ICommand
    {

        public AddContract(ContractsViewModel viewModel)
        {
            //get the view model associated with the command
            _ViewModel = viewModel;
        }

        private ContractsViewModel _ViewModel;

        public event System.EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _ViewModel.SubmitContract(parameter);

        }
    }

    /**
    * \Class SelectOrder
    * \Brief Select the type of order from the view model event handler
    * \Details Allow the order to be selected from the view model and controlled
    *           based on the selection of the order
    */

    public class SelectOrder : ICommand
    {
        public SelectOrder(OrderViewModel viewModel)
        {
            //get the view model associated with the command
            _ViewModel = viewModel;
        }

        private OrderViewModel _ViewModel;

        public event System.EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _ViewModel.OrderChecked(parameter);

        }
    }

    /**
    * \Class RemoveOrder
    * \Brief Base class for MVVM command binding
    * \Details Interface allowing commands to be bound to buttons. Inherits from the
    * ICommand interface. Instances command objects through which the view can call
    * methods.
    */
    public class RemoveOrder : ICommand
    {
        public RemoveOrder(OrderViewModel viewModel)
        {
            //get the view model associated with the command
            _ViewModel = viewModel;
        }

        private OrderViewModel _ViewModel;

        public event System.EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _ViewModel.RemoveOrder(parameter);

        }
    }

    public class SelectFTLOrder : ICommand
    {

        public SelectFTLOrder(OrderViewModel viewModel)
        {
            //get the view model associated with the command
            _ViewModel = viewModel;
        }

        private OrderViewModel _ViewModel;

        public event System.EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _ViewModel.FTLOrderCheck(parameter);

        }
    }
    public class ProcessInvoice : ICommand
    {

        public ProcessInvoice(ContractsViewModel viewModel)
        {
            //get the view model associated with the command
            _ViewModel = viewModel;
        }

        private ContractsViewModel _ViewModel;

        public event System.EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _ViewModel.SubmitContract(parameter);

        }
    }
}
