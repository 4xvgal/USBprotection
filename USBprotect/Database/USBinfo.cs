using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace UsbSecurity
{ 

// USB 장치 정보 클래스
    public class USBinfo
    {
        // 스레드 동기화를 위한 lock 객체
        public static readonly object _lock = new object();
        private static readonly string WfilePath = "WhiteList.xml"; // XML 파일 경로
        private static readonly string BfilePath = "BlackList.xml"; // XML 파일 경로    

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
        public static  void AddBlackListDevice(USBinfo device)
        {
            lock (_lock)
            {
                BlackListDevices.Add(device);
            }
        }

        // 블랙리스트에서 장치 제거하는 메서드
        public void RemoveBlackListDevice(USBinfo device)
        {
            lock (_lock)
            {
                BlackListDevices.Remove(device);
            }
        }

        public static void SaveWhiteList()  // 허용 요청을 파일에 저장하는 메서드
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<USBinfo>)); // 직렬화 객체 생성
                using (FileStream stream = new FileStream(WfilePath, FileMode.Create)) // 파일 스트림 열기
                {
                    serializer.Serialize(stream, WhiteListDevices); // 요청 리스트를 XML로 직렬화하여 파일에 저장
                }
            }
            catch (Exception ex)
            {
                throw new Exception("요청을 저장하는 중 오류 발생: " + ex.Message); // 저장 실패시 예외 발생
            }
        }

        public static void LoadWhiteList() // 파일에서 허용 요청을 로드하는 메서드
        {
            try
            {
                if (File.Exists(WfilePath)) // 파일이 존재하는지 확인
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<USBinfo>)); // 역직렬화 객체 생성
                    using (FileStream stream = new FileStream(WfilePath, FileMode.Open)) // 파일 스트림 열기
                    {
                        WhiteListDevices = ((ObservableCollection<USBinfo>)serializer.Deserialize(stream)); // XML을 역직렬화하여 요청 리스트에 할당
                    }
                }
            }
            catch (Exception ex)
            {   
                throw new Exception("요청을 불러오는 중 오류 발생: " + ex.Message); // 로딩 실패시 예외 발생
            }
        }


        public static void SaveBlackList()  // 허용 요청을 파일에 저장하는 메서드
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<USBinfo>)); // 직렬화 객체 생성
                using (FileStream stream = new FileStream(BfilePath, FileMode.Create)) // 파일 스트림 열기
                {
                    serializer.Serialize(stream, BlackListDevices); // 요청 리스트를 XML로 직렬화하여 파일에 저장
                }
            }
            catch (Exception ex)
            {
                throw new Exception("요청을 저장하는 중 오류 발생: " + ex.Message); // 저장 실패시 예외 발생
            }
        }

        public static void LoadBlackList() // 파일에서 허용 요청을 로드하는 메서드
        {
            try
            {
                if (File.Exists(BfilePath)) // 파일이 존재하는지 확인
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<USBinfo>)); // 역직렬화 객체 생성
                    using (FileStream stream = new FileStream(BfilePath, FileMode.Open)) // 파일 스트림 열기
                    {
                        BlackListDevices = ((ObservableCollection<USBinfo>)serializer.Deserialize(stream)); // XML을 역직렬화하여 요청 리스트에 할당
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("요청을 불러오는 중 오류 발생: " + ex.Message); // 로딩 실패시 예외 발생
            }
        }
    }
}
