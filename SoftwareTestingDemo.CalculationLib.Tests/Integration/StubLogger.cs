namespace SoftwareTestingDemo.CalculationLib.Tests.Integration
{
    internal class StubLogger : ILogger
    {
        public bool Log(string content)
        {
            return true;
        }
    }
}