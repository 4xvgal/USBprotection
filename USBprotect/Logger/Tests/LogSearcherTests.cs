using NUnit.Framework;
using NUnit.Framework.Legacy;
using System.IO;
using USBprotect.Log;

namespace USBprotect.Tests
{
    [TestFixture]
    public class LogSearcherTests
    {
        private string _testFilePath = Path.Combine(TestContext.CurrentContext.TestDirectory, "testLog.txt");
        private LogSearcher _logSearcher;

        [SetUp]
        public void Setup()
        {
            // 테스트 전에 테스트용 로그 파일을 생성하고 초기 데이터를 씁니다.
            File.WriteAllLines(_testFilePath, new string[] {
                "Error: Something went wrong",
                "Warning: Potential issue detected",
                "Info: System started successfully",
                "Error: Failed to load module",
                "Debug: Checking system integrity"
            });
            _logSearcher = new LogSearcher(_testFilePath);
        }

        [TearDown]
        public void TearDown()
        {
            // 테스트 후에 테스트용 파일을 삭제합니다.
            if (File.Exists(_testFilePath))
            {
                File.Delete(_testFilePath);
            }
        }

        [Test]
        public void SearchLogs_WithExistingTerm_ShouldReturnCorrectLines()
        {
            // 테스트: "Error" 검색어로 로그를 검색
            var results = _logSearcher.SearchLogs("Error");

            // 검증: "Error"를 포함하는 라인만 반환되었는지 확인
            ClassicAssert.AreEqual(2, results.Length);
            ClassicAssert.Contains("Error: Something went wrong", results);
            ClassicAssert.Contains("Error: Failed to load module", results);
        }

        [Test]
        public void SearchLogs_WithNonExistingTerm_ShouldReturnEmptyArray()
        {
            // 테스트: "Fatal" 검색어로 로그를 검색
            var results = _logSearcher.SearchLogs("Fatal");

            // 검증: 반환된 배열이 비어있는지 확인
            ClassicAssert.IsEmpty(results);
        }

        [Test]
        public void SearchLogs_WithEmptyTerm_ShouldReturnAllLines()
        {
            // 테스트: 빈 문자열로 로그를 검색
            var results = _logSearcher.SearchLogs("");

            // 검증: 모든 라인이 반환되었는지 확인
            ClassicAssert.AreEqual(5, results.Length);
        }
    }
}
