using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using USBsecurity;

namespace USBprotect.PermitRequest
{
    internal class PermitRequestApprove
    {
        private List<PermitRequest> requests; // 허용 요청을 저장하는 리스트
        private readonly string filePath = "PermitRequests.xml"; // 요청 목록 XML 파일 경로
        private readonly string approvedFilePath = "ApprovedRequests.xml"; // 승인된 요청 목록 XML 파일 경로
        private ManageAllowList manageAllowList; // ManageAllowList 인스턴스


        public PermitRequestApprove()   // 생성자
        {
            requests = new List<PermitRequest>(); // 리스트 초기화
            LoadRequests(); // 허용 요청 로드
            manageAllowList = new ManageAllowList(); // ManageAllowList 초기화
        }
        public List<PermitRequest> GetRequests()     // 현재 저장된 허용 요청 리스트를 반환하는 메서드
        {
            return requests; // 요청 리스트 반환
        }

   public void ApproveRequest(int index)    // 선택한 인덱스의 요청을 승인하는 메서드
        {
            if (index >= 0 && index < requests.Count)
            {
                var approvedRequest = requests[index];
                requests.RemoveAt(index); // 리스트에서 요청 삭제
                SaveRequests(); // 변경된 요청 목록 저장
                SaveApprovedRequest(approvedRequest); // 승인된 요청 저장
                manageAllowList.enableEveryDevice(approvedRequest.DeviceName); // ManageAllowList를 사용하여 장치 활성화
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
                XmlSerializer serializer = new XmlSerializer(typeof(List<PermitRequest>)); // 직렬화 객체 생성
                using (FileStream stream = new FileStream(filePath, FileMode.Create)) // 파일 스트림 열기
                {
                    serializer.Serialize(stream, requests); // 요청 리스트를 XML로 직렬화하여 파일에 저장
                }
            }
            catch (Exception ex)
            {
                throw new Exception("요청을 저장하는 중 오류 발생: " + ex.Message); // 저장 실패시 예외 발생
            }
        }

        private void SaveApprovedRequest(PermitRequest request)  // 승인된 요청을 파일에 저장하는 메서드
        {
            try
            {
                List<PermitRequest> approvedRequests;

                if (File.Exists(approvedFilePath)) // 파일이 존재하는지 확인
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(List<PermitRequest>));
                    using (FileStream stream = new FileStream(approvedFilePath, FileMode.Open)) // 파일 스트림 열기
                    {
                        approvedRequests = (List<PermitRequest>)serializer.Deserialize(stream); // XML을 역직렬화하여 요청 리스트에 할당
                    }
                }
                else
                {
                    approvedRequests = new List<PermitRequest>();
                }

                approvedRequests.Add(request);

                XmlSerializer approveSerializer = new XmlSerializer(typeof(List<PermitRequest>)); // 직렬화 객체 생성
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
                if (File.Exists(filePath)) // 파일이 존재하는지 확인
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(List<PermitRequest>)); // 역직렬화 객체 생성
                    using (FileStream stream = new FileStream(filePath, FileMode.Open)) // 파일 스트림 열기
                    {
                        requests = (List<PermitRequest>)serializer.Deserialize(stream); // XML을 역직렬화하여 요청 리스트에 할당
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
