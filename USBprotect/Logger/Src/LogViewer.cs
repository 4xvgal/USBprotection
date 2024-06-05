using System;
using System.IO;

namespace USBprotect.Log
{
    public class LogViewer
    {
        private readonly string _filePath;

        public LogViewer(string filePath)
        {
            _filePath = filePath;
        }

        public string[] ViewLogs()
        {
            if (File.Exists(_filePath))
            {
                // 로그 파일의 모든 줄을 읽어 배열로 반환합니다.
                return File.ReadAllLines(_filePath);
            }
            else
            {
                Console.WriteLine("로그 파일이 존재하지 않습니다.");
                return new string[] {};  // 로그 파일이 없을 경우 빈 배열을 반환
            }
        }
    }
}