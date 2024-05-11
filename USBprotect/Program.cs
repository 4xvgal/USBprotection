using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

using UsbSecurity;

namespace USBprotect
{
    internal static class Program
    {


        /// <summary>
        /// 해당 애플리케이션의 주 진입점입니다.
        /// </summary>
        /// 




        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();

        [STAThread]
        static void Main()
        {
            AllocConsole(); // 콘솔 창 할당

            DeviceMonitor watcher = new DeviceMonitor();
            watcher.Start();

            Console.WriteLine("=========================");
            Console.WriteLine("비인가 USB 접근 차단중...");
            Console.WriteLine("=========================");
            Console.ReadKey(); // 사용자 입력 대기

            watcher.Stop();
            /*
             // 수정 필요..
                      Application.EnableVisualStyles();
                      Application.SetCompatibleTextRenderingDefault(false);

                      UsbDeviceWatcher watcher = new UsbDeviceWatcher();
                      watcher.Start();
                      Application.Run(new Form1());
                  }
                   */
        }
    }
}