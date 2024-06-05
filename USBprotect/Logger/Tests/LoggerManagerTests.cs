using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using NUnit.Framework.Internal;
using NUnit.Framework.Legacy;

namespace USBprotect.Log.test
{
    [TestFixture]
    public class LogManagerTests
    {
        private string _filePath = "testlog.txt";
        private Logger _logger;
        private LogViewer _viewer;
        private LogDeleter _deleter;
        private LogRetentionManager _retentionManager;
        private TimeSpan _retentionPeriod = TimeSpan.FromDays(7);

        [SetUp]
        public void Setup()
        {
            // 테스트를 위해 새로운 로그 파일 생성
            _logger = new Logger(_filePath);
            _viewer = new LogViewer(_filePath);
            _deleter = new LogDeleter(_filePath);
            _retentionManager = new LogRetentionManager(_filePath, _retentionPeriod);
        }

        [Test]
        public void Logger_CanLogMessages()
        {
            _logger.LogMessage("Test log message");
            var content = File.ReadAllText(_filePath);
            ClassicAssert.IsTrue(content.Contains("Test log message"));
        }

        [Test]
        public void LogViewer_CanReadLogs()
        {
            _logger.LogMessage("Another test log message");
            var logs = _viewer.ViewLogs();
            ClassicAssert.IsTrue(logs.Any(log => log.Contains("Another test log message")));
        }

        [Test]
        public void LogDeleter_CanDeleteLogs()
        {
            _logger.LogMessage("Delete this log message");
            _deleter.DeleteLogs();
            ClassicAssert.IsFalse(File.Exists(_filePath));
        }

        [Test]
        public void LogRetentionManager_CanEnforceRetention()
        {
            // 로그를 직접 파일에 쓰기 (테스트를 위한)
            File.WriteAllLines(_filePath, new string[] {
                $"{DateTime.Now.AddDays(-10).ToString("yyyy-MM-dd HH:mm:ss")}: Old log message",
                $"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}: New log message"
            });

            _retentionManager.EnforceRetention();

            var logs = File.ReadAllLines(_filePath);
            Assert.That(logs.Length, Is.EqualTo(1));  // 한 개의 로그만 남아 있어야 합니다.
            Assert.That(logs[0], Does.Contain("New log message"));  // "New log message"가 포함되어 있어야 합니다.
        }


        [TearDown]
        public void TearDown()
        {
            if (File.Exists(_filePath))
                File.Delete(_filePath);  // 테스트 종료 후 파일 정리
        }
    }
}

