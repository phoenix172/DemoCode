using System.Collections.Generic;

namespace SoftwareTestingDemo.CalculationLib.Tests.Unit
{
    internal class MockLogger : ILogger
    {
        public List<string> LogEntries { get; } = new List<string>();   
            
        public bool Log(string content)
        {
            LogEntries.Add(content);
            return true;
        }
    }
}