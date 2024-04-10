using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using USBprotect.src.dataClass;

namespace USBprotect.src.USB.test
{
    internal class ParsingUsbDeviceTest : src.USB.ParsingUsbDevice
    {
        //parsingusbdevice 클래스의 테스트 클래스 
        //GetConnectedUSBdevices 메소드를 테스트합니다.
        public static void TestGetConnectedUSBdevices()
        {
            List<USBdevice> uSBdevices = ParsingUsbDevice.GetConnectedUSBdevices();

            foreach (var device in uSBdevices)
            {
                Console.WriteLine("Name: " + device.name);
                Console.WriteLine("Status: " + device.status);
                Console.WriteLine("DeviceID: " + device.deviceID);
                Console.WriteLine("PnpDeviceID: " + device.pnpDeviceID);
                Console.WriteLine("Description: " + device.description);
                Console.WriteLine();
            }
        }
    }
}
