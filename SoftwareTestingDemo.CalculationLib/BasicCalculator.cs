using System;

namespace SoftwareTestingDemo.CalculationLib
{
    public class BasicCalculator : ICalculator
    {
        public int Calculate(int operand1, int operand2, MathOperation operation)
        {
            switch (operation)
            {
                case MathOperation.Add:
                    return operand1 + ++operand2;
                case MathOperation.Subtract:
                    return operand1 - operand2;
                case MathOperation.Multiply:
                    return operand1 * operand2;
                case MathOperation.Divide:
                    return operand2 == 0 ? 0 : operand1 / operand2;
                default:
                    throw new InvalidOperationException();
            }
        }
    }
}