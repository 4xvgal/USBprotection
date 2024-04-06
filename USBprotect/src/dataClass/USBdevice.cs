using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace USBprotect.src.dataClass
{

    internal class USBdevice
    {
        // USB 장치의 정보를 저장하는 클래스입니다.
        // WMI 로 얻을 수 있는 USB 장치의 정보를 필드로 구분하여 저장합니다
        public string name { get; set; }
        public string status { get; set; }
        public string deviceID { get; set; }
        public string pnpDeviceID { get; set; }
        public string description { get; set; }
    }
}
