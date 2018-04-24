namespace SoftwareTestingDemo.CalculationLib.Tests.Unit
{
    internal class StubLogger : ILogger
    {
        public bool Log(string content)
        {
            return true;
        }
    }
}