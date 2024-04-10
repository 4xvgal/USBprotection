using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

// class information :: 
// 현재 연결된 USB 저장장치 (이동저장장치만)의 인스턴스 ID 를 추출합니다. 
// 차단 또는 승인할 장치의 인스턴스 아이디에 사용됩니다.
// 마지막 수정일 2024-04-03 13:45 LGJ

namespace UsbSecurity
{
    using System;
    using System.Collections.Generic;
    using System.Management; // System.Management 네임스페이스 참조 필요
    using USBprotect.src.dataClass;

    class ParsingUsbDevice
    {
    
        private List<string> usbDeviceId = new List<string>(); // USB 장치 ID 가 저장되는 List 제너릭 
       
        public static List<string> GetConnectedUSBDevicesHardwareIDs()
        {
            List<string> pnpDeviceIDs = new List<string>();
            string pattern =  @"USB\\VID_[0-9A-F]+&PID_[0-9A-F]+"; // USB 장치 ID 패턴 정규식 
                
            // 'Win32_PnPEntity'에서 USB 저장 장치만 필터링합니다.
            var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PnPEntity WHERE Service = 'USBSTOR'");
            
            foreach (ManagementObject queryObj in searcher.Get())
            {
                string deviceId = queryObj["PNPDeviceID"].ToString(); // PNP 장치 ID를 가져옴;

                if (!string.IsNullOrEmpty(deviceId)) // 장치 ID가 비어있지 않다면
                {   
                    Match parsingdata = Regex.Match(deviceId, pattern); // 장치 ID를 정규식으로 추출
                    if (parsingdata.Success)    // 추출 성공시
                    {
                        pnpDeviceIDs.Add(parsingdata.Value); // 추출된 장치 ID를 리스트에 추가
                    }

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

        public void removeData(string id)
        {
            usbDeviceId.Clear();  
     
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

    


