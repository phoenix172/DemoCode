using System;
using NUnit.Framework;

namespace SoftwareTestingDemo.CalculationLib.Tests.Unit
{
    [TestFixture]
    public class BasicCalculatorTests
    {
        [Test]
        public void Calculate_DivideByZero_ThrowsDivideByZeroException()
        {
            ICalculator calculator = MakeCalculator();
            Assert.That(() => calculator.Calculate(5, 0, MathOperation.Divide),
                Throws.Exception.TypeOf<DivideByZeroException>());
        }
        
        [TestCase(5,5, MathOperation.Add, 10)]
        [TestCase(5,5, MathOperation.Subtract, 0)]
        [TestCase(10,5, MathOperation.Divide, 2)]
        [TestCase(10,5, MathOperation.Multiply, 50)]
        public void Calculate_NumbersAndOperationReturnsCorrectResult_ReturnsCorrectResult
            (int operand1, int operand2, MathOperation operation, int expectedResult)
        {
            ICalculator calculator = MakeCalculator();
            int result = calculator.Calculate(operand1, operand2, operation);
            Assert.That(result, Is.EqualTo(expectedResult));
        }
        
        private ICalculator MakeCalculator() => new BasicCalculator();
    }
}