using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using NSubstitute.Routing.Handlers;
using NUnit.Framework;
using SoftwareTestingDemo.CalculationLib.Tests.Integration;

namespace SoftwareTestingDemo.CalculationLib.Tests.Unit
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
            FakeLogger fakeLogger = MakeFakeLogger();
            ICalculator calculator = MakeCalculator(fakeLogger);
            calculator.Calculate(operand1, operand2, operation);
            Assert.That(fakeLogger.LogEntries.Last(), Is.EqualTo(expectedLogEntry));
        }
        
        [Test]
        public void Calculate_CallsLog_EveryTime()
        {
            MockLogger mockLogger = MakeMockLogger();
            ICalculator calculator = MakeCalculator(mockLogger); 
            
            calculator.Calculate(5, 5, MathOperation.Add);
            calculator.Calculate(5, 5, MathOperation.Subtract);
            calculator.Calculate(5, 5, MathOperation.Multiply);
            
            Assert.That(mockLogger.CallsCount, Is.EqualTo(3));
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
        
        private MockLogger MakeMockLogger()=> new MockLogger();
        private StubLogger MakeStubLogger() => new StubLogger();
        private FakeLogger MakeFakeLogger() => new FakeLogger();
        
        private ICalculator MakeCalculator(ILogger logger) => new LoggingCalculator(logger);
    }
}