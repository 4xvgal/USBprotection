using System;
using System.IO;
using System.Linq;

namespace USBprotect.Log
{
    public class LogRetentionManager
    {
        private readonly string _filePath;
        private readonly TimeSpan _retentionPeriod;

        public LogRetentionManager(string filePath, TimeSpan retentionPeriod)
        {
            _filePath = filePath;
            _retentionPeriod = retentionPeriod;
        }

        public void EnforceRetention()
        {
            if (File.Exists(_filePath))
            {
                var lines = File.ReadAllLines(_filePath)
                    .Where(line => DateTime.Now - ParseDateFromLogLine(line) < _retentionPeriod)
                    .ToArray();
                File.WriteAllLines(_filePath, lines);
            }
        }

        private DateTime ParseDateFromLogLine(string logLine)
        {
            // 가정: 로그 라인의 시작 부분이 "yyyy-MM-dd HH:mm:ss" 형식의 날짜임
            return DateTime.Parse(logLine.Substring(0, 19));
        }
    }
}