using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using USBprotect.src.dataClass;

namespace USBprotect.src.USB
{
    internal class ParsingUsbDevice
    {
        public static List<USBdevice> GetConnectedUSBdevices()
        {
            //USBdevice 클래스의 리스트 생성
            List<USBdevice> usbDevices = new List<USBdevice>();
            var searcher = new ManagementObjectSearcher(@"SELECT * FROM Win32_PnPEntity WHERE PNPDeviceID LIKE '%USB%'");

            //리스트에 정보 저장하기
            foreach (var device in searcher.Get())
            {
                USBdevice usbDevice = new USBdevice(); //temp USBdevice 객체 생성
                usbDevice.name = device["Name"].ToString(); //정보 필드 설정
                usbDevice.status = device["Status"].ToString();
                usbDevice.deviceID = device["DeviceID"].ToString();
                usbDevice.pnpDeviceID = device["PNPDeviceID"].ToString();
                usbDevice.description = device["Description"].ToString();
                usbDevices.Add(usbDevice); //객체를 List에 추가
            }
            return usbDevices;
        }
    }
}
