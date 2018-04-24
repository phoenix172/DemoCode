using System;
using System.Windows.Input;
using SoftwareTestingDemo.CalculationLib;

namespace SoftwareTestingDemo.Calculator
{
    public class CalculateCommand : ICommand
    {
        private ICalculatorViewModel _calculatorViewModel;

        public CalculateCommand(ICalculatorViewModel calculatorViewModel, ICalculator calculator)
        {
            _calculatorViewModel = calculatorViewModel 
                ?? throw new ArgumentNullException(nameof(calculatorViewModel));
            Calculator = calculator
                ?? throw new ArgumentNullException(nameof(calculator));
        }

        public ICalculator Calculator { get; set; }

        public bool CanExecute(object parameter)
        {
            return _calculatorViewModel.Operand1.HasValue &&
                   _calculatorViewModel.Operand2.HasValue;
        }

        public void Execute(object parameter)
        {
            try
            {
                var operation = (MathOperation)parameter;
                int result = Calculator.Calculate(
                    _calculatorViewModel.Operand1.Value, 
                    _calculatorViewModel.Operand2.Value,
                    operation);
                OnCalculated(result);
            }
            catch (Exception ex)
            {
                OnError(ex);
            }
        }

        private void OnCalculated(int result)
        {
            Calculated?.Invoke(this, result);
        }

        private void OnError(Exception exception)
        {
            Error?.Invoke(this,exception);    
        }
        
        public event EventHandler CanExecuteChanged {
            add {
                CommandManager.RequerySuggested += value;
            }
            remove {
                CommandManager.RequerySuggested -= value;
            }
        }
        public event EventHandler<int> Calculated;
        public event EventHandler<Exception> Error;
    }
}