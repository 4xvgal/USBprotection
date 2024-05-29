using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace USBprotect
{ 

// USB 장치 정보 클래스
    public class USBinfo
    {
        // 스레드 동기화를 위한 lock 객체
        public static readonly object _lock = new object();

        // 블랙리스트 장치 목록
        public static ObservableCollection<USBinfo> BlackListDevices = new ObservableCollection<USBinfo>();

        // 화이트리스트 장치 목록
        public static ObservableCollection<USBinfo> WhiteListDevices = new ObservableCollection<USBinfo>();

        // 장치 이름
        public string DeviceName { get; set; }

        // 장치 상태
        public string Status { get; set; }

        // 장치 ID
        public string DeviceId { get; set; }

        // PnP 장치 ID
        public string PnpDeviceId { get; set; }

        // 장치 설명
        public string Description { get; set; }

        // 화이트리스트에 등록되어 있는지 여부
        public bool IsWhiteListed { get; set; }

        // 블랙리스트에 장치 추가하는 메서드
        public static void AddBlackListDevice(USBinfo device)
        {
            lock (_lock)
            {
                BlackListDevices.Add(device);
            }
        }

        // 블랙리스트에서 장치 제거하는 메서드
        public static void RemoveBlackListDevice(USBinfo device)
        {
            lock (_lock)
            {
                BlackListDevices.Remove(device);
            }
        }
    }
}
