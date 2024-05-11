﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

// class information :: 
// 현재 연결된 USB 저장장치 (이동저장장치만)의 인스턴스 ID 를 추출합니다. 
// 차단 또는 승인할 장치의 인스턴스 아이디에 사용됩니다.
// 2024:04:04:14:15 마지막 수정 LGJ

namespace UsbSecurity
{
    using System;
    using System.Collections.Generic;
    using System.Management; // System.Management 네임스페이스 참조 필요
    using USBprotect.InternalFunction;

    class ParsingUsbDevice
    {
        public static List<USBinfo> usbDevices = new List<USBinfo>();

        public void GetUsbDevices()
        {
            var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PnPEntity WHERE Service = 'USBSTOR'");

            foreach (ManagementObject queryObj in searcher.Get())
            {
                USBinfo usbDevice = new USBinfo
                {
                    DeviceName = queryObj["Name"]?.ToString() ?? "Unknown",
                    Status = queryObj["Status"]?.ToString() ?? "Unknown",
                    DeviceId = queryObj["DeviceID"]?.ToString() ?? "Unknown",
                    PnpDeviceId = queryObj["PNPDeviceID"]?.ToString() ?? "Unknown",
                    Description = queryObj["Description"]?.ToString() ?? "Unknown"
                };

            
                var existingDeviceIndex = usbDevices.FindIndex(existingDevice => AreDevicesEqual(existingDevice, usbDevice));

                if (existingDeviceIndex != -1)
                {
                    usbDevices.RemoveAt(existingDeviceIndex);
                }
                else
                {
                    usbDevices.Add(usbDevice);
                }
            }
        }

        private bool AreDevicesEqual(USBinfo device1, USBinfo device2)
        {
           
            return device1.DeviceId == device2.DeviceId && device1.PnpDeviceId == device2.PnpDeviceId;
        }

        public void showUSBinfo()
        {
            foreach(var device in usbDevices)
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

    


