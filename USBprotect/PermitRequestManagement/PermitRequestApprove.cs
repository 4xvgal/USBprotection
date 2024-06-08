using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;



namespace UsbSecurity
{
    internal class PermitRequestApprove
    {
        private List<PermitRequestEnt> requests; // 허용 요청을 저장하는 리스트
        private readonly string approvedFilePath = "ApprovedRequests.xml"; // 승인된 요청 목록 XML 파일 경로
        private ManageAllowList manageAllowList; // ManageAllowList 인스턴스

        public PermitRequestApprove()   // 생성자
        {
            requests = new List<PermitRequestEnt>(); // 리스트 초기화
            LoadRequests(); // 허용 요청 로드
            manageAllowList = new ManageAllowList(); // ManageAllowList 초기화
        }

        public void ApproveRequest(int index)
        {
            if (index >= 0 && index < requests.Count)
            {
                var approvedRequest = requests[index];
                requests.RemoveAt(index); // 리스트에서 요청 삭제
                SaveRequests(); // 변경된 요청 목록 저장
                SaveApprovedRequest(approvedRequest); // 승인된 요청 저장

                // 승인된 요청을 화이트리스트로 추가
                USBinfo usbInfo = new USBinfo
                {
                    DeviceName = approvedRequest.DeviceName,
                    DeviceId = approvedRequest.DeviceId,
                    PnpDeviceId = approvedRequest.DeviceId, // 적절한 PnpDeviceId 설정
                    Status = "Approved",
                    IsWhiteListed = true
                };

                USBinfo.WhiteListDevices.Add(usbInfo); // 화이트리스트에 추가
                manageAllowList.enableEveryDevice(approvedRequest.DeviceId); // 장치 활성화
            }
            else
            {
                throw new ArgumentOutOfRangeException("index", "Index is out of range.");
            }
        }


        public void SaveRequests()  // 허용 요청을 파일에 저장하는 메서드
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<PermitRequestEnt>)); // 직렬화 객체 생성
                using (FileStream stream = new FileStream(PermitRequestEnt.FilePath, FileMode.Create)) // 파일 스트림 열기
                {
                    serializer.Serialize(stream, requests); // 요청 리스트를 XML로 직렬화하여 파일에 저장
                }
            }
            catch (Exception ex)
            {
                throw new Exception("요청을 저장하는 중 오류 발생: " + ex.Message); // 저장 실패시 예외 발생
            }
        }

        private void SaveApprovedRequest(PermitRequestEnt request)  // 승인된 요청을 파일에 저장하는 메서드
        {
            try
            {
                List<PermitRequestEnt> approvedRequests;

                if (File.Exists(approvedFilePath)) // 파일이 존재하는지 확인
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(List<PermitRequestEnt>));
                    using (FileStream stream = new FileStream(approvedFilePath, FileMode.Open)) // 파일 스트림 열기
                    {
                        approvedRequests = (List<PermitRequestEnt>)serializer.Deserialize(stream); // XML을 역직렬화하여 요청 리스트에 할당
                    }
                }
                else
                {
                    approvedRequests = new List<PermitRequestEnt>();
                }

                approvedRequests.Add(request);

                XmlSerializer approveSerializer = new XmlSerializer(typeof(List<PermitRequestEnt>)); // 직렬화 객체 생성
                using (FileStream stream = new FileStream(approvedFilePath, FileMode.Create)) // 파일 스트림 열기
                {
                    approveSerializer.Serialize(stream, approvedRequests); // 승인된 요청 리스트를 XML로 직렬화하여 파일에 저장
                }
            }
            catch (Exception ex)
            {
                throw new Exception("승인된 요청을 저장하는 중 오류 발생: " + ex.Message); // 저장 실패시 예외 발생
            }
        }

        private void LoadRequests()      // 파일에서 허용 요청을 로드하는 메서드
        {
            try
            {
                if (File.Exists(PermitRequestEnt.FilePath)) // 파일이 존재하는지 확인
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(List<PermitRequestEnt>)); // 역직렬화 객체 생성
                    using (FileStream stream = new FileStream(PermitRequestEnt.FilePath, FileMode.Open)) // 파일 스트림 열기
                    {
                        requests = (List<PermitRequestEnt>)serializer.Deserialize(stream); // XML을 역직렬화하여 요청 리스트에 할당
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
