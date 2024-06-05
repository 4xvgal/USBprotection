using System;
using System.Collections.Generic;
using System.IO;

namespace USBprotect.Log
{
    public class LogSearcher
    {
        private readonly string _filePath;

        public LogSearcher(string filePath)
        {
            _filePath = filePath;
        }

        // 주어진 검색어를 포함하는 로그 라인들을 반환하는 메서드
        public string[] SearchLogs(string searchTerm)
        {
            if (File.Exists(_filePath))
            {
                // 파일에서 모든 라인을 읽어들인다.
                string[] allLines = File.ReadAllLines(_filePath);
                // 검색어를 포함하는 라인만 필터링
                List<string> filteredLines = new List<string>();
                foreach (string line in allLines)
                {
                    if (line.Contains(searchTerm))
                    {
                        filteredLines.Add(line);
                    }
                }
                return filteredLines.ToArray();
            }
            else
            {
                Console.WriteLine("로그 파일이 존재하지 않습니다.");
                return new string[] { };  // 로그 파일이 없을 경우 빈 배열을 반환
            }
        }
    }
}
