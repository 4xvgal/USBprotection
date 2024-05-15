﻿        using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using USBprotect.InternalFunction;

// class information :: 
// 프로그램 구현에 필요한 핵심 DEVCON 명령어들이 C# 에서 작동할 수 있도록 하는 함수형 명령코드의 집합입니다.
// 필요한 명령어는 DevconCommand 의 매개변수로 제공해서 실행 함수를 따로 빼서 쓰시면 됩니다.

namespace UsbSecurity
{
    class DevconCMD
    {
 
        string devconPath = @"C:\Program Files (x86)\Windows Kits\10\Tools\10.0.22621.0\x64\devcon.exe"; // !! devcon 모듈의 경로에 대한 수정 요구됨 

        public string DevconCommand(string command) // Devcon 명령어를 실행하는 메서드 , 매개변수로 devcon 명령어를 받습니다. 
        {
            ProcessStartInfo psi = new ProcessStartInfo() // 프로세스 시작 정보
            {
                FileName = devconPath,         // Devcon 경로
                Arguments = command,           // 명령어
                UseShellExecute = true,       // 셸 실행 사용 안함
                Verb = "runas",               // 관리자 권한으로 실행
                CreateNoWindow = true    // 창 생성 안함
            };

            using (Process process = Process.Start(psi)) // 프로세스 시작
            {
                using (StreamReader reader = process.StandardOutput) // 프로세스 읽어옴...
                {
                    string result = reader.ReadToEnd(); 
                    return result;

                    ///예외처리 추가 필요
                }
            }
        }

        internal bool SetDevconPath(string path) // devcon 경로 설정
        {
            devconPath = path; // 경로 설정
            if (checkDevconExist() == true)
            { // devcon 존재 여부 확인
                return true;
            }
            
            return false;
        }
        private bool checkDevconExist() // devcon 존재 여부 확인
        {
            //devon help 를 실행해 결과가 나오면 devcon 이 존재하는 것으로 판단
            //Device Console Help 을 포함해야함
            string result = DevconCommand("help");
            if (result.Contains("Device Console Help")) // 결과에 Device Console Help 가 포함되어 있으면
            {
                return true; // 존재함
            }
            else
            {
                return false; // 존재하지 않음
            }

        }
    }
}
