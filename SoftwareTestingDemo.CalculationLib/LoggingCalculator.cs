using System;

namespace SoftwareTestingDemo.CalculationLib
{
    public class LoggingCalculator : ICalculator
    {
        private readonly ICalculator _basicCalculator;
        private readonly ILogger _logger;

        public LoggingCalculator(ILogger logger)
        {
            _basicCalculator = new BasicCalculator();
            _logger = logger;
        }
        
        public int Calculate(int operand1, int operand2, MathOperation operation)
        {
            int result = _basicCalculator.Calculate(operand1, operand2, operation);
            _logger.Log($"{operation.ToString()}({operand1},{operand2})={result}");
            return result;
        }
    }
}