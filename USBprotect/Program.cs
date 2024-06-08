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
        /// The main entry point for the application.
        /// </summary>
        
    
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            // Application.Run(new RequestManagementForm());  // 'MainForm'은 앱의 메인 폼입니다*

           // 허용 요청폼 실행 코드
            /*string initialMessage = "This is the initial message for the PermitRequestForm.";
            PermitRequestForm permitRequestForm = PermitRequestForm.GetInstance(initialMessage);

            Application.Run(permitRequestForm);*/
            Application.Run(new MainForm());

        }




        /*
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
        }
        
       */
    }
    
}