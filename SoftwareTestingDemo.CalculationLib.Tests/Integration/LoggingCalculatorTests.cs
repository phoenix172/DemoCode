using System.IO;
using System.Linq;
using NSubstitute;
using NUnit.Framework;
using SoftwareTestingDemo.CalculationLib.Tests.Unit;

namespace SoftwareTestingDemo.CalculationLib.Tests.Integration
{
    [TestFixture]
    public class LoggingCalculatorTests
    {
        private ICalculator _calculator;
        private FileLogger _logger;

        private string LastLogEntry => File.ReadAllLines(_logger.LogFilePath).Last();

        [SetUp]
        public void SetUp()
        {
            _logger = new FileLogger();
            _calculator = new LoggingCalculator(_logger);
        }

        [TestCase(5,5,MathOperation.Add, "Add(5,5)=10")]
        [TestCase(5,5,MathOperation.Subtract, "Subtract(5,5)=0")]
        [TestCase(5,5,MathOperation.Multiply, "Multiply(5,5)=25")]
        public void Calculate_LogsCorrectEntry(int operand1, int operand2, MathOperation operation,
            string expectedLogEntry)
        {
            _calculator.Calculate(operand1, operand2, operation);
            Assert.That(LastLogEntry, Is.EqualTo(expectedLogEntry));
        }
    }
}