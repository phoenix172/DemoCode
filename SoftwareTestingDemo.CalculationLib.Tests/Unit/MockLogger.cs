using System.Collections.Generic;

namespace SoftwareTestingDemo.CalculationLib.Tests.Integration
{
    internal class MockLogger : ILogger
    {
        public int CallsCount { get; private set; } = 0;
            
        public bool Log(string content)
        {
            CallsCount++;
            return true;
        }
    }
}