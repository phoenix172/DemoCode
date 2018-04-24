using System.IO;
using System.Net.Mime;

namespace SoftwareTestingDemo.CalculationLib
{
    public class FileLogger : ILogger
    {
        private const string _logFileName = "calcLog.txt";

        public FileLogger()
        {
            LogFilePath = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, _logFileName);
            File.WriteAllText(LogFilePath, "");
        }

        public string LogFilePath { get; }

        public bool Log(string content)
        {
            File.AppendAllLines(LogFilePath, new[] {content});
            return true;
        }
    }
}