using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// class information :: 
// 프로그램 구현에 필요한 핵심 DEVCON 명령어들이 C# 에서 작동할 수 있도록 하는 함수형 명령코드의 집합입니다.
// 필요한 명령어는 DevconCommand 의 매개변수로 제공해서 실행 함수를 따로 빼서 쓰시면 됩니다.

namespace UsbSecurity
{
    class DevconClass
    {
        private string usbDeviceId; // USB 장치 
        string devconPath = @"C:\Program Files (x86)\Windows Kits\10\Tools\10.0.22621.0\x64\devcon.exe"; // !! devcon 모듈의 경로에 대한 수정 요구됨 

        private string DevconCommand(string command) // Devcon 명령어를 실행하는 메서드 , 매겨변수로 devcon 명령어를 받습니다. 
        {
            ProcessStartInfo psi = new ProcessStartInfo() // 프로세스 시작 정보
            {
                FileName = devconPath,         // Devcon 경로
                Arguments = command,           // 명령어
                UseShellExecute = false,       // 셸 실행 사용 안함
                RedirectStandardOutput = true, // 표준 출력 리다이렉트
                CreateNoWindow = true    // 창 생성 안함
            };

            using (Process process = Process.Start(psi)) // 프로세스 시작
            {
                using (StreamReader reader = process.StandardOutput) // 프로세스 읽어옴...
                {
                    string result = reader.ReadToEnd(); 
                    return result;
                }
            }
        }
            
        // --------------------------------------------- 요 아래에 함수 형태로 devcon 명령어 구현 --------------------------------------------------------- //

        internal void DisableDevice() // 차단하기
        {
            DevconCommand("disable" + usbDeviceId);
        }

        internal void EnableDevice() // 허용하기 
        {
           DevconCommand("enable" + usbDeviceId);
        }
        
    }
}
