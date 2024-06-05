using System;
using System.IO;

namespace USBprotect.Log
{
    public class Logger
    {
        private readonly string _filePath;

        public Logger(string filePath)
        {
            _filePath = filePath;
        }

        public void LogMessage(string message)
        {
            string timeStamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string logEntry = $"{timeStamp}: {message}";
            using (StreamWriter writer = new StreamWriter(_filePath, true))
            {
                writer.WriteLine(logEntry);
            }
        }
    }
}