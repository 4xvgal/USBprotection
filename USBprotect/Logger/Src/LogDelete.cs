using System;
using System.IO;

namespace USBprotect.Log
{
    public class LogDeleter
    {
        private readonly string _filePath;

        public LogDeleter(string filePath)
        {
            _filePath = filePath;
        }

        public void DeleteLogs()
        {
            if (File.Exists(_filePath))
            {
                File.Delete(_filePath);
                Console.WriteLine("로그 파일이 삭제되었습니다.");
            }
            else
            {
                Console.WriteLine("삭제할 로그 파일이 존재하지 않습니다."); // 파일이 없어 로그 미 삭제
            }
        }
    }
}