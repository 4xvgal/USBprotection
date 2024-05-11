
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace UsbSecurity
{
    class ManageBlackList
    {
        public static List<string> blackList = new List<string>(); // 블랙리스트를 저장할 리스트 생성 
        DevconCMD devconCMD = new DevconCMD(); // DevconCMD 클래스의 인스턴스 생성
       
        public void disableEveryDevice() // 블랙리스트 장치 비활성화 메서드
        {
            foreach (var device in ParsingUsbDevice.usbDevices) // 블랙리스트에 있는 장치들을 하나씩 꺼내서
            {

                devconCMD.DevconCommand($"disable \"{device.DeviceId}\""); //명령어 실행
                
            }
        }
       

    }
}