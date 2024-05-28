using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using System.IO;
using NUnit.Framework;  

// class information :: 
// 현재 연결된 USB 저장장치 (이동저장장치만)의 인스턴스 ID 를 추출합니다. 
// 차단 또는 승인할 장치의 인스턴스 아이디에 사용됩니다.
// 2024:04:04:14:15 마지막 수정 LGJ

namespace UsbSecurity
{
    using System;
    using System.Collections.Generic;
    using System.Management; // System.Management 네임스페이스 참조 필요
    using System.Windows.Forms;


    [TestFixture]
    class ParsingUsbDevice
    {
        [Test]
        public void GetUsbDevices()
        {
            string wmiQuery = "SELECT Name, Status, DeviceID, PNPDeviceID, Description FROM Win32_PnPEntity WHERE PNPDeviceID LIKE 'USB%' AND (Description LIKE '%디스크 드라이브%' OR DeviceID LIKE 'USBSTOR%')";
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(wmiQuery);

            lock (USBinfo._lock) // 리스트 수정 작업에 대한 동기화 처리
            {
                foreach (ManagementObject queryObj in searcher.Get())
                {
                    USBinfo usbDevice = new USBinfo
                    {
                        DeviceName = queryObj["Name"]?.ToString() ?? "Unknown",
                        Status = queryObj["Status"]?.ToString() ?? "Unknown",
                        DeviceId = queryObj["DeviceID"]?.ToString() ?? "Unknown",
                        PnpDeviceId = queryObj["PNPDeviceID"]?.ToString() ?? "Unknown",
                        Description = queryObj["Description"]?.ToString() ?? "Unknown",
                    };


                    var existingDevice = USBinfo.BlackListDevices.FirstOrDefault(x => AreDevicesEqual(x, usbDevice));
                    if (existingDevice != null)
                    {
                        USBinfo.BlackListDevices.Remove(existingDevice); // 기존 장치 제거
                    }

                    USBinfo.BlackListDevices.Add(usbDevice); // 새 장치 추가

                }   
            }
        }

        
        private bool AreDevicesEqual(USBinfo device1, USBinfo device2) // 장치가 같은지 확인
        {
            return device1.DeviceId == device2.DeviceId && device1.PnpDeviceId == device2.PnpDeviceId; // 비교연산
        }

        public void showUSBinfo()
        {
            foreach(var device in USBinfo.BlackListDevices)
            {
                Console.WriteLine("Device Name : " + device.DeviceName);
                Console.WriteLine("Status : " + device.Status);
                Console.WriteLine("Device ID : " + device.DeviceId);
                Console.WriteLine("Pnp Device ID : " + device.PnpDeviceId);
                Console.WriteLine("Description : " + device.Description);
                Console.WriteLine("=====================================");
            }   
        }
      
    }


}

    


