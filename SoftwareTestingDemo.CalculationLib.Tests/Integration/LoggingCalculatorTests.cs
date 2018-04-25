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
        [TestCase(5,5,MathOperation.Add, "Add(5,5)=10")]
        [TestCase(5,5,MathOperation.Subtract, "Subtract(5,5)=0")]
        [TestCase(5,5,MathOperation.Multiply, "Multiply(5,5)=25")]
        public void Calculate_LogsCorrectEntry(int operand1, int operand2, MathOperation operation,
            string expectedLogEntry)
        {
            MockLogger mockLogger = MakeMockLogger();
            ICalculator calculator = MakeCalculator(mockLogger);
            calculator.Calculate(operand1, operand2, operation);
            Assert.That(mockLogger.LogEntries.Last(), Is.EqualTo(expectedLogEntry));
        }
        
        [Test]
        public void Calculate_CallsLog_EveryTime()
        {
            MockLogger mockLogger = MakeMockLogger();
            ICalculator calculator = MakeCalculator(mockLogger); 
            
            calculator.Calculate(5, 5, MathOperation.Add);
            calculator.Calculate(5, 5, MathOperation.Subtract);
            calculator.Calculate(5, 5, MathOperation.Multiply);
            
            Assert.That(mockLogger.LogEntries.Count, Is.EqualTo(3));
        }

        [TestCase(5, 5, MathOperation.Add, 10)]
        [TestCase(5, 5, MathOperation.Subtract, 0)]
        [TestCase(5, 5, MathOperation.Multiply, 25)]
        public void Calculate_ReturnsCorrectResult(int operand1, int operand2, MathOperation operation,
            int expectedResult)
        {
            StubLogger stubLogger = MakeStubLogger();
            ICalculator calculator = MakeCalculator(stubLogger);

            int result = calculator.Calculate(operand1, operand2, operation);
            
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        private static MockLogger MakeMockLogger()=> new MockLogger();
        private static StubLogger MakeStubLogger() => new StubLogger();
        private static ICalculator MakeCalculator(ILogger logger) => new LoggingCalculator(logger);
    }
}