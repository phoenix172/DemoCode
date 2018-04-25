using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Data;
using SoftwareTestingDemo.CalculationLib;
using SoftwareTestingDemo.Calculator.Properties;

namespace SoftwareTestingDemo.Calculator
{
    public class CalculatorViewModel : INotifyPropertyChanged, ICalculatorViewModel
    {
        private int? _operand1, _operand2;
        private string _result;
        private string _errorMessage;

        public CalculatorViewModel()
        {
            InitCalculators();
            InitCalculateCommand();
        }

        private void InitCalculateCommand()
        {
            CalculateCommand = new CalculateCommand(this, CalculatorImplementation);
            CalculateCommand.Calculated += (s, e) =>
            {
                Result = e.ToString();
                ErrorMessage = string.Empty;
            };
            CalculateCommand.Error += (s, e) =>
            {
                ErrorMessage = e is DivideByZeroException 
                    ? "Cannot divide by zero" : e.ToString();
            };
        }

        private void InitCalculators()
        {
            CalculatorImplementationsView =
                new CollectionView(new ICalculator[]
                {
                    new BasicCalculator(),
                    new LoggingCalculator(new FileLogger())
                });
            CalculatorImplementationsView.MoveCurrentToFirst();
            CalculatorImplementationsView.CurrentChanged += (s, e) =>
                CalculateCommand.Calculator = CalculatorImplementation;
        }

        public int? Operand1
        {
            get => _operand1;
            set
            {
                if (value == _operand1) return;
                _operand1 = value;
                OnPropertyChanged();
            }
        }

        public int? Operand2
        {
            get => _operand2;
            set
            {
                if (value == _operand2) return;
                _operand2 = value;
                OnPropertyChanged();
            }
        }

        public string Result
        {
            get => _result;
            private set
            {
                if (value == _result) return;
                _result = value;
                OnPropertyChanged();
            }
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            private set
            {
                if (value == _errorMessage) return;
                _errorMessage = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(HasError));
            }
        }


        public bool HasError => !string.IsNullOrEmpty(ErrorMessage);
        public CalculateCommand CalculateCommand { get; set; }
        public ICollectionView CalculatorImplementationsView { get; set; }
        private ICalculator CalculatorImplementation => CalculatorImplementationsView.CurrentItem as ICalculator;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}