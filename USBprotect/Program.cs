﻿using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using USBprotect.src.dataClass;
using USBprotect.src.USB.test;
using UsbSecurity;

namespace USBprotect
{
    internal static class Program
    {
        

        /// <summary>
        /// 해당 애플리케이션의 주 진입점입니다.
        /// </summary>
        /// 

        // 임시로 터미널 띄우는 코드
/*
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();

        [STAThread]
        static void Main(List<string> parsingUsbDevice)
        {
            AllocConsole(); // 콘솔 창 할당

            UsbDeviceWatcher watcher = new UsbDeviceWatcher();
            watcher.Start();

            Console.WriteLine("=========================");
            Console.WriteLine("비인가 USB 접근 차단중...");
            Console.WriteLine("=========================");
            Console.ReadKey(); // 사용자 입력 대기

            watcher.Stop();
            
        }*/
         
        // 원래코드
        
        [STAThread]
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();
        static void Main()
        {
            /* Application.EnableVisualStyles();
             Application.SetCompatibleTextRenderingDefault(false);
             Application.Run(new Form1());*/
            AllocConsole(); // 콘솔 창 할당

            //USB 리스트 테스트
            ParsingUsbDeviceTest.TestGetConnectedUSBdevices();

            //devcon 클래스 테스트
            DevconClass devcon = new DevconClass();
            Console.Write(devcon.SetDevconPath(@"C:\Users\4xvgal\AppData\Roaming\DevCon\devcon.exe"));

            Console.Write("Type Device ID :"); // 장치 ID 입력
            string deviceId = Console.ReadLine(); // 장치 ID 입력
            USBdevice uSBdevice = new USBdevice(); // USB 장치 정보 생성
            uSBdevice.deviceID = deviceId; // 장치 ID 설정
            devcon.setDevice(uSBdevice); // USB 장치 정보 설정
            devcon.DisableDevice(); // 장치 비활성화
        }


       
    }
}
