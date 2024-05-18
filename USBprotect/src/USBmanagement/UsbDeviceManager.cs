using UsbSecurity;

namespace USBprotect.USBmanagement
{
    // USB 장치 관리를 위한 클래스
    class UsbDeviceManager
    {
        private DevconCMD devconCmd;

        public UsbDeviceManager()
        {
            devconCmd = new DevconCMD(); // DevconCMD 인스턴스 생성
        }

        // USB 장치를 활성화하는 메서드
        public string EnableDevice(USBinfo usbInfo)
        {
            string command = $"enable @{usbInfo.DeviceId}"; // Devcon enable 명령어 구성
            string result = devconCmd.DevconCommand(command); // Devcon 명령어 실행
            return result; // 결과 반환
        }

        // USB 장치를 비활성화하는 메서드
        public string DisableDevice(USBinfo usbInfo)
        {
            string command = $"disable @{usbInfo.DeviceId}"; // Devcon disable 명령어 구성
            string result = devconCmd.DevconCommand(command); // Devcon 명령어 실행
            return result; // 결과 반환
        }
    }
}