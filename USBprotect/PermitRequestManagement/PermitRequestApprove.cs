using System;
using System.Collections.Generic;
using System.Linq;

namespace UsbSecurity
{
    internal class PermitRequestApprove
    {
        private List<PermitRequestEnt> requests;
        private ManageAllowList manageAllowList;
        private PermitRequestEnt permitrequestent;

        // 생성자에서는 컴포넌트를 초기화하고 보류 중인 요청을 불러옵니다
        public PermitRequestApprove()
        {
            manageAllowList = new ManageAllowList(); // 허용 목록을 관리하는 컴포넌트
            permitrequestent = new PermitRequestEnt(); // 허가 요청 데이터를 처리하는 엔티티 클래스
            requests = permitrequestent.LoadRequests(); // 현재 USB 디바이스 요청을 불러옵니다
        }

        // 특정 요청을 인덱스 기반으로 승인하는 메서드
        public void ApproveRequest(int index)
        {
            var requests = permitrequestent.LoadRequests(); // 업데이트된 요청 사항을 확인하기 위해 요청을 다시 불러옵니다
            if (index >= 0 && index < requests.Count) // 인덱스가 유효 범위 내에 있는지 확인
            {
                var approvedRequest = requests[index]; // 승인할 요청을 가져옵니다
                requests.RemoveAt(index); // 승인 후 요청 목록에서 해당 요청을 제거합니다
                permitrequestent.SaveRequests(requests); // 업데이트된 요청 목록을 저장합니다

                // 해당 디바이스를 블랙리스트에서 제거합니다 (해당하는 경우)
                var blackListDevice = USBinfo.BlackListDevices.FirstOrDefault(d => d.DeviceId == approvedRequest.DeviceId);
                if (blackListDevice != null)
                {
                    USBinfo.BlackListDevices.Remove(blackListDevice); // 블랙리스트에서 제거
                }

                USBinfo usbInfo = new USBinfo
                {
                    DeviceName = approvedRequest.DeviceName, // 디바이스 이름
                    DeviceId = approvedRequest.DeviceId, // 디바이스 ID
                    PnpDeviceId = approvedRequest.DeviceId, // PnP 디바이스 ID
                    Status = "Approved", // 승인 상태
                    IsWhiteListed = true // 화이트리스트에 추가
                };

                USBinfo.WhiteListDevices.Add(usbInfo); // 화이트리스트에 디바이스 정보 추가
                USBinfo.RemoveBlackListDevice(usbInfo); // 블랙리스트에서 디바이스 정보 제거
                ManageAllowList manageAllowList = new ManageAllowList();
                manageAllowList.enableEveryDevice(approvedRequest.DeviceId); // 승인된 디바이스에 대한 모든 장치 활성화 처리
            }
            else
            {
                throw new ArgumentOutOfRangeException("index", "Index is out of range."); // 인덱스가 범위를 벗어날 경우 예외 발생
            }
        }
    }
}