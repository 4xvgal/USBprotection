using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// class information :: 
// 연결된 USB 저장장치 (이동저장장치만)의 인스턴스 ID 를 추출합니다. 
// 차단 또는 승인할 장치의 인스턴스 아이디에 사용됩니다.



namespace UsbSecurity
{
    using System;
    using System.Collections.Generic;
    using System.Management; // System.Management 네임스페이스 참조 필요

    class ParsingUsbDevice
    {
        private List<string> usbDeviceId = new List<string>(); // USB 장치 ID 가 저장되는 List 제너릭 


        public static List<string> GetConnectedUSBDevicesHardwareIDs()
        {
            List<string> pnpDeviceIDs = new List<string>();

            // 'Win32_PnPEntity'에서 USB 저장 장치만 필터링합니다.
            var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PnPEntity WHERE Service = 'USBSTOR'");

            foreach (ManagementObject queryObj in searcher.Get())
            {
                string deviceId = queryObj["PNPDeviceID"].ToString();
                if (!string.IsNullOrEmpty(deviceId))
                {
                    pnpDeviceIDs.Add(deviceId);
                }
            }

            return pnpDeviceIDs;
        }

        public void InsertData()
        {
            var externalDriveIDs = GetConnectedUSBDevicesHardwareIDs(); // 이동식 드라이브 인스턴스 ID를 가져옴 자료형은 
            foreach (var id in externalDriveIDs) // 왜 VAR 형인가 ? 
            {
                usbDeviceId.Add(id); //usb 장치 목록에 추출된 아이디를 추가한다.
            }

        }

        public void ShowList()
        { // usb 장치의 인스턴스 id 를 출력하는 함수
            Console.WriteLine("USB 장치 목록");
            foreach (var number in usbDeviceId)
            {
                Console.WriteLine(number);
            }
        }
    }
}

    


